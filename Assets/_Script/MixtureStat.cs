using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class MixtureStat: MonoBehaviour 
{
    // Criteria
    public static Dictionary<string, SharedStats> sharedStatsDict = new Dictionary<string, SharedStats>();
    private string mixtureKey;

    // Appearance
    public bool trueSolution { get; set; }
    public bool suspension { get; set; }
    public bool colloid { get; set; }

    // Uses
    public bool foodAndBeverage { get; set; }
    public bool medicine { get; set; }
    public bool cosmetic { get; set; }
    public bool cleaning { get; set; }
    public bool hygiene { get; set; }
    public bool agriculture { get; set; }

    public int manualIndex = -1;
    public MixtureStat()
    {
        // Initialize all variables with default values
        trueSolution = false;
        suspension = false;
        colloid = false;

        foodAndBeverage = false;
        medicine = false;
        cosmetic = false;
        cleaning = false;
        hygiene = false;
        agriculture = false;

        initializeStat(); //initialize stats for the mixture

        mixtureKey = GenerateKey(); 

        if (!sharedStatsDict.ContainsKey(mixtureKey))
        {
            sharedStatsDict[mixtureKey] = new SharedStats();
        }
    }

    public MixtureStat(int chosenIndex)
    {
        // Initialize all variables with default values
        trueSolution = false;
        suspension = false;
        colloid = false;

        foodAndBeverage = false;
        medicine = false;
        cosmetic = false;
        cleaning = false;
        hygiene = false;
        agriculture = false;

        manualIndex = chosenIndex;

        initializeStat(); //initialize stats for the mixture

        mixtureKey = GenerateKey();

        if (!sharedStatsDict.ContainsKey(mixtureKey))
        {
            sharedStatsDict[mixtureKey] = new SharedStats();
        }
    }

    public string GenerateKey()
    {
        return $"{trueSolution}_{suspension}_{colloid}_{foodAndBeverage}_{medicine}_{cosmetic}_{cleaning}_{hygiene}_{agriculture}";
    }

    // Accessor for the shared itemFound variable
    public bool ItemFound
    {
        get { return sharedStatsDict[mixtureKey].itemFound; }
        set { sharedStatsDict[mixtureKey].itemFound = value; }
    }

    // Accessor for the shared appearanceFound variable
    public bool AppearanceFound
    {
        get { return sharedStatsDict[mixtureKey].appearanceFound; }
        set { sharedStatsDict[mixtureKey].appearanceFound = value; }
    }

    // Accessor for the shared useFound variable
    public bool UseFound
    {
        get { return sharedStatsDict[mixtureKey].useFound; }
        set { sharedStatsDict[mixtureKey].useFound = value; }
    }

    public void initializeStat()
    {
        //int randomindex = Random.Range(0, 18);
        int randomindex = (manualIndex >= 0 && manualIndex <= 17) ? manualIndex : Random.Range(0, 18);

        switch (randomindex)
        {
            //True Solution
            case 0:
                trueSolution = true;
                foodAndBeverage = true;
                break;
            case 1:
                trueSolution = true;
                medicine = true;
                break;
            case 2:
                trueSolution = true;
                cosmetic= true; 
                break;
            case 3: 
                trueSolution = true;
                cleaning= true;
                break;
            case 4:
                trueSolution = true;
                hygiene= true;
                break;
            case 5:
                trueSolution = true;
                agriculture= true; 
                break;
            //Suspension
            case 6:
                suspension = true;
                foodAndBeverage = true;
                break;
            case 7:
                suspension = true;
                medicine = true;
                break;
            case 8:
                suspension = true;
                cosmetic = true;
                break;
            case 9:
                suspension = true;
                cleaning = true;
                break;
            case 10:
                suspension = true;
                hygiene = true;
                break;
            case 11:
                suspension = true;
                agriculture = true;
                break;
            //Colloids
            case 12:
                colloid = true;
                foodAndBeverage = true;
                break;
            case 13:
                colloid = true;
                medicine = true;
                break;
            case 14:
                colloid = true;
                cosmetic = true;
                break;
            case 15:
                colloid = true;
                cleaning = true;
                break;
            case 16:
                colloid = true;
                hygiene = true;
                break;
            case 17:
                colloid = true;
                agriculture = true;
                break;
            default:
                Debug.LogError("Random index out of range");
                break;
        }

        Debug.Log("Updated stats based on random index: " + randomindex);

        /*        Debug.Log($"True Solution: {trueSolution}");
                Debug.Log($"Suspension: {suspension}");
                Debug.Log($"Colloid: {colloid}");
                Debug.Log($"Food and Beverage: {foodAndBeverage}");
                Debug.Log($"Medicine: {medicine}");
                Debug.Log($"Cosmetic: {cosmetic}");
                Debug.Log($"Cleaning: {cleaning}");
                Debug.Log($"Hygiene: {hygiene}");
                Debug.Log($"Agriculture: {agriculture}");   */
    }

    public class SharedStats
    {
        public bool itemFound { get; set; } = false;
        public bool itemVisible { get; set; } = false;
        public bool appearanceFound { get; set; } = false;
        public bool useFound { get; set; } = false;
    }

    public void UpdateSharedStats(string toolName)
    {
        if (sharedStatsDict.ContainsKey(mixtureKey))
        {
            SharedStats stats = sharedStatsDict[mixtureKey];
            //Appearance
            if (toolName == "Flashlight" && colloid == true) { AppearanceFound = true; }
            else if (toolName == "Centrifuge" && (suspension == true || trueSolution == true)) { AppearanceFound = true; }
            else if (toolName == "Observe" && suspension == true) { AppearanceFound = true;  }

            // Log updated stats
            Debug.Log("Analyzer applied: Updated shared stats");
            Debug.Log($" - ItemFound: {ItemFound}");
            Debug.Log($" - AppearanceFound: {AppearanceFound}");
            Debug.Log($" - UseFound: {UseFound}");
        }
    }


}
