using UnityEngine;

using UnityEngine.UI;
public class healthTest : MonoBehaviour
{
    public Player plr;
    float amount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amount = plr.maxHealth * 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        plr.ChangeHealth(-amount);
        
    }
}
