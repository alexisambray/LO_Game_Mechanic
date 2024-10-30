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
}

