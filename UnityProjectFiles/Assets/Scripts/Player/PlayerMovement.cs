using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Movement movement;
    private void Start() 
    {
        movement = GetComponent<Movement>();
    }
    void Update()
    {
        movement.direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    }
}
