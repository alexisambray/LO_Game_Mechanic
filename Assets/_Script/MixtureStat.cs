using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtureStat : MonoBehaviour
{
    // Criteria
    public static Dictionary<string, SharedStats> sharedStatsDict = new Dictionary<string, SharedStats>();
    private string mixtureKey;

    // Appearance
    public bool TrueSolution { get; set; }
    public bool Suspension { get; set; }
    public bool Colloid { get; set; }

    // Uses
    public bool FoodAndBeverage { get; set; }
    public bool Medicine { get; set; }
    public bool Cosmetic { get; set; }
    public bool Cleaning { get; set; }
    public bool Hygiene { get; set; }
    public bool Agriculture { get; set; }

    public int ManualIndex = -1;

    private void Awake()
    {
        InitializeDefaultValues();
        InitializeStat();

        mixtureKey = GenerateKey();

        if (!sharedStatsDict.ContainsKey(mixtureKey))
        {
            sharedStatsDict[mixtureKey] = new SharedStats();
        }

        Debug.Log($"MixtureStat Awake: mixtureKey: {mixtureKey}");
    }

    private void InitializeDefaultValues()
    {
        // Initialize all variables with default values
        TrueSolution = false;
        Suspension = false;
        Colloid = false;

        FoodAndBeverage = false;
        Medicine = false;
        Cosmetic = false;
        Cleaning = false;
        Hygiene = false;
        Agriculture = false;
    }

    public string GenerateKey()
    {
        Debug.Log($"Mixture properties - trueSolution: {TrueSolution}, suspension: {Suspension}, colloid: {Colloid}");
        Debug.Log($"Mixture properties - foodAndBeverage: {FoodAndBeverage}, medicine: {Medicine}, cosmetic: {Cosmetic}");

        return $"{TrueSolution}_{Suspension}_{Colloid}_{FoodAndBeverage}_{Medicine}_{Cosmetic}_{Cleaning}_{Hygiene}_{Agriculture}";
    }

    // Accessor for the shared itemFound variable
    public bool ItemFound
    {
        get
        {
            Debug.Log($"Accessing ItemFound for mixtureKey: {mixtureKey}");
            return sharedStatsDict.ContainsKey(mixtureKey) && sharedStatsDict[mixtureKey].ItemFound;
        }
        set
        {
            if (sharedStatsDict.ContainsKey(mixtureKey))
            {
                sharedStatsDict[mixtureKey].ItemFound = value;
                Debug.Log($"Setting ItemFound to {value} for mixtureKey: {mixtureKey}");
            }
        }
    }

    public void InitializeStat()
    {
        int randomindex = (ManualIndex >= 0 && ManualIndex <= 17) ? ManualIndex : Random.Range(0, 18);

        switch (randomindex)
        {
            //True Solution
            case 0:
                TrueSolution = true;
                FoodAndBeverage = true;
                break;
            case 1:
                TrueSolution = true;
                Medicine = true;
                break;
            case 2:
                TrueSolution = true;
                Cosmetic = true;
                break;
            // Add other cases as needed
            default:
                Debug.LogError("Random index out of range");
                break;
        }

        Debug.Log($"Initialized Stat with randomindex: {randomindex}");
    }

    public class SharedStats
    {
        public bool ItemFound { get; set; } = false;
        public bool ItemVisible { get; set; } = false;
        public bool AppearanceFound { get; set; } = false;
        public bool UseFound { get; set; } = false;
    }

    public void UpdateSharedStats(string toolName)
    {
        if (sharedStatsDict.ContainsKey(mixtureKey))
        {
            SharedStats stats = sharedStatsDict[mixtureKey];
            // Appearance
            if (toolName == "Flashlight" && Colloid == true)
            {
                stats.AppearanceFound = true;
                Debug.Log($"Updated AppearanceFound to true for mixtureKey: {mixtureKey}");
            }

            // Log updated stats
            Debug.Log("Analyzer applied: Updated shared stats");
        }
    }
}
