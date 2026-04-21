using UnityEngine;

public class KarakterController : MonoBehaviour
{
    public float kecepatan = 5f;
    public int jumlahKayu = 0;
    
    private Rigidbody2D rb;
    private Vector2 pergerakan;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        pergerakan = new Vector2(inputX, inputY).normalized;
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + pergerakan * kecepatan * Time.fixedDeltaTime);
    }
}
