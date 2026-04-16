using System;
using UnityEngine;
using System.Collections;

public class Kriper : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float[] detectRange;
    public Rigidbody2D rb;
    public GameObject itemDrop;
    public Material hitFlash;
    public Transform detectPoint;
    public Transform target;
    private SpriteRenderer selfSprite;
    public LayerMask pLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (amount < 0)
        {
            StartCoroutine(HurtAnim());
        }
        if (health > maxHealth)
        { health = maxHealth; }
        if (health <= 0)
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public IEnumerator HurtAnim()
    {
        Material defaultMaterial = selfSprite.material;
        selfSprite.material = hitFlash;
        yield return new WaitForSeconds(0.2f);
        selfSprite.material = defaultMaterial;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-0.2f);

        }
    }

    // private void CheckForPlayer()
    // {
    //     Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.position, detectRange[0], pLayer);



    //     if (hits.Length > 0)
    //     {
    //         target = hits[0].transform;
    //         if (Vector2.Distance(transform.position, player.position) <= stat.atkRange && atkTimer <= 0)
    //         {

    //             ChangeState(EnemyState.Attacking);
    //         }
    //         else if (Vector2.Distance(transform.position, player.position) > stat.atkRange && enemyState != EnemyState.Attacking)
    //         {
    //             ChangeState(EnemyState.Chasing);
    //         }
    //     }
    //     else
    //     {
    //         rb.linearVelocity = Vector2.zero;
    //         ChangeState(EnemyState.Idle);

    //     }

    //     // if (collision.CompareTag("Player"))
    //     // {
    //     //     if (player == null)
    //     //     {

    //     //         player = collision.transform;
    //     //     }

    //     // }
    // }
}
