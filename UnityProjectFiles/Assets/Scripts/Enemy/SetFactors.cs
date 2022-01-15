using UnityEngine;

public class SetFactors : MonoBehaviour
{
    public int mass;

    public Rigidbody2D rb2d;

    void Start()
    {
        mass = Random.Range(1, 7);
        rb2d.mass = mass;
    }
}
