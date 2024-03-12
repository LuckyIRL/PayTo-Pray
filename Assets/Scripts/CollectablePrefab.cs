using System.Collections;
using UnityEngine;

public class CollectablePrefab : MonoBehaviour
{
    public float cpsReductionPercentage = 0.5f; // The percentage by which CPS will be reduced if not clicked in time
    public float clickWindowDuration = 5f; // The duration within which the collectible must be clicked

    private bool clicked = false;

    private void Start()
    {
        // Start a timer for the click window duration
        StartCoroutine(StartClickWindowTimer());
    }

    private void OnMouseDown()
    {
        // Set clicked flag to true
        clicked = true;

        // Apply any effects for clicking the collectible
        // For example, increase score, apply multiplier, etc.

        // Destroy collectible
        Destroy(gameObject);
    }

    private IEnumerator StartClickWindowTimer()
    {
        yield return new WaitForSeconds(clickWindowDuration);

        // If the collectible hasn't been clicked within the click window duration
        if (!clicked)
        {
            // Apply the click penalty
            ResourceTracker.Instance.ApplyCPSReduction(cpsReductionPercentage);
        }

        // Destroy collectible
        Destroy(gameObject);
    }
}
