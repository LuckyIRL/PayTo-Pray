using TMPro;
using UnityEngine;
using System.Collections;

public class ResourceTracker : MonoBehaviour
{
    // Singleton pattern
    public static ResourceTracker Instance { get; private set; }

    public int resourcesAvailable;
    public float autoClicks;
    private float autoClickPool;
    public TMP_Text resourceCounter, clickCounter;

    // Variables for collectable
    private bool cpsReductionCollectableActive = false;
    private float cpsReductionCollectableTimer = 0f;
    private float cpsReductionCollectableActiveTime = 5f; // Time the CPS reduction collectable remains active
    private float cpsReductionPercentage = 0.5f; // Percentage by which CPS will be reduced if not clicked in time

    // Variables for multiplier
    private bool multiplierActive = false;
    private float multiplierValue = 1f;
    private float multiplierDuration = 0f;
    private float multiplierTimer = 0f;

    // Variables for prayBoost
    private bool prayBoostActive = false;
    private float prayBoostMultiplier = 1f;
    private float prayBoostDuration = 0f;
    private float prayBoostTimer = 0f;

    // Penalty collectable variables
    private float penaltyPercentage = 0.5f; // Percentage by which resources will be penalized if not clicked
    private bool penaltyActive = false; // Flag to track if a penalty collectable is active
    private bool penaltyCollectableSpawned = false; // Flag to track if a penalty collectable has spawned but not clicked
    private float penaltyTimer = 0f; // Timer for active penalty collectable
    private float penaltyDuration = 5f; // Duration for which the penalty collectable remains active


    private void Awake()
    {
        // Singleton pattern: Ensure only one instance of ResourceTracker exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Method to apply clicks per second reduction
    public void ApplyCPSReduction(float reductionPercentage)
    {
        autoClicks *= (1f - reductionPercentage); // Reduce autoClicks by the specified percentage
    }

    public void AddResources(int amountToAdd)
    {
        if (prayBoostActive)
        {
            // Apply the pray boost multiplier
            amountToAdd = Mathf.RoundToInt(amountToAdd * prayBoostMultiplier);
        }
        resourcesAvailable += amountToAdd;
    }

    // Method to check if there are enough resources available
    public bool CheckResources(int amount)
    {
        return resourcesAvailable >= amount;
    }

    // Method to remove resources from the available pool
    public void RemoveResources(int amountToRemove)
    {
        resourcesAvailable -= amountToRemove;
    }

    // Method to update the UI
    private void UpdateUI()
    {
        resourceCounter.text = resourcesAvailable.ToString();
        clickCounter.text = (Mathf.Round(autoClicks * 10) / 10).ToString();
    }

    // Method to activate CPS reduction collectable
    public void ActivateCPSReductionCollectable()
    {
        cpsReductionCollectableActive = true;
    }

    // Method to activate multiplier
    public void ActivateMultiplier(float duration, float value)
    {
        multiplierActive = true;
        multiplierDuration = duration;
        multiplierValue = value;
        multiplierTimer = 0f;
        autoClicks *= multiplierValue; // Apply multiplier to autoClicks
    }

    public void ActivatePrayBoost(float duration, float multiplier)
    {
        prayBoostActive = true;
        prayBoostDuration = duration;
        prayBoostMultiplier = multiplier;
        prayBoostTimer = 0f;
    }

    public void ActivatePenaltyCollectable()
    {
        StartCoroutine(PenaltyCollectableRoutine());
        penaltyCollectableSpawned = true; // Mark that a penalty collectable has spawned
    }



    // Coroutine for penalty collectable behavior
    private IEnumerator PenaltyCollectableRoutine()
    {
        
        // Wait for the collectable to disappear if it's not clicked
        yield return new WaitForSeconds(penaltyDuration);

        // Check if the collectable is still active and not clicked
        if (penaltyActive)
        {
            // Apply the penalty
            ApplyResourcePenalty(penaltyPercentage);

            // Deactivate penalty collectable
            penaltyActive = false;
            penaltyTimer = 0f;
        }
    }

    // Method to apply the resource penalty
    private void ApplyResourcePenalty(float penaltyPercentage)
    {
        int penaltyAmount = Mathf.RoundToInt(resourcesAvailable / penaltyPercentage);
        resourcesAvailable -= penaltyAmount;
    }


    private void Update()
    {
        // Update autoClicks based on CPS reduction collectable status and multiplier status
        float currentAutoClicks = autoClicks;
        if (cpsReductionCollectableActive)
        {
            currentAutoClicks *= (1f - cpsReductionPercentage);
        }
        if (multiplierActive)
        {
            multiplierTimer += Time.deltaTime;
            if (multiplierTimer >= multiplierDuration)
            {
                multiplierActive = false;
                autoClicks /= multiplierValue; // Remove multiplier effect from autoClicks
            }
        }

        // Add autoClicks adjusted for framerate
        autoClickPool += currentAutoClicks * Time.deltaTime;

        // Check if the autoClicks have created a new resource (>1)
        if (autoClickPool > 1f)
        {
            // Store the fractional remainder of the autoClickPool
            var fractionalRemainder = autoClickPool % 1;

            // Add the integer amount of autoclicks by removing the fractional component
            AddResources((int)(autoClickPool - fractionalRemainder));

            // Set the autoClick pool to be the fractional remainder
            autoClickPool = fractionalRemainder;
        }

        if (prayBoostActive)
        {
            prayBoostTimer += Time.deltaTime;
            if (prayBoostTimer >= prayBoostDuration)
            {
                prayBoostActive = false;
            }
            else
            {
                currentAutoClicks *= prayBoostMultiplier;
            }
        }

        // Update the UI
        UpdateUI();

        // Check if CPS reduction collectable is active
        if (cpsReductionCollectableActive)
        {
            // Update the timer for active CPS reduction collectable
            cpsReductionCollectableTimer += Time.deltaTime;

            // Check if time limit exceeded
            if (cpsReductionCollectableTimer >= cpsReductionCollectableActiveTime)
            {
                // Deactivate CPS reduction collectable
                cpsReductionCollectableActive = false;
                cpsReductionCollectableTimer = 0f;
            }
        }

        if (penaltyActive && penaltyCollectableSpawned)
        {
            penaltyTimer += Time.deltaTime;

            // Check if time limit exceeded
            if (penaltyTimer >= penaltyDuration)
            {
                // Apply the penalty
                ApplyResourcePenalty(penaltyPercentage);

                // Deactivate penalty collectable
                penaltyActive = false;
                penaltyCollectableSpawned = false; // Reset the flag
                penaltyTimer = 0f;
            }
        }
    }
}
