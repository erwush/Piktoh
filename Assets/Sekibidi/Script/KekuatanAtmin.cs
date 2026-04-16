using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KekuatanAtmin : MonoBehaviour
{
    public string[] listKode;
    public string text;
    public Action[] lariCit;
    public GameObject menuCit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listKode = new string[2] { "Give", "2" };
        lariCit = new Action[2] { GiveItem, null};
    }

    // Update is called once per frame
    void Update()
    {
        //need to hold p093 at the same time
        if (Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.Alpha0) && Input.GetKey(KeyCode.Alpha9) && Input.GetKey(KeyCode.Alpha3))
        {
            menuCit.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        menuCit.SetActive(false);
    }

    public void Ngecit()
    {
        bool cek = false;
        Action cit = null;
        int index;
        for (int i = 0; i < listKode.Length; i++)
        {

            if (listKode[i].Contains(text))
            {
                index = i;
                Debug.Log("Kekuatan Atmin");
                cek = true;
                cit = lariCit[index];
                break;
            }

        }

        if (cek)
        {
            cit?.Invoke();
        }
        else
        {
            return;
        }
    }

    void GiveItem()
    {
        
    }
}
