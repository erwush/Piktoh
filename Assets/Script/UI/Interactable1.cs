using UnityEngine;

public class Interactable1 : MonoBehaviour
{
    public bool inArea;
    public GameObject DialogUI;
    public DialogUI dialScript;
    public GameObject self;
    public Dialog dialog;
    public GameObject keybind;
    public YuAi yuai;
    public GameObject player;
    public MainMenu mainMenu;
    public string scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetButtonDown("Interact"))
        {
            // Debug.Log("skibidi");
            Dialog();
        }
    }
    
    public void Dialog()
    {
           DialogUI.SetActive(true);
            // player.GetComponent<Movement>().StopMove();
            dialScript.dial = dialog;
            if (dialog.currentDial != dialog.dialogCount)
            {
                dialScript.ChangeDialog();
            }
            else
            {
                mainMenu.GoToScene(scene);
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
