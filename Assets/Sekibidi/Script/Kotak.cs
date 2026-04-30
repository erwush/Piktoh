using UnityEngine;

public class Kotak : MonoBehaviour
{
    public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeHealth(float amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
