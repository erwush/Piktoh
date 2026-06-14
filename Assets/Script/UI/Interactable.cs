using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool inArea;
    public GameObject DialogUI;
    public DialogUI dialScript;
    public GameObject self;
    public Dialog dialog;
    public GameObject keybind;
    public YuAi yuai;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = gameObject;
        // player = GameObject.FindWithTag("Player");
        dialScript = GameObject.FindWithTag("Mekanik").GetComponent<DialogUI>();
        DialogUI = dialScript.UI;
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetButtonDown("Interact"))
        {
            DialogUI.SetActive(true);
            player.GetComponent<Movement>().StopMove();
            dialScript.dial = dialog;
            dialScript.ChangeDialog();
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = true;
            player = collision.gameObject;
            keybind.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            inArea = false;
            keybind.SetActive(false);
        }
    }
}
