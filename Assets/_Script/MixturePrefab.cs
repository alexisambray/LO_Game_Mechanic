using UnityEngine;
using UnityEngine.UI;

public class MixturePrefab : MonoBehaviour
{
    public string itemName; // Item name
    public Sprite itemSprite; // Sprite to show when unlocked
    public Image itemImage;
    public string Description { get; set; }

    public bool IsUnlocked { get; private set; } = false; // Unlock status

    public void UnlockItem()
    {
        IsUnlocked = true; // Set the item as unlocked
        Debug.Log($"{itemName} is being unlocked.");
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite; // Update the sprite to the actual item sprite
            Color color = itemImage.color; // Get current color
            color.a = 1f; // Set alpha to 1 (fully visible)
            itemImage.color = color; // Update the color
        }
        Debug.Log($"{itemName} has been unlocked!");
    }
}
