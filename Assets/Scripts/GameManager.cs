using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ResourceTracker resourceTracker;
    public GameObject winTextPrefab;
    public Animator winTextAnimator; // Reference to the WinText animator
    public float transitionDelay = 3f; // Delay before transitioning to the end screen
    public int winAmount = 100; // Win condition amount
    public int endSceneIndex; // Index of the end scene

    private bool gameWon = false;


    void Update()
    {
        if (!gameWon && resourceTracker.resourcesAvailable >= winAmount)
        {
            WinGame();
        }


    }

    void WinGame()
    {
        gameWon = true;


        // Pause gameplay
        Time.timeScale = 0f;

        // Display win text and play animation
        DisplayWinText();
    }



    void DisplayWinText()
    {
        // Find the Canvas object in the scene
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            // Instantiate the win text prefab as a child of the Canvas
            GameObject winTextInstance = Instantiate(winTextPrefab, canvas.transform);
            // Get the TMP_Text component of the instantiated win text prefab
            TMP_Text winText = winTextInstance.GetComponent<TMP_Text>();
            if (winText != null)
            {
                // Set the win text content
                winText.text = "You Win!";
                // Set the position of the win text (e.g., center of the screen)
                winText.rectTransform.anchoredPosition = Vector2.zero;
                // Start coroutine to make the text disappear and transition to end screen
                StartCoroutine(HideWinTextAndTransition(winTextInstance));
            }
            else
            {
                Debug.LogError("TMP_Text component not found in the win text prefab.");
            }
        }
        else
        {
            Debug.LogError("Canvas not found in the scene. Cannot display win text.");
        }
    }

    IEnumerator HideWinTextAndTransition(GameObject winTextInstance)
    {
        // Wait for a few seconds
        yield return new WaitForSecondsRealtime(transitionDelay);

        // Destroy the win text object
        Destroy(winTextInstance);

        // Load the end scene
        SceneManager.LoadScene(endSceneIndex);
    }
}
