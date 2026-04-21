using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

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
    public Button[] dropButton;
    public Button[] useButton;
    private bool isMoving;
    private bool isSelecting;
    private Player pleyer;
    private int currentSelected;
    public SlotInpen[] slotInpen;
    private YuAi YuAi;
    public GameObject inven;
    public Button[] slotBtn;

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
        //find tag player;
        pleyer = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        currentSelected = -1;
        isMoving = true;
        if (!isOpenedOnce)
        {
            for (int i = 0; i < item.Count; i++)
            {
                slotImg[i].sprite = item[i].itemSprite;
                stackCount[i].text = item[i].itemCount.ToString();
                dropButton[i] = slot[i].transform.Find("Drop").GetComponent<Button>();
                useButton[i] = slot[i].transform.Find("Use").GetComponent<Button>();
                slotBtn[i] = slot[i].GetComponent<Button>();
                int index = i;
                slotBtn[i].onClick.AddListener(() => SelectItem(index));
                slotInpen[i] = slot[i].GetComponent<SlotInpen>();
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
        else if (!isOpen && !YuAi.isOpen)
        {
            StartCoroutine(ShowInventory());
            isOpen = true;
            YuAi.isOpen = true;
        }
    }

    public void DropItem(int id)
    {
        if (item[id].itemCount > 0)
        {
            item[id].itemCount--;
            stackCount[id].text = item[id].itemCount.ToString();
            Debug.Log("Drop Item");
        }
    }

    public void UseItem(int id)
    {
        if (item[id].itemCount > 0)
        {
            item[id].itemCount--;
            stackCount[id].text = item[id].itemCount.ToString();
            pleyer.ChangeEnergy(50f);
            pleyer.ChangeHealth(0.3f);
        }
    }

    public void SelectItem(int id)
    {
        for (int i = 0; i < dropButton.Length; i++)
        {
            dropButton[i].gameObject.SetActive(false);
            useButton[i].gameObject.SetActive(false);
        }

        dropButton[id] = slotInpen[id].dropBtn;
        useButton[id] = slotInpen[id].useBtn;
        currentSelected = id;
        dropButton[id].gameObject.SetActive(true);
        dropButton[id].onClick.RemoveAllListeners();
        dropButton[id].onClick.AddListener(() => DropItem(id));
        if (item[id].isFood)
        {
            useButton[id].gameObject.SetActive(true);
            useButton[id].onClick.RemoveAllListeners();
            useButton[id].onClick.AddListener(() => UseItem(id));
        }
        else
        {
            RectTransform rect = dropButton[id].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, 5);
            useButton[id].gameObject.SetActive(false);
        }
    }

}