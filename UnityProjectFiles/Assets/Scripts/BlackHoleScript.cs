using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject WhiteHolePrefab;
    private GameObject WhiteHole;
    void Start()
    {
        Vector3 position = new Vector3(transform.position.x - 8, 0, 0);
        WhiteHole = Instantiate(WhiteHolePrefab, position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        other.transform.position = WhiteHole.transform.position;
    }
}
