using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public bool canMove = true;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Animator anim;

    private int direction;

    [Header("Knockback")]
    public float knockbackResistance = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.y = Input.GetAxisRaw("Vertical");

            moveDir.Normalize();

            if (moveDir != Vector2.zero)
            {
                if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
                {
                    direction = moveDir.x > 0 ? 3 : 2;
                }
                else
                {
                    direction = moveDir.y > 0 ? 1 : 0;
                }
            }

            anim.SetInteger("Direction", direction);
            anim.SetBool("IsMoving", moveDir != Vector2.zero);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = moveDir * speed;
        }
    }

    public void StopMove()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void ApplyKnockback(Vector2 attackerPosition, float force, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackCoroutine(attackerPosition, force, duration));
    }

    IEnumerator KnockbackCoroutine(Vector2 attackerPosition, float force, float duration)
    {
        canMove = false;

        Vector2 knockDir =
            ((Vector2)transform.position - attackerPosition).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDir * (force / knockbackResistance), ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        canMove = true;
    }
}