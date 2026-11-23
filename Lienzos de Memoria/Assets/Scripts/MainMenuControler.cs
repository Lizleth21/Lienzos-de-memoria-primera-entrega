//Generate by IA

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string museumSceneName = "SampleScene";

    void Update()
    {
        // WCAG 2.2 Keyboard accessibility: press Enter or Space to start
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(museumSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}