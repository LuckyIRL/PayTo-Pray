using UnityEngine;

public class UpgradeSpriteSpawner : MonoBehaviour
{
    public GameObject spritePrefab; // Reference to the sprite prefab to be spawned
    public RectTransform prefabPanelRectTransform; // Reference to the PrefabPanel's RectTransform

    // Method to handle upgrade button click event
    public void OnUpgradeButtonClicked()
    {
        if (spritePrefab != null && prefabPanelRectTransform != null)
        {
            // Calculate the boundaries of the PrefabPanel
            Vector3[] corners = new Vector3[4];
            prefabPanelRectTransform.GetWorldCorners(corners);
            Vector3 bottomLeft = corners[0];
            Vector3 topRight = corners[2];

            // Generate a random position within the boundaries of the PrefabPanel
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector3 spawnPosition = new(randomX, randomY, 0f);

            // Instantiate a new sprite prefab at the calculated position
            Instantiate(spritePrefab, spawnPosition, Quaternion.identity, prefabPanelRectTransform);
        }
        else
        {
            Debug.LogWarning("Sprite prefab or prefab panel is not set.");
        }
    }
}
