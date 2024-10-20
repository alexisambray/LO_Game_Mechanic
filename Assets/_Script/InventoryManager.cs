using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public GridLayoutGroup gridLayoutGroup;

    // Inventory slots
    public List<GameObject> itemSlots; // Assign these in the inspector (ItemSlot panels)
    public GameObject selectedItemImage; // Image to show the selected item
    public TextMeshProUGUI selectedItemDescription; // Description text to show selected item

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>();
    private List<string> unlockedItems = new List<string>(); // List of unlocked items

    void Start()
    {
        Time.fixedDeltaTime = 0.02f; // Reduce the number of physics updates to optimize performance
        gridLayoutGroup.cellSize = new Vector2(200, 200); // Adjust size for tablet touch
        menuActivated = false;
        InventoryMenu.SetActive(false); // Start with inventory menu hidden

        // Example item descriptions (after DFA approval)
        itemDescriptions.Add("Vinegar", "A homogeneous mixture used for cooking.");
        itemDescriptions.Add("Blush", "A cosmetic product used to add color to the cheeks.");
        itemDescriptions.Add("Soap", "A product used for cleaning and hygiene.");

        foreach (GameObject slot in itemSlots)
        {
            slot.SetActive(false); // Initially, no items are unlocked
        }
    }

    void Update()
    {
        // Handle input for opening/closing the inventory menu
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void UnlockItem(string itemName, Sprite itemSprite)
    {
        if (!unlockedItems.Contains(itemName))
        {
            unlockedItems.Add(itemName); // Add item to the unlocked list

            // Find the corresponding item slot in the UI and activate it
            foreach (GameObject slot in itemSlots)
            {
                if (slot.name == itemName)
                {
                    slot.SetActive(true); // Reveal the item slot in the UI
                    slot.GetComponent<Image>().sprite = itemSprite; // Assign the correct sprite
                }
            }
        }
    }

    public void DisplayItemDetails(string itemName)
    {
        if (unlockedItems.Contains(itemName))
        {
            // Update the selected item image and description
            selectedItemImage.SetActive(true);
            selectedItemImage.GetComponent<Image>().sprite = GetItemSprite(itemName); // Fetch the sprite of the item
            selectedItemDescription.text = itemDescriptions[itemName];
        }
    }

    private Sprite GetItemSprite(string itemName)
    {
        // Find the item GameObject by name and get its sprite
        foreach (GameObject item in itemSlots)
        {
            if (item.name == itemName)
            {
                Item itemScript = item.GetComponent<Item>();
                if (itemScript != null)
                {
                    return itemScript.itemSprite;
                }
            }
        }
        return null;
    }
}
