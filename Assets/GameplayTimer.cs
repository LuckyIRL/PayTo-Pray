using UnityEngine;
using TMPro;

public class GameplayTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; // Reference to the TextMeshPro Text object for displaying the timer

    private float startTime;
    private bool gameWon = false;

    private void Start()
    {
        startTime = Time.time; // Record the start time
    }

    private void Update()
    {
        if (!gameWon)
        {
            // Calculate elapsed time since the game started
            float elapsedTime = Time.time - startTime;

            // Update UI element to display the elapsed time
            UpdateUITime(elapsedTime);

            // Check if the win condition is met
            if (IsWinConditionMet(elapsedTime))
            {
                // Set the gameWon flag to true to prevent further updates
                gameWon = true;
            }
        }
    }

    private void UpdateUITime(float timeInSeconds)
    {
        // Convert time to a readable format (e.g., minutes:seconds)
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        int hundredths = Mathf.FloorToInt((timeInSeconds * 100) % 100);

        // Format the time including hundredths of a second
        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);

        // Update the UI element with the formatted time
        if (timerText != null)
        {
            timerText.text = formattedTime;
        }
    }

    private bool IsWinConditionMet(float elapsedTime)
    {
        // Check if the elapsed time meets the win condition (elapsedTime >= winAmount)
        return elapsedTime >= GameManager.Instance.winAmount;
    }

    public float GetElapsedTime()
    {
        // Return the elapsed time
        return Time.time - startTime;
    }
}
