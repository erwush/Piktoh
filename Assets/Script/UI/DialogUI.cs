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
    public string dialtext;
    private Coroutine dialCor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dial = obj.GetComponent<Interactable>().dialog;
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
        if(dialCor != null)
        {
            StopCoroutine(dialCor);
            dialCor = null;
        }
        int i = dial.currentDial;
        if (i >= dial.dialogCount)
        {
            dial.currentDial = 0;
            player.GetComponent<Movement>().canMove = true;
            UI.SetActive(false);
        }
        else
        {
            
            text[1].text = dial.nama[i];
            image.sprite = dial.avatar[i];
            image.preserveAspect = true;
            if(dialCor == null) {
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
}
