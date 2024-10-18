using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MixtureStat;

public class Tool : MonoBehaviour
{
    private static Dictionary<string, SharedStats> sharedStatsDict = new Dictionary<string, SharedStats>();
    public string toolName;
    
    private void Awake()
    {
        toolName = gameObject.name;
    }

    public void ApplyEffect(GameObject selectedObject)
    {
        Mixture mixture  = selectedObject.GetComponent<Mixture>();
        if (mixture == null)
        {
            Debug.LogError("Selected object does not have a MixtureObject component.");
            return;
        }
       
        //if(!sharedStatsDict.ContainsKey(mixture.mixtureStat)) {
    }
}
