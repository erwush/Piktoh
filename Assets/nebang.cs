using UnityEngine;

public class Pohon : MonoBehaviour
{
    public float health = 3; 
    public GameObject itemKayu;

    public void ChangeHealth(float amount)
    {
        // Debug warna hijau untuk Pohon
        Debug.Log("<color=#00FF00>Pohon:</color> Menerima Damage: " + amount);
        
        health += amount; 
        
        Debug.Log("<color=#00FF00>Pohon:</color> HP tersisa: " + health);

        if (health <= 0)
        {
            Debug.Log("<color=red>Pohon:</color> Hancur Total!");
            HancurTotal();
        }
    }

    void HancurTotal()
    {
        if (itemKayu != null) Instantiate(itemKayu, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}