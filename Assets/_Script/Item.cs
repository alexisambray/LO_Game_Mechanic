using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public string itemName; // Changed to camelCase
    public Sprite itemSprite; // Changed to camelCase
    public bool isUnlocked { get; private set; } = false; // Changed to camelCase
    public string description; // Changed to camelCase
    public string hiddenDescription; // Changed to camelCase

    private Image itemImage;
    private MixtureStat mixtureStat; // Reference to the MixtureStat

    void Awake()
    {
        // Get the MixtureStat component from the associated Mixture
        Mixture mixture = GetComponent<Mixture>(); // Assuming this Item is part of a Mixture
        if (mixture != null)
        {
            mixtureStat = mixture.mixtureStat;
        }
        else
        {
            Debug.LogError("Mixture component not found!");
        }
    }

    void Start()
    {
        itemImage = GetComponent<Image>();
        if (!isUnlocked && itemImage != null)
        {
            itemImage.sprite = Resources.Load<Sprite>("Sprites/question_mark");
        }
    }

    public void UnlockItem()
    {
        isUnlocked = true; // Use the camelCase variable
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
            Color color = itemImage.color;
            color.a = 1f;
            itemImage.color = color;
        }
        Debug.Log($"{itemName} has been unlocked!");
    }

    public bool IsVisible()
    {
        bool isVisible = isUnlocked && mixtureStat != null && mixtureStat.ItemFound;
        Debug.Log($"IsVisible called for {itemName}: isUnlocked={isUnlocked}, mixtureStat={mixtureStat != null}, ItemFound={mixtureStat?.ItemFound}");
        // Check if item is unlocked and if the MixtureStat's ItemFound is true
        return isUnlocked && mixtureStat != null && mixtureStat.ItemFound;
    }
}
