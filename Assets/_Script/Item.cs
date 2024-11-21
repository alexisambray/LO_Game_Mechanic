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
        Mixture mixture = GetComponent<Mixture>();
        if (mixture != null)
        {
            mixtureStat = mixture.mixtureStat; // Ensure you reference the MixtureStat instance
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
        isUnlocked = true;
        if (itemImage != null)
        {
            Debug.Log($"Unlocking item: {itemName}, Sprite: {itemSprite}");
            itemImage.sprite = itemSprite; // Update the sprite
            Color color = itemImage.color;
            color.a = 1f; // Make the image visible
            itemImage.color = color;
        }
    }

    public bool IsVisible()
    {
        bool isVisible = isUnlocked && mixtureStat != null && mixtureStat.ItemFound;
        Debug.Log($"IsVisible called for {itemName}: isUnlocked={isUnlocked}, mixtureStat={mixtureStat != null}, ItemFound={mixtureStat?.ItemFound}");
        // Return true if item is unlocked and found
        return isVisible;
    }
}
