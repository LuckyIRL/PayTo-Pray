using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject[] resourceCollectablePrefabs; // Array of different resource collectable prefabs
    public GameObject[] multiplierPrefabs; // Array of multiplier prefabs
    public GameObject[] cpsReductionCollectablePrefabs; // Array of CPS reduction collectable prefabs
    public float resourceCollectableSpawnInterval = 5f; // Time interval between resource collectable spawns
    public float multiplierSpawnInterval = 10f; // Time interval between multiplier spawns
    public float cpsReductionCollectableSpawnInterval = 15f; // Time interval between CPS reduction collectable spawns
    public float spawnRadius = 5f; // Radius within which collectables will spawn
    public float disappearDelay = 5f; // Time delay before collectables disappear if not clicked

    private void Start()
    {
        // Start spawning resource collectables, multipliers, and CPS reduction collectables
        InvokeRepeating("SpawnResourceCollectable", 0f, resourceCollectableSpawnInterval);
        InvokeRepeating("SpawnMultiplier", 0f, multiplierSpawnInterval);
        InvokeRepeating("SpawnCPSReductionCollectable", 0f, cpsReductionCollectableSpawnInterval);
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
}
