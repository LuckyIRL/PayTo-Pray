// GameManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public ScoreboardManager scoreboardManager;
    public ResourceTracker resourceTracker;
    public float transitionDelay = 3f;
    public long winAmount = 7888000000L;
    public int endSceneIndex;

    private bool gameWon = false;

    private void Start()
    {
        instance = this; // Initialize the instance reference
    }

    private void Update()
    {
        // Check if the game is not yet won and the win condition is met
        if (!gameWon && resourceTracker.resourcesAvailable >= winAmount)
        {
            // Calculate the elapsed ti

            // Call the method to handle winning the game
            WinGame();
        }
    }

    private void WinGame()
    {
        gameWon = true; // Set the game state to won

        Time.timeScale = 0f; // Pause the gameplay

        // Start the coroutine to transition to the end scene after the delay
        StartCoroutine(TransitionToEndScene());
    }



    private IEnumerator TransitionToEndScene()
    {
        // Wait for the specified delay
        yield return new WaitForSecondsRealtime(transitionDelay);

        // Load the end scene
        SceneManager.LoadScene(endSceneIndex);
    }
}
