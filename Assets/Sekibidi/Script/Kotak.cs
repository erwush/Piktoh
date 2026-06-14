using UnityEngine;

public class Kotak : MonoBehaviour
{
    public float health = 100;

    public bool isAttacked;

    private float attackedTimer;
    private float tickTimer;

    void Update()
    {
        if (isAttacked)
        {
            attackedTimer -= Time.deltaTime;

            tickTimer += Time.deltaTime;

            if (tickTimer >= 1f)
            {
                tickTimer = 0f;
                health -= 1;
            }

            if (attackedTimer <= 0)
            {
                isAttacked = false;
                tickTimer = 0f;
            }
        }
        else
        {
            tickTimer += Time.deltaTime;

            if (tickTimer >= 1f)
            {
                tickTimer = 0f;
                health += 5;

                if (health > 100)
                    health = 100;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHealth(float amount)
    {
        isAttacked = true;
        attackedTimer = 5f;
    }
}