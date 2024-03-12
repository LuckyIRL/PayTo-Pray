using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject[] resourceCollectablePrefabs; // Array of different resource collectable prefabs
    public GameObject[] multiplierPrefabs; // Array of multiplier prefabs
    public GameObject[] cpsReductionCollectablePrefabs; // Array of CPS reduction collectable prefabs
    public GameObject[] resourceCollectableMultiplier;
    public float resourceCollectableSpawnInterval = 5f; // Time interval between resource collectable spawns
    public float multiplierSpawnInterval = 10f; // Time interval between multiplier spawns
    public float resourceCollectableMultiplierInterval = 5f;
    public float cpsReductionCollectableSpawnInterval = 15f; // Time interval between CPS reduction collectable spawns
    public float spawnRadius = 5f; // Radius within which collectables will spawn
    public float disappearDelay = 5f; // Time delay before collectables disappear if not clicked
    public float initialDelay = 10f; // Initial delay before collectables start spawning
    public int collectableAppearClipIndex;
    private AudioManager audioManager; // Reference to the AudioManager


    private void Start()
    {
        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Invoke method to start spawning collectables after the initial delay
        Invoke("StartSpawning", initialDelay);
    }

    private void StartSpawning()
    {
        // Start spawning resource collectables after the initial delay
        InvokeRepeating("SpawnResourceCollectable", initialDelay, resourceCollectableSpawnInterval);

        // Start spawning multipliers after an additional delay
        InvokeRepeating("SpawnMultiplier", initialDelay + multiplierSpawnInterval, multiplierSpawnInterval);

        // Start spawning CPS reduction collectables after another additional delay
        InvokeRepeating("SpawnCPSReductionCollectable", initialDelay + cpsReductionCollectableSpawnInterval, cpsReductionCollectableSpawnInterval);

        // Start spawning resource multiplier collectables after yet another additional delay
        InvokeRepeating("SpawnResourceCollectableMultiplier", initialDelay + resourceCollectableMultiplierInterval, resourceCollectableSpawnInterval);
    }


    private void SpawnResourceCollectable()
    {
        // Randomly select a resource collectable prefab
        GameObject collectablePrefab = resourceCollectablePrefabs[Random.Range(0, resourceCollectablePrefabs.Length)];

        // Calculate random position within spawn radius
        Vector2 randomPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;

        // Instantiate resource collectable at random position
        GameObject collectableInstance = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);

        // Start coroutine to make collectable disappear after a delay
        StartCoroutine(DisappearAfterDelay(collectableInstance));
    }

    private void SpawnMultiplier()
    {
        // Randomly select a multiplier prefab
        GameObject multiplierPrefab = multiplierPrefabs[Random.Range(0, multiplierPrefabs.Length)];

        // Calculate random position within spawn radius
        Vector2 randomPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;

        // Instantiate multiplier at random position
        GameObject multiplierInstance = Instantiate(multiplierPrefab, randomPosition, Quaternion.identity);

        // Set random color for the multiplier
        SetRandomColor(multiplierInstance);

        // Start coroutine to make multiplier disappear after a delay
        StartCoroutine(DisappearAfterDelay(multiplierInstance));
    }



    private void SpawnCPSReductionCollectable()
    {

        // Randomly select a CPS reduction collectable prefab
        GameObject collectablePrefab = cpsReductionCollectablePrefabs[Random.Range(0, cpsReductionCollectablePrefabs.Length)];

        // Calculate random position within spawn radius
        Vector2 randomPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;

        // Instantiate CPS reduction collectable at random position
        GameObject collectableInstance = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);

        // Play sound effect for CPS reduction collectable
        audioManager.PlayCollectableAppearClip(); // Assuming you have a method to play the sound effect in AudioManager

        // Start coroutine to make collectable disappear after a delay
        StartCoroutine(DisappearAfterDelay(collectableInstance));
    }


    private void SpawnResourceCollectableMultiplier()
    {
        // Randomly select a resource collectable prefab
        GameObject collectablePrefab = resourceCollectableMultiplier[Random.Range(0, resourceCollectableMultiplier.Length)];

        // Calculate random position within spawn radius
        Vector2 randomPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;

        // Instantiate resource collectable at random position
        GameObject collectableInstance = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);

        // Start coroutine to make collectable disappear after a delay
        StartCoroutine(DisappearAfterDelay(collectableInstance));
    }

    IEnumerator DisappearAfterDelay(GameObject obj)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(disappearDelay);

        // Destroy the object if it still exists
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    // Method to set a random color for the multiplier
    private void SetRandomColor(GameObject multiplierInstance)
    {
        SpriteRenderer spriteRenderer = multiplierInstance.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Random.ColorHSV();
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on Multiplier prefab.");
        }
    }


}
