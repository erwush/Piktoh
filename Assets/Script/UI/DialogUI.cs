using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    //0 = DIALOGUE TEXT
    //1 = Name 1
    public Image image;
    public GameObject UI;
    public GameObject obj;
    public float typingSpeed;
    public GameObject player;
    public Dialog dial;
    public Inpentori inven;
    public string dialtext;
    private Coroutine dialCor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // dial = obj.GetComponent<Interactable>().dialog;
    }


    // Update is called once per frame
    void Update()
    {
        text[0].text = dialtext;

    }

    IEnumerator TypeDialog()
    {
        typingSpeed = dial.typingSpeed;
        dialtext = "";
        foreach (char c in dial.text[dial.currentDial])
        {
            dialtext += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ChangeDialog()
    {
        if (dialCor != null)
        {
            StopCoroutine(dialCor);
            dialCor = null;
        }
        int i = dial.currentDial;
        if (i >= dial.dialogCount)
        {
            dial.currentDial = 0;

            if (dial.isItem)
            {
                StartCoroutine(GiveItem(dial.givenItem));
            }

        }
        else
        {

            text[1].text = dial.nama[i];
            image.sprite = dial.avatar[i];
            image.preserveAspect = true;
            if (dialCor == null)
            {
                dialCor = StartCoroutine(TypeDialog());
            }
            dial.currentDial++;
            i++;
            if (i > dial.dialogCount)
            {
                UI.SetActive(false);
                player.GetComponent<Movement>().canMove = true;
            }

        }
    }

    //*item section
    public GameObject itemObj;
    public TextMeshProUGUI itemName;
    public GameObject textBg;
    public GameObject itemUI;
    // public void GainItem(Item item)
    // {
    //     StartCoroutine(GetItem(item));
    // }

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
        inven.AddItem(theItem, dial.itemCount);
        itemName.text = "Mendapatkan " + dial.itemCount + " " + theItem.itemName + "!";
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
