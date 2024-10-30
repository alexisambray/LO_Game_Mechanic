using UnityEngine;
using UnityEngine.UI;

public class MixturePrefab : MonoBehaviour
{
    public string itemName; // Item name
    public Sprite itemSprite; // Sprite to show when unlocked
    public Image itemImage;
    public string description; // Keep this as the description field

    public bool isUnlocked { get; private set; } = false; // Unlock status

    public void UnlockItem()
    {
        isUnlocked = true; // Set the item as unlocked

        if (itemImage != null)
        {
            itemImage.sprite = itemSprite; // Update the sprite to show the actual item
            Color color = itemImage.color;
            color.a = 1f; // Set alpha to 1 to make it visible
            itemImage.color = color;
        }

        Debug.Log($"{itemName} has been unlocked!");
        Debug.Log($"Unlocking Item - Name: {itemName}, Sprite: {itemSprite}, Description: {description}");
    }
}
