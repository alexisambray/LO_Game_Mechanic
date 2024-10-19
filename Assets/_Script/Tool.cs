using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MixtureStat;

public class Tool : MonoBehaviour
{
    public string toolName;
    
    private void Awake()
    {
        toolName = gameObject.name;
    }

    public void ApplyEffect()
    {
        GameObject selectedObject = GameManager.Instance.selectedObject;

        
        if (selectedObject != null)
        {
            Mixture mixture = selectedObject.GetComponent<Mixture>();
            if (mixture != null) //if there is a selected object, get stats
            {
                MixtureStat mixtureStat = mixture.mixtureStat;
                //if (mixtureStat == null)
                //{
                //    Debug.LogError("Selected object does not have a MixtureStat component.");
                //    return;
                //}
                mixtureStat.UpdateSharedStats(toolName);

            }
        }
        
    }
}
