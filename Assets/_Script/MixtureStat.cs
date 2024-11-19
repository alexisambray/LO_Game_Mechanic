using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text feedbackText;

    private void Awake()
    {
        feedbackText = GameObject.Find("FeedbackText").GetComponent<Text>();

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
            case 3:
                TrueSolution = true;
                Cleaning = true;
                break;
            case 4:
                TrueSolution = true;
                Hygiene = true;
                break;
            case 5:
                TrueSolution = true;
                Agriculture = true;
                break;
            //Suspension
            case 6:
                Suspension = true;
                FoodAndBeverage = true;
                break;
            case 7:
                Suspension = true;
                Medicine = true;
                break;
            case 8:
                Suspension = true;
                Cosmetic = true;
                break;
            case 9:
                Suspension = true;
                Cleaning = true;
                break;
            case 10:
                Suspension = true;
                Hygiene = true;
                break;
            case 11:
                Suspension = true;
                Agriculture = true;
                break;
            //Colloids
            case 12:
                Colloid = true;
                FoodAndBeverage = true;
                break;
            case 13:
                Colloid = true;
                Medicine = true;
                break;
            case 14:
                Colloid = true;
                Cosmetic = true;
                break;
            case 15:
                Colloid = true;
                Cleaning = true;
                break;
            case 16:
                Colloid = true;
                Hygiene = true;
                break;
            case 17:
                Colloid = true;
                Agriculture = true;
                break;
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
            if (toolName == "Flashlight" && Colloid == true) { stats.AppearanceFound = true; }
            else if (toolName == "Centrifuge" && (Suspension == true || TrueSolution == true)) { stats.AppearanceFound = true; }
            else if (toolName == "Observe" && Suspension == true) { stats.AppearanceFound = true; }

            // Log updated stats
            Debug.Log($"Updated AppearanceFound to true for mixtureKey: {mixtureKey}");
            Debug.Log($" - ItemFound: {stats.ItemFound}");
            Debug.Log($" - AppearanceFound: {stats.AppearanceFound}");
            Debug.Log($" - UseFound: {stats.UseFound}");

            // If Appearance is found, automatically mark Item as found
            if (stats.AppearanceFound)
            {
                stats.ItemFound = true; // Set ItemFound to true
                Debug.Log($"ItemFound set to true for mixtureKey: {mixtureKey}");
            }
            // Provide feedback
            if (stats.AppearanceFound)
            {
                feedbackText.text = "Appearance found!";
                feedbackText.color = Color.green;
            }
            else
            {
                feedbackText.text = "Nothing found.";
                feedbackText.color = Color.red;
            }

            StartCoroutine(FadeOutFeedback());
        }
    }

    private IEnumerator FadeOutFeedback()
    {
        // Show the feedback text
        feedbackText.gameObject.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Fade out the text
        Color originalColor = feedbackText.color;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            feedbackText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        // Hide the feedback text after fading out
        feedbackText.gameObject.SetActive(false);
    }
}
