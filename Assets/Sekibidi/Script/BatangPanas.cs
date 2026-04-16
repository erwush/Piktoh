using UnityEngine;
using UnityEngine.UI;


public class BatangPanas : MonoBehaviour
{
 
    public int activeSlot;
    public int maxSlot;
    public Image[] slotImg;
    public Sprite[] slotSprite; //0 = unselcted, 1 = selected
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                activeSlot = i;
                slotImg[i].sprite = slotSprite[1];
                for (int j = 0; j < maxSlot; j++)
                {
                    if (j != i)
                    {
                        slotImg[j].sprite = slotSprite[0];
                    }
                }
            }
        }
        //input with scroll up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (activeSlot < maxSlot - 1)
            {
                activeSlot += 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[activeSlot - 1].sprite = slotSprite[0];
            } else if(activeSlot == maxSlot - 1)
            {
                activeSlot = 0;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[maxSlot - 1].sprite = slotSprite[0];
            }
        }
        //input with scroll down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (activeSlot > 0)
            {
                activeSlot -= 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[activeSlot + 1].sprite = slotSprite[0];
            } else if(activeSlot == 0)
            {
                activeSlot = maxSlot - 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[0].sprite = slotSprite[0];
            }
        }
    }
}
