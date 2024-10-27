using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public string ItemName;
    public Sprite ItemSprite;
    public bool IsUnlocked { get; set; } = false; // Corrected capitalization
    public string Description;
    public string HiddenDescription;

    private Image itemImage;

    void Start()
    {
        itemImage = GetComponent<Image>();
        if (!IsUnlocked && itemImage != null)
        {
            itemImage.sprite = Resources.Load<Sprite>("Sprites/question_mark"); // Load your question mark sprite
        }
    }

    public void UnlockItem()
    {
        IsUnlocked = true; // Set the item as unlocked
        if (itemImage != null)
        {
            itemImage.sprite = ItemSprite; // Update the sprite to the actual item sprite
            Color color = itemImage.color;
            color.a = 1f; // Set alpha to fully visible
            itemImage.color = color;
        }
        Debug.Log($"{ItemSprite} has been unlocked!");
    }
}
