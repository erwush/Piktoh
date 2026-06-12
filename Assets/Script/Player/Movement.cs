 using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public bool canMove;
    Vector2 moveDir;
    Rigidbody2D rb;
    Animator anim;

    int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        moveDir.Normalize();

        // cek arah dominan
        if (moveDir != Vector2.zero)
        {
            if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
            {
                // kiri kanan
                if (moveDir.x > 0)
                    direction = 3; // right
                else
                    direction = 2; // left
            }
            else
            {
                // atas bawah
                if (moveDir.y > 0)
                    direction = 1; // up
                else
                    direction = 0; // down
            }
        }

        anim.SetInteger("Direction", direction);
        anim.SetBool("IsMoving", moveDir != Vector2.zero);
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
}