using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    public int raysToBeShoot;
    public float range;
    public uint myMass;
    private Movement movement;

    private void Start() 
    {
        movement = GetComponent<Movement>();
    }
    private void FixedUpdate()
    {
        float angle = 0;
        for (int i = 0; i < raysToBeShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += (360 / raysToBeShoot) * Mathf.PI / raysToBeShoot;
            Vector3 dir = new Vector3(x, y, 0);
            Debug.DrawRay(transform.position + dir, dir * 2, Color.green);
            GetComponent<CircleCollider2D> ().enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir);
            GetComponent<CircleCollider2D> ().enabled = true;
            if (hit.collider != null && hit.distance < range)
            {
                Vector3 direction = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, hit.transform.position.z - transform.position.z);
                if(hit.transform.GetComponent<Nuclide>().nuclideData.A < myMass)
                {
                    movement.direction = direction;
                }
                else if(hit.transform.GetComponent<Nuclide>().nuclideData.A > myMass)
                {
                    movement.direction = direction * -1;
                }
            }
        }
    }
}
