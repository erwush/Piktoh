using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 33550336;
    public float maxHealth = 33550336;
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void ChangeHealth(float value)
    {
        health += value;
    }
}
