using UnityEngine;
using UnityEngine.UI;

public class BatangPanas : MonoBehaviour
{
    // 1. TAMBAHKAN INI: Agar script lain bisa mengakses BatangPanas dengan mudah
    public static BatangPanas instance;

    public int activeSlot;
    public int maxSlot;
    public Image[] slotImg;
    public Sprite[] slotSprite; //0 = unselected, 1 = selected

    private void Awake()
    {
        // 2. TAMBAHKAN INI: Set variabel instance saat game mulai
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
    }

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

        // Input scroll up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (activeSlot < maxSlot - 1)
            {
                activeSlot += 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[activeSlot - 1].sprite = slotSprite[0];
            } 
            else if(activeSlot == maxSlot - 1)
            {
                activeSlot = 0;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[maxSlot - 1].sprite = slotSprite[0];
            }
        }

        // Input scroll down
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (activeSlot > 0)
            {
                activeSlot -= 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[activeSlot + 1].sprite = slotSprite[0];
            } 
            else if(activeSlot == 0)
            {
                activeSlot = maxSlot - 1;
                slotImg[activeSlot].sprite = slotSprite[1];
                slotImg[0].sprite = slotSprite[0];
            }
        }
    }
}