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
    public int state;
    public Player pleyer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pleyer=  GameObject.FindWithTag("Player").GetComponent<Player>();
        timer = 0f;
        sprite = GetComponent<SpriteRenderer>();
        canFarmed = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1+timer / 20f, 1+timer / 20f, 1+timer/ 20f);
        if (timer < 20f)
        {
            timer += Time.deltaTime;
        }
        if (timer <= 10f)
        {
            state = 0;
            sprite.sprite = spriteImg[0];
            canFarmed = false;
            
        } else if(timer > 10f && timer < 20f)
        {
            state = 1;
            sprite.sprite = spriteImg[1];
        } else
        {
            state = 2;
            canFarmed = true;
            sprite.sprite = spriteImg[2];
        }
        // if (timer <= 0)
        // {
        //     tanah.biji--;
        //     Destroy(gameObject);
        // }
        if (inArea && Input.GetKeyDown(KeyCode.F) && canFarmed && hotbar.activeSlot == 3)
        {
            inven.AddItem(tanah.item[0], 1);
            // inven.stackCount[tanah.plantIdx[0]].text = inven.item[tanah.plantIdx[0]].itemCount.ToString();
            inven.AddItem(tanah.item[1], 2);
            tanah.state = 1;
            tanah.biji = null;
            Destroy(gameObject);

            // inven.stackCount[tanah.plantIdx[1]].text = inven.item[tanah.plantIdx[1]].itemCount.ToString();
        }
        
        if(inArea && Input.GetKeyDown(KeyCode.F) && !canFarmed){
            if(pleyer.sumur > 0)
            {
                pleyer.sumur--;
                timer = 20f;
                canFarmed = true;
            }
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
