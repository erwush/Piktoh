using UnityEngine;

public class KriperCombat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created    


    public float demeg;
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




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTargetingPlayer)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (plAttr == null)
                {
                    plAttr = collision.gameObject.GetComponent<Player>();

                }
                // demeg = GameUtils.DamageApplier(stat.atk, plAttr.def, stat.dmgType[idx], plAttr.dmgRes[idx], stat.elemDmg[idx], plAttr.elemRes[idx], 0, 0, idx);

                // collision.gameObject.GetComponent<PlayerHealth>().HealthChange(-demeg);

            }
        }
        else if (!isTargetingPlayer)
        {
            if (collision.gameObject.tag == "House")
            {
                if (houseAttr == null)
                {
                    houseAttr = collision.gameObject.GetComponent<Kotak>();
                }
            }
        }

    }

    private void Start()
    {
        targetLayer = layer[0];
        anim = GetComponent<Animator>();
        enemyMovement = GetComponent<KriperMovement>();
    }


    void Update()
    {
    }

    void FinishAttacking()
    {
        if (anim.GetBool("isAttacking") == true)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, stat.atkRange, targetLayer);
        enemyMovement.atkTimer = stat.atkSpd;
        if (isTargetingPlayer)
        {
            if (plAttr == null)
            {
                plAttr = hits[0].gameObject.GetComponent<Player>();

            }
            if (hits.Length > 0)
            {
                plAttr.ChangeHealth(-0.2f);
                Debug.Log("demeg musuh:" + demeg);

            }
        }
        else
        {
            if (houseAttr == null)
            {
                houseAttr = hits[0].gameObject.GetComponent<Kotak>();
            }
            if (hits.Length > 0)
            {
                houseAttr.ChangeHealth(-stat.atk);
                Debug.Log("demeg kotak:" + demeg);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, stat.atkRange);
    }
}
