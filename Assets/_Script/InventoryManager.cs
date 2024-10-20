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

    private List<Item> itemsInInventory = new List<Item>(); // List of items in the inventory

    void Start()
    {
        Time.fixedDeltaTime = 0.02f; // Reduce the number of physics updates to optimize performance

        // Adjust grid size for larger touch targets on tablet screens
        gridLayoutGroup.cellSize = new Vector2(200, 200); // Adjust size for tablet touch
        menuActivated = false;
        InventoryMenu.SetActive(false); // Start with inventory menu hidden

        // Assume item slots are already assigned in the Inspector, populate them as hidden initially
        foreach (GameObject slot in itemSlots)
        {
            // Initially, all items are hidden
            slot.SetActive(false);
        }

        // Assign Item components to slots dynamically
        foreach (GameObject slot in itemSlots)
        {
            Item itemComponent = slot.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemsInInventory.Add(itemComponent);
            }
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
            InventoryMenu.SetActive(true); // Activates the menu
            menuActivated = true;
        }

        // Check for touch input (single finger tap)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleTouchInput();
        }

        // Maintain mouse input for testing in Unity Editor
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseInput();
        }
    }

    void HandleTouchInput()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            HandleItemInteraction(clickedObject);
        }
    }

    void HandleMouseInput()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            HandleItemInteraction(clickedObject);
        }
    }

    void HandleItemInteraction(GameObject clickedObject)
    {
        // Check if clicked object is an item slot
        Item clickedItem = clickedObject.GetComponent<Item>();
        if (clickedItem != null)
        {
            if (clickedItem.isUnlocked)
            {
                // If the item is unlocked, display its description and image
                selectedItemImage.GetComponent<Image>().sprite = clickedItem.itemImage;
                selectedItemImage.SetActive(true); // Show the selected item image
                selectedItemDescription.text = clickedItem.description; // Show the description
            }
            else
            {
                // If the item is still locked, show the pending DFA approval message
                selectedItemDescription.text = clickedItem.hiddenDescription;
            }
        }
    }

    // Method to unlock items after DFA approval and reveal them in the inventory
    public void UnlockItem(string itemName)
    {
        foreach (Item item in itemsInInventory)
        {
            if (item.itemName == itemName && !item.isUnlocked)
            {
                item.Unlock(); // Unlock the item
                // Find the corresponding item slot in the UI and activate it
                foreach (GameObject slot in itemSlots)
                {
                    if (slot.name == itemName)
                    {
                        slot.SetActive(true); // Reveal the item slot in the UI
                    }
                }
            }
        }
    }
}
