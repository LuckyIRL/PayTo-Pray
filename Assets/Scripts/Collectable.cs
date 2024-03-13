using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        Angel,
        Bell,
        PowerUp,
        CPSReduction,
        Multiplier,
        PrayBoost,
        Penalty // Add Penalty collectable type
        // Add more collectable types as needed
    }

    public CollectableType type; // Type of collectable
    public int resourceAmount = 10; // Amount of resources to award when collected
    public int collectableClipIndex; // Index of the collectable audio clip
    public float cpsReductionPercentage = 0.5f; // Percentage by which CPS will be reduced if not clicked in time
    public float prayBoostMultiplier = 0.5f; // Prayerboost increase
    public float prayBoostDuration = 10f;
    public float resourceMultiplierValue = 2f; // Multiplier value for resource amount
    public float multiplierDuration = 10f; // Duration of the multiplier effect
    public float multiplierValue = 1.5f; // Value of the multiplier (e.g., 1.5 for a 50% increase)
    private AudioManager audioManager; // Reference to the AudioManager

    private bool clicked = false; // Flag to track if collectable was clicked
    public bool playOnAppearance = false; // Flag to control whether the sound should be played upon appearance

    private void Start()
    {
        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Check if the collectable should play its sound upon appearance
        if (playOnAppearance)
        {
            // Play collectable audio clip
            audioManager.PlayCollectableClip(collectableClipIndex);
        }
    }

    private void OnMouseDown()
    {
        // Set clicked flag to true
        clicked = true;

        // Play collectable audio clip if it shouldn't be played on appearance
        if (!playOnAppearance)
        {
            audioManager.PlayCollectableClip(collectableClipIndex);
        }

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
            case CollectableType.PrayBoost:
                // Apply resource multiplier effect to resourceAmount
                FindObjectOfType<ResourceTracker>().ActivatePrayBoost(prayBoostDuration, prayBoostMultiplier);
                break;
            case CollectableType.Penalty:
                // Halve resource amount
                break;
                // Add more cases for additional collectable types
        }

        // Destroy the collectable
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!clicked)
        {
            switch (type)
            {
                case CollectableType.CPSReduction:
                    ResourceTracker.Instance.ApplyCPSReduction(cpsReductionPercentage);
                    break;
                case CollectableType.Penalty:
                    ResourceTracker.Instance.ApplyResourcePenalty(); // Update this line
                    break;
            }
        }
    }

}


