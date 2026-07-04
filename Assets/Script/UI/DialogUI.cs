using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    //0 = DIALOGUE TEXT
    //1 = NAME

    public Image image;
    public GameObject UI;
    public GameObject obj;
    public float typingSpeed;
    public GameObject player;
    public Dialog dial;
    public Inpentori inven;
    public Interactable npc;
    public string dialtext;
    public Dialog[] sekardadu;
    public Quest[] quest;
    public GameObject gambarakhir;

    private Coroutine dialCor;
    private bool isTyping;
    private string currentFullText;
    bool fadeGambarAkhir;
    float fadeSpeed = 0.7f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        text[0].text = dialtext;

        if (fadeGambarAkhir)
        {
            Image img = gambarakhir.GetComponent<Image>();

            Color c = img.color;
            c.a += Time.deltaTime * fadeSpeed;
            img.color = c;

            if (c.a >= 1f)
            {
                c.a = 1f;
                img.color = c;
                fadeGambarAkhir = false;
            }
        }
    }

    IEnumerator TypeDialog(string fullText)
    {
        isTyping = true;
        currentFullText = fullText;
        dialtext = "";

        foreach (char c in fullText)
        {
            dialtext += c;
            yield return new WaitForSeconds(dial.typingSpeed);
        }

        dialtext = fullText;
        isTyping = false;
        dialCor = null;
    }

    public void ChangeDialog()
    {
        // Kalau masih mengetik, langsung tampilkan semua teks
        if (isTyping)
        {
            dialtext = currentFullText;
            isTyping = false;

            if (dialCor != null)
            {
                StopCoroutine(dialCor);
                dialCor = null;
            }

            return;
        }

        int i = dial.currentDial;

        // Dialog selesai
        if (i >= dial.dialogCount)
        {
            dial.currentDial = 0;

            // Ganti dialog NPC ke dialog berikutnya
            for (int j = 0; j < sekardadu.Length; j++)
            {
                if (sekardadu[j] == npc.dialog)
                {
                    if (j < sekardadu.Length - 1)
                    {
                        npc.dialog = sekardadu[j + 1];

                        Debug.Log(
                            "Dialog NPC berubah ke: " +
                            npc.dialog.name
                        );
                    }
                    else
                    {
                        gambarakhir.SetActive(true);

                        Image img = gambarakhir.GetComponent<Image>();

                        Color c = img.color;
                        c.a = 0f;
                        img.color = c;

                        fadeGambarAkhir = true;

                        Destroy(npc.gameObject);
                    }

                    break;
                }
                if(npc.dialog.codeName == "kayang0" || npc.dialog.codeName == "kayang1")
                {
                    StartCoroutine(Kayang(npc.dialog.codeName, npc.GetComponent<Animator>()));
                }
            }

            if (dial.isItem)
            {
                StartCoroutine(GiveItem(dial.givenItem));
            }
            else
            {
                UI.SetActive(false);
                player.GetComponent<Movement>().canMove = true;

                if (Questing.Instance.daftarMisi[8].status == QuestStatus.Active)
                    Questing.Instance.LaporkanProgress(8, 1);
                else if (Questing.Instance.daftarMisi[10].status == QuestStatus.Active)
                    Questing.Instance.LaporkanProgress(10, 1);
            }

            return;
        }

        text[1].text = dial.nama[i];
        image.sprite = dial.avatar[i];
        image.preserveAspect = true;

        string fullText = dial.text[i];

        if (dialCor != null)
        {
            StopCoroutine(dialCor);
            dialCor = null;
        }

        dialCor = StartCoroutine(TypeDialog(fullText));

        dial.currentDial++;
    }

    public IEnumerator Kayang(string name, Animator anim)
    {
        if(name == "kayang0")
        {
            anim.Play("kayang0");
            yield return new WaitForSeconds(0.75f);
            Destroy(anim.gameObject);
        } else if(name == "kayang1")
        {
            anim.Play("kayang1");
            yield return new WaitForSeconds(1.45f);
            Destroy(anim.gameObject);
        }
    }

    //* ITEM SECTION

    public GameObject itemObj;
    public TextMeshProUGUI itemName;
    public GameObject textBg;
    public GameObject itemUI;

    public IEnumerator GiveItem(Item theItem)
    {
        UI.SetActive(false);
        itemUI.SetActive(true);
        itemObj.SetActive(true);
        textBg.SetActive(true);

        itemObj.GetComponent<Animator>().speed = 1f;
        textBg.GetComponent<Animator>().speed = 1f;

        itemObj.GetComponent<Animator>().Play("entry");
        textBg.GetComponent<Animator>().Play("entry");

        itemObj.GetComponent<Image>().sprite = theItem.itemSprite;

        yield return new WaitForSeconds(0.5f);

        itemName.gameObject.SetActive(true);
        itemName.text = "Mendapatkan " + theItem.itemName + "!";

        itemObj.GetComponent<Animator>().speed = 0f;
        textBg.GetComponent<Animator>().speed = 0f;
    }

    public void CloseItem()
    {
        StartCoroutine(GetItemExit());
    }

    public IEnumerator GetItemExit()
    {
        itemName.gameObject.SetActive(false);

        itemObj.GetComponent<Animator>().speed = 1f;
        textBg.GetComponent<Animator>().speed = 1f;

        itemObj.GetComponent<Animator>().Play("exit");
        textBg.GetComponent<Animator>().Play("exit");

        yield return new WaitForSeconds(0.5f);

        itemObj.GetComponent<Animator>().speed = 0f;
        textBg.GetComponent<Animator>().speed = 0f;

        itemObj.SetActive(false);
        textBg.SetActive(false);
        itemUI.SetActive(false);

        player.GetComponent<Movement>().canMove = true;
    }
}