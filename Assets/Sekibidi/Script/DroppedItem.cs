using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public Item data;
    public SpriteRenderer sprite;
    public Inpentori inpen;

    void Start()
    {
        inpen = GameObject.Find("Inpentori").GetComponent<Inpentori>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = data.itemSprite;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            for (int i = 0; i < inpen.slot.Length; i++)
            {
                if (inpen.item[i] == data)
                {
                    inpen.item[i].itemCount++;
                    inpen.stackCount[i].text = inpen.item[i].itemCount.ToString();
                }
            }
            Destroy(gameObject);
        }
    }
}
