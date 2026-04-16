using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public Item data;
    public SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = data.itemSprite;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Inpentori inpen = collision.gameObject.GetComponent<Inpentori>();
            for (int i = 0; i < inpen.slot.Length; i++)
            {
                if (inpen.item[i] == data)
                {
                    inpen.item[i].itemCount++;
                }
            }
            Destroy(gameObject);
        }
    }
}
