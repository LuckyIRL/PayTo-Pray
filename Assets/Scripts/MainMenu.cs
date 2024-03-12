using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float delay = 1.0f; // Delay in seconds
    public Button[] allButtons; // Array to hold all UI buttons

    void Start()
    {
        // Find all UI buttons in the scene
        allButtons = FindObjectsOfType<Button>();
    }

    public void PlayGame()
    {
        // Disable all UI buttons
        foreach (Button button in allButtons)
        {
            button.interactable = false;
        }

        // Start a coroutine to load the scene after the delay
        StartCoroutine(LoadSceneWithDelay());
    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        // Start a coroutine to quit the game after the delay
        StartCoroutine(QuitWithDelay());
    }

    private IEnumerator QuitWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Quit the application
        Application.Quit();
    }
}
