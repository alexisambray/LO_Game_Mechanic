using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public MixturePrefab mixturePrefab; // Reference to the associated MixturePrefab
    public InventoryManager inventoryManager; // Reference to the InventoryManager
    public Image itemImage; // Reference to the Image component for the item

    void Start()
    {
        // Set the initial sprite to the question mark image
        itemImage.sprite = Resources.Load<Sprite>("Sprites/question_mark");
        Color color = itemImage.color;
        color.a = 0f; // Start hidden
        itemImage.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mixturePrefab != null)
        {
            Item item = mixturePrefab.GetComponent<Item>();

            if (item != null && item.IsVisible())
            {
                if (!mixturePrefab.isUnlocked)
                {
                    mixturePrefab.UnlockItem(); // Unlock the item
                    inventoryManager.HandleItemInteraction(gameObject); // Update inventory UI
                }
                else
                {
                    Debug.Log("Item already unlocked.");
                }
            }
        }
    }
}

