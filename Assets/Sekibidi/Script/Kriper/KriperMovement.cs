using UnityEngine;

public class KriperMovement : MonoBehaviour
{
    public float atkTimer;
    public Kriper stat;
    public Animator anim;
    public int facingDirection = -1;
    private Rigidbody2D rb;
    public Transform player;
    public Transform target;
    public EnemyState enemyState, newState;
    public float timer;
    public float detectRadius = 5;
    public LayerMask targetLayer;
    public LayerMask pLayer;
    public Transform detectPoint;
    public bool isTargetingPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }

    // Update is called once per frame
    void Update()
    {

        if (isTargetingPlayer)
        {
            CheckForPlayer();
        }
        else if (!isTargetingPlayer)
        {
            CheckForTarget();
        }
        if (atkTimer > 0)
        {
            atkTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;

        }


    }

    void Chase()
    {

        if (target.position.x > target.position.x && facingDirection == -1 || target.position.x < target.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * stat.spd;

        // if (timer > 0)
        // {
        //     timer -= Time.deltaTime;
        // }

        // if (timer <= 0)
        // {
        //     rb.linearVelocity = direction * stat.spd;
        // }
        // else
        // {
        //     rb.linearVelocity = direction;
        // }   
    }

    // void FixedUpdate()
    // {
    //     float horizontal = Input.GetAxis("Horizontal");
    //     float vertical = Input.GetAxis("Vertical");
    //     anim.SetFloat("horizontal", Mathf.Abs(horizontal));
    //     anim.SetFloat("vertical", Mathf.Abs(vertical));
    // }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.position, detectRadius * 1000, pLayer);


        if (hits.Length > 0)
        {
            if (player == null)
            {
                ChangeState(EnemyState.Idle);
                player = hits[0].transform;
            }
            if (Vector2.Distance(transform.position, player.position) <= stat.atkRange && atkTimer <= 0)
            {
                
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > stat.atkRange && enemyState != EnemyState.Attacking)
            {
                
                ChangeState(EnemyState.Chasing);
            } else
            {

                rb.linearVelocity = Vector2.zero;
                ChangeState(EnemyState.Idle);
            }
        }
        else
        {

            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);

        }

        // if (collision.CompareTag("Player"))
        // {
        //     if (player == null)
        //     {

        //         player = collision.transform;
        //     }

        // }
    }
    private void CheckForTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.position, detectRadius * 1000, targetLayer);


        if (hits.Length > 0)
        {


            if (target == null)
            {
                ChangeState(EnemyState.Idle);
                target = hits[0].transform;
            }
            if (Vector2.Distance(transform.position, target.position) <= stat.atkRange && atkTimer <= 0)
            {

                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, target.position) > stat.atkRange && enemyState != EnemyState.Attacking)
            {

                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {

            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);

        }

        // if (collision.CompareTag("Player"))
        // {
        //     if (player == null)
        //     {

        //         player = collision.transform;
        //     }

        // }
    }


    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         ChangeState(EnemyState.Idle);
    //         timer = 1f;

    //     }
    // }

    public void ChangeState(EnemyState newState)
    {
        //Exit the current animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        //Update current state
        enemyState = newState;

        //Enter the new animation
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPoint.position, detectRadius);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}
