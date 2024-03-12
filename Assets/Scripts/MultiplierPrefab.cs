using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplierPrefab : MonoBehaviour
{
    public static MultiplierPrefab Instance; // Singleton instance

    public List<Multiplier> activeMultipliers = new(); // List of active multipliers
    private PolygonCollider2D polygonCollider; // Reference to the PolygonCollider2D component
    public AudioManager audioManager; // Reference to the AudioManager
    public int multiplierClipIndex; // Index of the upgrade clip to play
    public Transform targetPanel; // Reference to the panel where multipliers will be spawned

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Get the PolygonCollider2D component
        polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            // If the PolygonCollider2D component is missing, log a warning
            Debug.LogWarning("PolygonCollider2D component not found on MultiplierPrefab.");
        }
    }

    // Method to handle mouse clicks on the Multiplier prefab
    private void OnMouseDown()
    {
        // Check if the PolygonCollider2D component is present
        if (polygonCollider != null)
        {
            // Play the selected SFX when the item is purchased
            audioManager.PlayMultiplierClip(multiplierClipIndex);
            // You can add your logic here to handle mouse clicks on the Multiplier prefab
            Debug.Log("Multiplier prefab clicked!");

            // Trigger the multiplier effect when clicked
            ApplyMultiplierEffect();

            // Destroy the multiplier object after being clicked
            Destroy(gameObject);
        }
    }

    // Method to activate a multiplier
    public void ActivateMultiplier(Multiplier multiplier)
    {
        activeMultipliers.Add(multiplier);
        StartCoroutine(CountdownMultiplier(multiplier));
    }

    // Coroutine to countdown the duration of a multiplier
    private IEnumerator CountdownMultiplier(Multiplier multiplier)
    {
        while (multiplier.duration > 0)
        {
            yield return new WaitForSeconds(1f);
            multiplier.duration--;
        }
        DeactivateMultiplier(multiplier);
    }

    // Method to deactivate a multiplier
    public void DeactivateMultiplier(Multiplier multiplier)
    {
        activeMultipliers.Remove(multiplier);
        // Remove UI element for the multiplier
    }

    // Method to apply the multiplier effect
    private void ApplyMultiplierEffect()
    {
        // Iterate through active multipliers and apply their effects
        foreach (Multiplier multiplier in activeMultipliers)
        {
            // Implement logic here to apply multiplier effects to the game
            // For example, you can call methods in other scripts to apply the effects
            activeMultipliers.Add(multiplier);

            // Start coroutine to countdown the duration of the multiplier
            StartCoroutine(CountdownMultiplier(multiplier));
            // based on the type and value of the multiplier.
        }
    }

    // Method to spawn the multiplier prefab in the target panel
    public void SpawnMultiplierInPanel()
    {
        if (targetPanel != null)
        {
            // Instantiate the multiplier prefab as a child of the target panel
            Instantiate(gameObject, targetPanel);
        }
        else
        {
            Debug.LogWarning("Target panel not assigned for spawning the multiplier prefab.");
        }
    }
}

[System.Serializable]
public class Multiplier
{
    public enum Type { ResourceProduction, ClickingPower } // Type of multiplier
    public Type type;
    public float value; // Value of the multiplier (e.g., 1.5 for a 50% increase)
    public int duration; // Duration of the multiplier in seconds

    public Multiplier(Type type, float value, int duration)
    {
        this.type = type;
        this.value = value;
        this.duration = duration;
    }
}