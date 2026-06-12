using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject UI;
    public GameObject[] Scroll;
    public bool isOpen;
    private Coroutine cor;
    private YuAi YuAi;
    public Animator anim;
    
    [Header("UI Text Elements")]
    // quesText[0] = Tempat Judul, quesText[1] = Tempat Deskripsi
    public TextMeshProUGUI[] quesText; 

    private Questing questingManager;

    void Start()
    {
        YuAi = GameObject.Find("Mekanik").GetComponent<YuAi>();
        
        // Mencari komponen Questing yang ada di scene secara otomatis
        questingManager = Object.FindFirstObjectByType<Questing>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cor == null)
        {
            cor = StartCoroutine(MoveUI());
        }
    }

    // Fungsi pembantu untuk mengubah isi teks teks dari script Questing
    public void UpdateTeksMisi(string judul, string deskripsi)
    {
        if (quesText.Length >= 2)
        {
            quesText[0].text = judul;       // Mengisi Questext1
            quesText[1].text = deskripsi;   // Mengisi Questext2
        }
    }

    IEnumerator MoveUI()
    {
        RectTransform rect = UI.GetComponent<RectTransform>();

        if (!isOpen && !YuAi.isOpen)
        {
            if (questingManager.isNotif)
            {
                questingManager.notif.speed = 1f;
                questingManager.notif.Play("Naik");
                yield return new WaitForSeconds(0.5f);
                questingManager.notif.speed = 0f;
                questingManager.isNotif = false;
            }
            isOpen = true;
            YuAi.isOpen = true;

            // Pastikan data teks adalah data misi yang paling update sebelum UI terbuka penuh
            if (questingManager != null)
            {
                questingManager.TampilkanMisiAktif();
            }

            anim.speed = 1f;
            anim.Play("Open");
            // while (rect.anchoredPosition.x > 770)
            // {
            //     rect.anchoredPosition = new Vector2(
            //         rect.anchoredPosition.x - 10,
            //         rect.anchoredPosition.y
            //     );
            //     yield return null;
            // }
            yield return new WaitForSeconds(0.5f);
            anim.speed = 0f;
            rect.anchoredPosition = new Vector2(770, rect.anchoredPosition.y);

            Scroll[0].GetComponent<Animator>().speed = 1;
            Scroll[0].GetComponent<Animator>().Play("ScrollOpen");
            Scroll[1].GetComponent<Animator>().speed = 1;
            Scroll[1].GetComponent<Animator>().Play("ScrollOpen");
            
            yield return new WaitForSeconds(0.75f);
            Scroll[1].GetComponent<Animator>().speed = 0;
            Scroll[0].GetComponent<Animator>().speed = 0;
            
            quesText[0].gameObject.SetActive(true);
            quesText[1].gameObject.SetActive(true);
        }
        else if (isOpen)
        {
            isOpen = false;
            YuAi.isOpen = false;
            Scroll[0].GetComponent<Animator>().speed = 1;
            quesText[0].gameObject.SetActive(false);
            quesText[1].gameObject.SetActive(false);
            Scroll[0].GetComponent<Animator>().Play("ScrollClose");
            Scroll[1].GetComponent<Animator>().speed = 1;
            Scroll[1].GetComponent<Animator>().Play("ScrollClose");
            
            yield return new WaitForSeconds(0.75f);
            Scroll[1].GetComponent<Animator>().speed = 0;
            Scroll[0].GetComponent<Animator>().speed = 0;

            // while (rect.anchoredPosition.x < 1150)
            // {
            //     rect.anchoredPosition = new Vector2(
            //         rect.anchoredPosition.x + 10,
            //         rect.anchoredPosition.y
            //     );
            //     yield return null;
            // }
            anim.speed = 1f;
            anim.Play("Close");
            yield return new WaitForSeconds(0.5f);
            anim.speed = 0f;
            rect.anchoredPosition = new Vector2(1150, rect.anchoredPosition.y);
        }

        cor = null;
    }
}