using System.Collections;
using UnityEngine;

public class Pohon : MonoBehaviour
{
    public GameObject qte;
    public GameObject obj;
    public AudioSource sfx;
    public int brokenWood;
    private bool canQte;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        qte.SetActive(false);
        brokenWood = 0;
        canQte = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (brokenWood == 2)
        {
            qte.SetActive(true);
            canQte = true;
        }
        if (canQte)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Sound());
            }
        }

    }

     IEnumerator Sound()
    {
        sfx.Play();
        yield return new WaitForSeconds(0.75f);
        Instantiate(obj);

        Destroy(gameObject);
    }
}
