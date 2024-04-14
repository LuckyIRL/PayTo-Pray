using UnityEngine;
using TMPro;

public class LocalScoreboard : MonoBehaviour
{
    public TMP_Text bestTimeText; // Reference to the UI text element displaying the best time
    private long winAmount = 7888000000L; // Win condition amount
    private float bestTime = Mathf.Infinity; // Initially set the best time to infinity
    private bool gameWon = false;
    private float startTime;

    private void Update()
    {
        // Check if the game is won
        if (!gameWon && FindObjectOfType<ResourceTracker>().resourcesAvailable >= winAmount)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameWon = true;

        // Calculate the time taken to win
        float currentTime = Time.time - startTime;

        // Update the best time if the current time is faster
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            bestTimeText.text = FormatTime(currentTime); // Update the UI with the new best time
        }
    }

    private void Start()
    {
        startTime = Time.time; // Record the start time when the game starts
    }

    // Helper method to format time in minutes and seconds
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
