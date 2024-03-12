using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        Angel,
        Bell,
        PowerUp,
        CPSReduction,
        Multiplier // Add Multiplier collectable type
        // Add more collectable types as needed
    }

    public CollectableType type; // Type of collectable
    public int resourceAmount = 10; // Amount of resources to award when collected
    public int collectableClipIndex; // Index of the collectable audio clip
    public float cpsReductionPercentage = 0.5f; // Percentage by which CPS will be reduced if not clicked in time
    public float multiplierDuration = 10f; // Duration of the multiplier effect
    public float multiplierValue = 1.5f; // Value of the multiplier (e.g., 1.5 for a 50% increase)
    private AudioManager audioManager; // Reference to the AudioManager

    private bool clicked = false; // Flag to track if collectable was clicked

    private void Start()
    {
        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnMouseDown()
    {
        // Set clicked flag to true
        clicked = true;

        // Play collectable audio clip
        audioManager.PlayCollectableClip(collectableClipIndex);

        // Handle different types of collectables
        switch (type)
        {
            case CollectableType.Angel:
                FindObjectOfType<ResourceTracker>().AddResources(resourceAmount);
                break;
            case CollectableType.Bell:
                FindObjectOfType<ResourceTracker>().AddResources(resourceAmount * 2); // Example: Gems award double resources
                break;
            case CollectableType.PowerUp:
                // Implement power-up functionality
                break;
            case CollectableType.CPSReduction:
                // No action needed when clicked
                break;
            case CollectableType.Multiplier:
                // Activate multiplier
                ResourceTracker.Instance.ActivateMultiplier(multiplierDuration, multiplierValue);
                break;
                // Add more cases for additional collectable types
        }

        // Destroy the collectable
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // If the collectible hasn't been clicked
        if (!clicked && type == CollectableType.CPSReduction)
        {
            // Apply the CPS reduction
            ResourceTracker.Instance.ApplyCPSReduction(cpsReductionPercentage);
        }
    }
}
