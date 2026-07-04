using UnityEngine;

public class Farming : MonoBehaviour
{
    public Player pleyer;
    public Inpentori inven;
    public GameObject biji;
    public BatangPanas hotbar;
    public SpriteRenderer sprite;
    public Sprite[] spriteImg; //0 = unhoed, 1 = hoed

    public int state; //0 = unhoed, 1 = hoed, 3 = seeded b  
    public GameObject plantObj;
    private bool inArea;
    public Item[] item;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        inven = GameObject.Find("Inpentori").GetComponent<Inpentori>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 2 && biji != null)
        {
            if (biji.GetComponent<Biji>().state == 1)
            {
                biji.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            }
            else if (biji.GetComponent<Biji>().state == 2)
            {
                biji.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            }
        }
        if (inArea && Input.GetKeyDown(KeyCode.F) && hotbar.activeSlot == 3)
        {
            if (state == 0)
            {
                if (Questing.Instance.daftarMisi[1].status == QuestStatus.Active || Questing.Instance.daftarMisi[1].status == QuestStatus.Completed)
                {
                    state = 1;
                    if (Questing.Instance.daftarMisi[1].status == QuestStatus.Active && Questing.Instance.daftarMisi[1].currentAmount == 0) Questing.Instance.LaporkanProgress(1, 1);
                    // pleyer.energy -= 3f;
                    pleyer.GetComponent<Animator>().Play("macul");
                    sprite.sprite = spriteImg[1];
                }
            }
            else if (state == 1)
            {

                // inven.stackCount[plantIdx[0]].text = inven.item[plantIdx[0]].itemCount.ToString();
                biji = Instantiate(plantObj, transform.position, Quaternion.identity);
                state = 2;
                biji.GetComponent<Biji>().tanah = this.gameObject.GetComponent<Farming>();
                if (Questing.Instance.daftarMisi[1].status == QuestStatus.Active && Questing.Instance.daftarMisi[1].currentAmount == 1) Questing.Instance.LaporkanProgress(1, 1);
                biji.GetComponent<Biji>().inven = inven;
                biji.GetComponent<Biji>().hotbar = hotbar;

            }
        }
    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
            pleyer = other.GetComponent<Player>();

            hotbar = other.GetComponent<BatangPanas>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }
}
