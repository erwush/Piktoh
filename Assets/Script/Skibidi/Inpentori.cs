using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Inpentori : MonoBehaviour
{
    public List<Item> item = new List<Item>();
    public bool isOpen;
    public TextMeshProUGUI textMesh;
    public GameObject panel;
    public Image[] slotImg;
    public Image[] slot;
    public TextMeshProUGUI[] stackCount;
    public bool isOpenedOnce;
    public GameObject inv;
    public Scrollbar scrollBar;
    private bool isMoving;
    private YuAi YuAi;
    public GameObject inven;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i] = panel.transform.GetChild(i).GetComponent<Image>();
            slotImg[i] = slot[i].transform.Find("ItemSprite").GetComponent<Image>();

            stackCount[i] = panel.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
        }
        isOpenedOnce = false;
        YuAi = GameObject.Find("Mekanik").GetComponent<YuAi>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isMoving)
        {
            ToggleInventory();
        }
    }

    // Update is called once per frame
    void Awake()
    {
           item.AddRange(Resources.LoadAll<Item>("Items"));
    }

    IEnumerator ShowInventory()
    {
        isMoving = true;
        if (!isOpenedOnce)
        {
            for (int i = 0; i < item.Count; i++)
            {
                slotImg[i].sprite = item[i].itemSprite;
                stackCount[i].text = item[i].itemCount.ToString();
            }
        }
        inv.SetActive(true);
        inven.GetComponent<Animator>().Play("Open");
        yield return new WaitForSeconds(0.5f);
        inven.GetComponent<Animator>().speed = 0;
        isOpenedOnce = true;
        isMoving = false;
    }

    IEnumerator CloseInventory()
    {
        isMoving = true;
        inven.GetComponent<Animator>().speed = 1;
        inven.GetComponent<Animator>().Play("Close");
        yield return new WaitForSeconds(0.5f);
        inv.SetActive(false);
        scrollBar.value = 1;
        isMoving = false;
    }

    public void ToggleInventory()
    {
        if (isOpen)
        {
            StartCoroutine(CloseInventory());
            isOpen = false;
            YuAi.isOpen = false;
        }
        else if(!isOpen && !YuAi.isOpen)
        {
            StartCoroutine(ShowInventory());
            isOpen = true;
            YuAi.isOpen = true;
        }
    }
}
