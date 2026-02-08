using UnityEngine;

public class Block : MonoBehaviour
{
    public Animator anim;
    public float progress;
    public float breakTime;
    public Pohon pohon;
    public AudioSource sfx;
    public bool isPlaying;
    [SerializeField] private bool inArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetButton("Mine"))
        {
            anim.SetBool("isBreaking", true);
            progress += Time.deltaTime;
            if (!isPlaying)
            {
                sfx.Play();
                isPlaying = true;
            }
        }
        else
        {
            progress = 0f;
            anim.SetBool("isBreaking", false);
            if (isPlaying)
            {
                sfx.Stop();
                isPlaying = false;
            }
        }

        if(progress >= breakTime)
        {
            pohon.brokenWood += 1;
            sfx.Stop();
            Destroy(gameObject);
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
