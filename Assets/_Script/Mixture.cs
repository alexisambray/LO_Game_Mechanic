using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mixture : MonoBehaviour
{
    public MixtureStat mixtureStat;
    private string mixtureKey;
    public int index = -1;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log($"Mixture Start: {gameObject.name}");


        // Initialize mixture stat based on index
        if (index >= 0 && index <= 17)
        {
            mixtureStat = gameObject.AddComponent<MixtureStat>();
            mixtureStat.ManualIndex = index;
            mixtureStat.InitializeStat();
            Debug.Log($"MixtureStat initialized with index {index}");
        }
        else
        {
            mixtureStat = gameObject.AddComponent<MixtureStat>();
            mixtureStat.InitializeStat();
            Debug.Log("MixtureStat initialized with default values");
        }

        mixtureKey = mixtureStat.GenerateKey();
        Debug.Log($"Mixture Key Generated: {mixtureKey}");

        // Log shared stats if they exist
        if (MixtureStat.sharedStatsDict.ContainsKey(mixtureKey))
        {
            MixtureStat.SharedStats sharedStats = MixtureStat.sharedStatsDict[mixtureKey];
            Debug.Log($"{gameObject.name} - ItemFound: {sharedStats.ItemFound}");
            Debug.Log($"{gameObject.name} - AppearanceFound: {sharedStats.AppearanceFound}");
            Debug.Log($"{gameObject.name} - UseFound: {sharedStats.UseFound}");
        }
    }

    // Method to handle unlocking and checking conditions for the mixture
    public void UnlockInInventory()
    {
        if (mixtureStat != null && mixtureStat.ItemFound) // Example condition
        {
            Debug.Log($"Attempting to unlock {gameObject.name}");
            // Call the InventoryManager to unlock the item
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.UnlockItem(gameObject.name, GetComponent<Image>().sprite, gameObject);
                Debug.Log($"{gameObject.name} has been unlocked.");
            }
            else
            {
                Debug.LogError("InventoryManager instance not found!");
            }
        }
        else
        {
            Debug.Log($"{gameObject.name} is not ready to be unlocked. ItemFound is false.");
        }
    }

    // Method to simulate tool interaction (update appearance or other stats)
    public void ApplyTool(string toolName)
    {
        Debug.Log($"Applying tool: {toolName} to {gameObject.name}");
        mixtureStat.UpdateSharedStats(toolName);
    }

    // Getter for mixture stat
    public MixtureStat GetMixtureStat()
    {
        return mixtureStat;
    }
}
