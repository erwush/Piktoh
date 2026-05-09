using UnityEngine;

public class Biji : MonoBehaviour
{
    public Farming tanah;
    public float timer;
    public bool canFarmed;
    public bool inArea;
    public Inpentori inven;
    public BatangPanas hotbar;
    public Sprite[] spriteImg;
    public SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        sprite = GetComponent<SpriteRenderer>();
        canFarmed = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1+timer / 720f, 1+timer / 720f, 1+timer/ 720f);
        if (timer < 720f)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 720f)
        {
            canFarmed = true;
            sprite.sprite = spriteImg[1];
            
        }
        // if (timer <= 0)
        // {
        //     tanah.biji--;
        //     Destroy(gameObject);
        // }
        if(inArea && Input.GetKeyDown(KeyCode.E) && canFarmed && hotbar.activeSlot == 3)
        {
            inven.item[tanah.plantIdx[0]].itemCount += 1;
            // inven.stackCount[tanah.plantIdx[0]].text = inven.item[tanah.plantIdx[0]].itemCount.ToString();
            inven.item[tanah.plantIdx[1]].itemCount += 1;
            tanah.state = 1;
            Destroy(gameObject);

            // inven.stackCount[tanah.plantIdx[1]].text = inven.item[tanah.plantIdx[1]].itemCount.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inArea = false;
        }
    }
}
