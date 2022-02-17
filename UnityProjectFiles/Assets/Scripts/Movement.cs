using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 direction;
    public int speed;
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {
        rb.velocity = direction.normalized * speed;
    }
}
