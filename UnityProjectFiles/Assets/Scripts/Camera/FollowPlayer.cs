using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 0, -10);

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
