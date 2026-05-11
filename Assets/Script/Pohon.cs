using UnityEngine;

public class Pohon : MonoBehaviour
{
    public float health = 3;
    public GameObject itemKayu;
    public Item kayu;
    public GameObject pohon;

    public void ChangeHealth(float amount)
    {

        
        health += amount; 
        
   

        if (health <= 0)
        {
      
            Destroy();
        }
    }

    void Destroy()
    {
        GameObject obj = Instantiate(itemKayu, transform.position, Quaternion.identity);
        obj.GetComponent<DroppedItem>().data = kayu;
        Destroy(pohon);
    }
}