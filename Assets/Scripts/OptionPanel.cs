using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    // Add any variables or methods related to the Options Panel here

    public void EnableInteractions()
    {
        // Enable interaction with the Options Panel
        gameObject.SetActive(true);
    }

    public void DisableInteractions()
    {
        // Disable interaction with the Options Panel
        gameObject.SetActive(false);
    }
}
