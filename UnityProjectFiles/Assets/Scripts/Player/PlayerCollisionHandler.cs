using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public int thisAtomsMass = 3;

    public SpriteRenderer spriteRenderer;
    public PlayerMovement playerMovement;
    public Rigidbody2D rb2d;

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            if(collisionInfo.gameObject.GetComponent<SetFactors>().mass > thisAtomsMass)
            {
                spriteRenderer.enabled = false;
                playerMovement.enabled = false;
                rb2d.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                Destroy(collisionInfo.gameObject);
            }
        }
    }
}
