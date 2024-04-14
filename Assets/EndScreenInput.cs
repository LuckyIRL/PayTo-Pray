using UnityEngine;
using TMPro;

public class EndScreenInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public ScoreboardManager scoreboardManager;

    public void SubmitName()
    {
        string playerName = inputField.text;

        // Update the scoreboard with the player's name
        scoreboardManager.UpdateScoreboard(playerName);

        // Hide the input field
        inputField.gameObject.SetActive(false);
    }
}
