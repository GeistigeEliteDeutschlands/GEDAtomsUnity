using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject settingsUI;

    public void Back()
    {
        menuUI.SetActive(true);
        settingsUI.SetActive(false);
    }
}
