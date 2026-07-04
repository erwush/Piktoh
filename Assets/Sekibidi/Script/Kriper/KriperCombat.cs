using UnityEngine;

public class KriperCombat : MonoBehaviour
{
    public float demeg;

    [Header("Knockback")]
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    public Animator anim;
    private int idx;

    public Kriper stat;
    public KriperMovement enemyMovement;

    public Player health;
    public Player plAttr;
    private Kotak houseAttr;

    public Transform attackPoint;

    public LayerMask targetLayer;
    public LayerMask[] layer;

    public bool isTargetingPlayer;

    private void Start()
    {
        targetLayer = layer[0];
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<KriperMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTargetingPlayer)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (plAttr == null)
                {
                    plAttr = collision.gameObject.GetComponent<Player>();
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("House"))
            {
                if (houseAttr == null)
                {
                    houseAttr = collision.gameObject.GetComponent<Kotak>();
                }
            }
        }
    }

    void Update()
    {

    }

    void FinishAttacking()
    {
        if (anim.GetBool("isAttacking"))
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            stat.atkRange,
            targetLayer
        );

        enemyMovement.atkTimer = stat.atkSpd;

        if (hits.Length <= 0)
            return;

        if (isTargetingPlayer)
        {
            Player player = hits[0].GetComponent<Player>();

            if (player != null)
            {
                player.ChangeHealth(-0.05f);

                Movement playerMovement = player.GetComponent<Movement>();

                if (playerMovement != null)
                {
                    playerMovement.ApplyKnockback(
                        transform.position,
                        knockbackForce,
                        knockbackDuration
                    );
                }
            }
        }
        else
        {
            Kotak house = hits[0].GetComponent<Kotak>();

            if (house != null)
            {
                house.ChangeHealth(-stat.atk);
                Debug.Log("demeg kotak: " + stat.atk);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(
                attackPoint.position,
                stat != null ? stat.atkRange : 1f
            );
        }
    }
}