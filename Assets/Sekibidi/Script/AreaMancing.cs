using Unity.VisualScripting;
using UnityEngine;

public class AreaMancing : MonoBehaviour
{
    public bool inArea;
    public GameObject mancingUI;
    public Mancing mancing;
    public GameObject pleyer;
    public BatangPanas hotbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inArea = true;
            if(pleyer == null) pleyer = collision.gameObject;
        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(pleyer == null) pleyer = collision.gameObject;
            inArea = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && Input.GetKeyDown(KeyCode.E) && !mancing.isFishing && hotbar.activeSlot == 4)
        {
            mancingUI.SetActive(true);
        }
    }
}
