using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject settingsUI;
    public GameObject creditsUI;

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        menuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    public void Credits()
    {
        menuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit ();
    }
}
