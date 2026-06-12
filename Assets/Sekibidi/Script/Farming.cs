using UnityEngine;

public class Farming : MonoBehaviour
{
    public Player pleyer;
    public Inpentori inven;
    public BatangPanas hotbar;
    public SpriteRenderer sprite;
    public Sprite[] spriteImg; //0 = unhoed, 1 = hoed
    public int state; //0 = unhoed, 1 = hoed, 3 = seeded b  
    public GameObject plantObj;
    private bool inArea;
    public int[] plantIdx; //0 = seed idx, 1 = food idx


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        plantIdx[0] = -1;
        plantIdx[1] = -1;
        inven = GameObject.Find("Inpentori").GetComponent<Inpentori>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E) && hotbar.activeSlot == 3)
        {
            if (state == 0)
            {
                state = 1;
                pleyer.energy -= 3f;
                pleyer.GetComponent<Animator>().Play("macul");
                sprite.sprite = spriteImg[1];
            }
            else if (state == 1)
            {
                if (plantIdx[0] == -1 && plantIdx[1] == -1)
                {
                    for (int i = 0; i < inven.item.Count; i++)
                    {
                        if (plantIdx[0] == -1 && inven.item[i].isPlant)
                        {
                            plantIdx[0] = i;
                            break;
                        }
                    }
                    for (int i = 0; i < inven.item.Count; i++)
                    {
                        if (plantIdx[1] == -1 && inven.item[i].codeName == "Umbikibidi")
                        {
                            plantIdx[1] = i;
                            break;
                        }
                    }
                }
                if (plantIdx[0] != -1 && plantIdx[1] != -1)
                {
                    inven.item[plantIdx[0]].itemCount -= 1;
                    // inven.stackCount[plantIdx[0]].text = inven.item[plantIdx[0]].itemCount.ToString();
                    GameObject biji = Instantiate(plantObj, transform.position, Quaternion.identity);
                    state = 2;
                    biji.GetComponent<Biji>().tanah = this.gameObject.GetComponent<Farming>();
                    biji.GetComponent<Biji>().inven = inven;
                    biji.GetComponent<Biji>().hotbar = hotbar;
                }
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
