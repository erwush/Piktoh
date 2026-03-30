using UnityEngine;

public class BatangPanas : MonoBehaviour
{
    public int[] slot;
    public int activeSlot;
    public int maxSlot;
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
            }
        }
    }
}
