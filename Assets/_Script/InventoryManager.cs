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


    public void ToggleInventoryVisibility()
    {
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            Debug.Log($"Inventory Menu set to: {inventoryMenu.activeSelf}");
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

        MixturePrefab mixturePrefab = clickedObject.GetComponent<MixturePrefab>();
        if (mixturePrefab != null)
        {
            if (mixturePrefab.isUnlocked)
            {
                selectedItemImage.SetActive(true);
                selectedItemImage.GetComponent<Image>().sprite = mixturePrefab.itemSprite; // Ensure sprite is assigned

                if (selectedItemNameText != null)
                    selectedItemNameText.text = mixturePrefab.itemName;

                if (selectedItemDescriptionText != null)
                    selectedItemDescriptionText.text = mixturePrefab.description;
            }
        }
    }

    public void PopulateInventory()
    {
        foreach(var slot in itemSlots)
        {
            Item item = slot.GetComponent<Item>();

            if (item != null && item.IsVisible())
            {
                slot.SetActive(true);
                Debug.Log($"Slot for {item.itemName} is visible.");
            }
            else
            {
                slot.SetActive(false);
                Debug.Log($"Slot for {item.itemName ?? "unknown item"} is hidden.");
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

