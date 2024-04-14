using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public float delay = 1.0f; // Delay in seconds
    public Button[] allButtons; // Array to hold all UI buttons

    public ScoreboardManager scoreboardManager; // Reference to the ScoreboardManager
    public TMP_Text[] topTimesText; // Array of TextMeshPro Text components to display the top times

    void Start()
    {
        // Find all UI buttons in the scene
        allButtons = FindObjectsOfType<Button>();

        // Update time entries in the main menu panel
        UpdateTimeEntries();
    }

    // Method to update the time entries in the main menu panel
    public void UpdateTimeEntries()
    {
        if (scoreboardManager != null)
        {
            topTimesText = scoreboardManager.topTimesText;

            // Iterate through the topTimesText array and update the time entries
            for (int i = 0; i < topTimesText.Length; i++)
            {
                // Assuming the time entry is stored as a string in the ScoreboardManager
                // You may need to adjust this based on how time entries are stored in your project
                string timeEntry = GetTimeEntry(i); // Implement this method to retrieve the time entry

                // Update the second TextMeshPro Text component in each set with the time entry
                // Assuming the time entry is the second text component in each set
                topTimesText[i].text = timeEntry;
            }
        }
        else
        {
            Debug.LogError("ScoreboardManager reference not set in the MainMenu script.");
        }
    }

    // Example method to retrieve time entry for a specific rank
    private string GetTimeEntry(int rank)
    {
        // Implement logic to retrieve time entry for the specified rank
        // You may retrieve the time entry from PlayerPrefs or another data source
        // For example:
        // return PlayerPrefs.GetString("TimeEntry_" + rank, "00:00");
        return "00:00"; // Placeholder value
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
