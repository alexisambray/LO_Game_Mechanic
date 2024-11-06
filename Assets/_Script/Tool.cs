using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MixtureStat;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    public string toolName;
    public GameObject currentMixture;

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

    public void AddMixture(GameObject mixture)
    {
        if (currentMixture == null)
        {
            currentMixture = mixture;
            Debug.Log("Mixture added to tool: " + mixture.name);

            // Set the mixture's parent to the tool and reset its position
            mixture.transform.SetParent(transform);
            mixture.transform.localPosition = Vector3.zero; // Reset position

            MakeObjectTransparent(mixture);
        }
        else
        {
            Debug.Log("Tool already has a mixture: " + currentMixture.name);
        }
    }

    public GameObject RemoveMixture()
    {
        GameObject removedMixture = currentMixture;
        if (removedMixture != null)
        {
            currentMixture = null;
            Debug.Log("Mixture removed from tool: " + removedMixture.name);

            // Optionally, reset the mixture's parent back to the mixture slot or main object
            removedMixture.transform.SetParent(null); // Or set to the original slot
            removedMixture.transform.localPosition = Vector3.zero; // Reset position
            
            MakeObjectOpaque(removedMixture);

            return removedMixture; // Return the removed mixture
        }
        return null;
    }

    private void MakeObjectTransparent(GameObject obj)
    {
        if (obj != null)
        {
            Graphic graphic = obj.GetComponent<Graphic>();
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

            if (graphic != null)
            {
                Color color = graphic.color; // Get current color
                color.a = 0; // Set alpha to 0 for full transparency
                graphic.color = color; // Apply the new color
                graphic.raycastTarget = false;
            }

            if (collider != null)
            {
                collider.enabled = false; // Disable the collider to prevent clicks
            }
        }
    }

    private void MakeObjectOpaque(GameObject obj)
    {
        if (obj != null)
        {
            Graphic graphic = obj.GetComponent<Graphic>();
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

            if (graphic != null)
            {
                Color color = graphic.color; // Get current color
                color.a = 1; // Set alpha to 1 for full opacity
                graphic.color = color; // Apply the new color
                graphic.raycastTarget = true;
            }

            if (collider != null)
            {
                collider.enabled = true; // Re-enable the collider when opaque
            }
        }
    }

}
