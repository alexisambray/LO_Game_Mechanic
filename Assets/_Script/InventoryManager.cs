using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    // Singleton instance
    public static InventoryManager Instance { get; private set; }

    // Inventory slots
    public List<GameObject> itemSlots; // Assign these in the inspector (ItemSlot panels)
    public GameObject selectedItemImage; // Image to show the selected item
    public TextMeshProUGUI selectedItemDescriptionText; // Description text to show selected item
    public GameObject inventoryMenu; // Inventory canvas panel to be toggled
    public TextMeshProUGUI selectedItemNameText;

    private void Awake()
    {
        // Initialize singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Ensure the inventory is hidden initially
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
        }
    }

    private void Update()
    {
        // Listen for the "i" key to toggle inventory visibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryMenu != null)
            {
                inventoryMenu.SetActive(!inventoryMenu.activeSelf);
                Debug.Log($"Inventory Menu set to: {inventoryMenu.activeSelf}");
            }
        }
    }

    public void UnlockItem(string itemName, Sprite itemSprite, GameObject clickedObject)
    {
        Debug.Log($"Attempting to unlock item: {itemName}");

        if (clickedObject != null)
        {
            Image slotImage = clickedObject.GetComponent<Image>();
            Item item = clickedObject.GetComponent<Item>();

            if (slotImage != null && item != null)
            {
                item.UnlockItem(); // Call the UnlockItem method to handle unlocking logic

                // Update the slot image
                slotImage.sprite = itemSprite;

                Transform questionMarkTransform = clickedObject.transform.Find("QuestionMarkImage");
                if (questionMarkTransform != null)
                {
                    questionMarkTransform.gameObject.SetActive(false); // Hide the question mark
                }

                Debug.Log($"{itemName} has been unlocked!");
            }
        }
    }


    public void HandleItemInteraction(GameObject clickedObject)
    {
        Debug.Log($"Clicked object: {clickedObject.name}");

        // Ensure clickedObject has a MixturePrefab component
        MixturePrefab mixturePrefab = clickedObject.GetComponent<MixturePrefab>();

        // If the clicked object is an ItemSlot, access its MixturePrefab
        if (mixturePrefab == null)
        {
            // Try getting MixturePrefab from the child GameObject if necessary
            mixturePrefab = clickedObject.GetComponentInChildren<MixturePrefab>();
        }

        if (mixturePrefab != null)
        {
            Debug.Log($"MixturePrefab found: {mixturePrefab.itemName}, IsUnlocked: {mixturePrefab.isUnlocked}");

            if (mixturePrefab.isUnlocked) // Check if unlocked
            {
                selectedItemImage.SetActive(true);
                selectedItemImage.GetComponent<Image>().sprite = mixturePrefab.itemSprite; // Update item sprite

                // Check for null references here
                if (selectedItemNameText != null)
                    selectedItemNameText.text = mixturePrefab.itemName; // Update item name text
                else
                    Debug.LogError("selectedItemNameText is null");

                if (selectedItemDescriptionText != null)
                    selectedItemDescriptionText.text = mixturePrefab.description; // Update item description
                else
                    Debug.LogError("selectedItemDescriptionText is null");

                Debug.Log($"Selected item: {mixturePrefab.itemName}");
            }
            else
            {
                Debug.LogError("Item is not unlocked.");
            }
        }
        else
        {
            Debug.LogError("MixturePrefab is missing.");
        }
        Debug.Log($"Item Sprite: {mixturePrefab?.itemSprite}, Item Name: {mixturePrefab?.itemName}, Description: {mixturePrefab?.description}");
    }

    public void PopulateInventory()
    {
        foreach(var slot in itemSlots)
        {
            Item item = slot.GetComponent<Item>();

            if (item != null && item.IsVisible())
            {
                slot.SetActive(true);
            }
            else
            {
                slot.SetActive(false);
            }
        }
    }


}




// Previous code is commented out below
/*
public void UnlockItem(string itemName, Sprite itemSprite, GameObject clickedObject)
{
    Debug.Log($"Attempting to unlock item: {itemName}");

    if (clickedObject != null)
    {
        // Get the Image component from the clicked object to update its sprite
        Image slotImage = clickedObject.GetComponent<Image>();
        Item item = clickedObject.GetComponent<Item>();

        if (slotImage != null && item != null)
        {
            item.IsUnlocked = true; // Set the item as unlocked
            slotImage.sprite = itemSprite;

            // Hide the question mark image and show the actual item image
            Transform questionMarkTransform = clickedObject.transform.Find("QuestionMarkImage");
            if (questionMarkTransform != null)
            {
                questionMarkTransform.gameObject.SetActive(false); // Hide the question mark
            }

            Debug.Log($"{itemName} has been unlocked!");
        }
    }
}

public void HandleItemInteraction(GameObject clickedObject)
{
    Debug.Log($"Interacting with item slot: {clickedObject.name}");

    if (clickedObject != null)
    {
        // Reveal the actual item image and hide the question mark
        Transform questionMarkTransform = clickedObject.transform.Find("QuestionMarkImage");
        if (questionMarkTransform != null)
        {
            questionMarkTransform.gameObject.SetActive(false); // Hide the question mark
        }

        Transform mixtureTransform = clickedObject.transform.Find("MixturePrefab");
        if (mixtureTransform != null)
        {
            mixtureTransform.gameObject.SetActive(true); // Show the mixture image
            selectedItemImage.SetActive(true);
            selectedItemImage.GetComponent<Image>().sprite = mixtureTransform.GetComponent<Image>().sprite;
        }

        // Update description (this can be static for testing purposes)
        Item item = mixtureTransform.GetComponent<Item>();
        if (item != null)
        {
            selectedItemDescription.text = item.IsUnlocked ? item.Description : item.HiddenDescription;
        }
    }
}
*/

