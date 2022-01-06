using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject creditsUI;

    public void Back()
    {
        menuUI.SetActive(true);
        creditsUI.SetActive(false);
    }
}
