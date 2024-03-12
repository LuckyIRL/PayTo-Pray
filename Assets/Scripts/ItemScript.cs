using UnityEngine;
using TMPro;
using System.Collections;

public class Item : MonoBehaviour
{
    public string itemName = "Name me?";
    public int numberOwned = 0;
    public int baseCost = 10;
    public int purchaseCost;
    public TMP_Text costText, nameText, numberText;
    public ResourceTracker myResources;
    public float autoClickIncrease = 0.1f;
    public AudioManager audioManager; // Reference to the AudioManager
    public int upgradeClipIndex; // Index of the upgrade clip to play
    public GameObject spritePrefab; // The sprite prefab to spawn
    public Canvas prefabCanvas; // Reference to the canvas for spawning prefabs
    public RectTransform panelBounds; // The RectTransform of the panel where spawning is confined
    public GameObject temporaryPrefab; // Reference to the temporary prefab to spawn
    public float displayDuration = 2f; // Duration to display the temporary prefab

    private void Start()
    {
        SetItemUI();
    }

    public void SetItemUI()
    {
        purchaseCost = Mathf.CeilToInt(baseCost * Mathf.Pow(1.15f, numberOwned));
        gameObject.name = itemName;
        costText.text = purchaseCost.ToString();
        nameText.text = itemName;
        numberText.text = numberOwned.ToString();
    }

    public void PurchaseItem()
    {
        if (myResources.CheckResources(purchaseCost))
        {
            myResources.RemoveResources(purchaseCost);
            myResources.autoClicks += autoClickIncrease; // Increase auto-clicks per second
            numberOwned++;
            SetItemUI();

            // Play the selected SFX when the item is purchased
            audioManager.PlayUpgradeClip(upgradeClipIndex);

            // Spawn the sprite prefab inside the panel bounds
            if (spritePrefab != null && panelBounds != null)
            {
                // Get the boundaries of the panel
                Vector3 panelMin = panelBounds.rect.min;
                Vector3 panelMax = panelBounds.rect.max;

                // Calculate random spawn position within panel boundaries
                float spawnX = Random.Range(panelMin.x, panelMax.x);
                float spawnY = Random.Range(panelMin.y, panelMax.y);
                Vector3 spawnPosition = panelBounds.TransformPoint(new Vector3(spawnX, spawnY, 0f));

                // Instantiate the prefab at the calculated spawn position
                GameObject newSprite = Instantiate(spritePrefab, panelBounds.transform);
                newSprite.transform.position = spawnPosition;
            }

            // Spawn the temporary prefab
            if (temporaryPrefab != null && prefabCanvas != null)
            {
                // Instantiate the temporary prefab on the canvas
                GameObject tempPrefabInstance = Instantiate(temporaryPrefab, prefabCanvas.transform);

                // Start coroutine to destroy the temporary prefab after a few seconds
                StartCoroutine(DestroyTemporaryPrefab(tempPrefabInstance));
            }
        }
    }

    // Coroutine to destroy the temporary prefab after a few seconds
    private IEnumerator DestroyTemporaryPrefab(GameObject tempPrefab)
    {
        yield return new WaitForSeconds(displayDuration);

        // Destroy the temporary prefab
        Destroy(tempPrefab);
    }
}
