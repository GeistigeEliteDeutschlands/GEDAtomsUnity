using UnityEngine;

public class RaycastScriptTest : MonoBehaviour
{
    void Start()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(2, 0, 0), Vector2.right);
        Debug.Log(hit.transform.gameObject.GetComponent<NPCNuclide>().nuclideData);
    }
}
