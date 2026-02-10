using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    public InputActionReference input;
    public bool canMove;
    public float spd;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove){
            Vector2 direction = input.action.ReadValue<Vector2>();
        rb.linearVelocity = direction * spd;
        }

    }
}
