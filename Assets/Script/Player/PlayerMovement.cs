using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float spd;
    private Rigidbody2D rb;
    [SerializeField]private InputActionReference input; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 direct = input.action.ReadValue<Vector2>();
        rb.linearVelocity = direct * spd;
    }
}
