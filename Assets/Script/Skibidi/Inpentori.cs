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
    public Image[] slot;
    public TextMeshProUGUI[] stackCount;
    public bool isOpenedOnce;
    public GameObject inv;
    private YuAi YuAi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i] = panel.transform.GetChild(i).GetComponent<Image>();
            stackCount[i] = panel.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
        }
        isOpenedOnce = false;
        YuAi = GameObject.Find("Mekanik").GetComponent<YuAi>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    // Update is called once per frame
    void Awake()
    {
           item.AddRange(Resources.LoadAll<Item>("Items"));
    }

    public void ShowInventory()
    {
        if (!isOpenedOnce)
        {
            for (int i = 0; i < item.Count; i++)
            {
                slot[i].sprite = item[i].itemSprite;
                stackCount[i].text = item[i].itemCount.ToString();
            }
        }
        inv.SetActive(true);
        isOpenedOnce = true;
    }

    public void CloseInventory()
    {
        inv.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (isOpen)
        {
            CloseInventory();
            isOpen = false;
            YuAi.isOpen = false;
        }
        else if(!isOpen && !YuAi.isOpen)
        {
            ShowInventory();
            isOpen = true;
            YuAi.isOpen = true;
        }
    }
}
