using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float health = 33550336;
    public float maxHealth = 33550336;
    public float energy = 100;
    public float maxEnergy = 100;
    public Image healthBar;
    public Image energyBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        energyBar.fillAmount = energy / maxEnergy;
    }


}
