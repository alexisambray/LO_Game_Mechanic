using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public GridLayoutGroup gridLayoutGroup;

    // Inventory slots
    public List<GameObject> itemSlots; // Assign these in the inspector (ItemSlot panels)
    public GameObject selectedItemImage; // Image to show the selected item
    public Text selectedItemDescription; // Description text to show selected item
    public Sprite someVinegarSprite; // Placeholder for an item sprite, like vinegar

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>();
    private List<string> unlockedItems = new List<string>(); // List of unlocked items

    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 0.02f; // Reduce the number of physics updates to optimize performance

        // Adjust grid size for larger touch targets on tablet screens
        gridLayoutGroup.cellSize = new Vector2(200, 200); // Adjust size for tablet touch
        menuActivated = false;
        InventoryMenu.SetActive(false); // Start with inventory menu hidden

        // Initialize item descriptions (after DFA approval)
        itemDescriptions.Add("Vinegar", "A homogeneous mixture used for cooking.");

        // Initialize all item slots as hidden initially, with question marks visible
        foreach (GameObject slot in itemSlots)
        {
            Transform questionMarkImage = slot.transform.Find("QuestionMarkImage");
            Transform mixturePrefabImage = slot.transform.Find("MixturePrefab");

            if (questionMarkImage != null)
            {
                questionMarkImage.gameObject.SetActive(true); // Show the question mark initially
            }

            if (mixturePrefabImage != null)
            {
                mixturePrefabImage.gameObject.SetActive(false); // Hide the mixture image initially
            }
        }

        // Test unlocking an item
        UnlockItem("Vinegar", someVinegarSprite); // Replace 'someVinegarSprite' with the actual Sprite variable for Vinegar
    }

    // Update is called once per frame
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

            // Make sure all question marks are shown when opening inventory
            foreach (GameObject slot in itemSlots)
            {
                Transform questionMarkImage = slot.transform.Find("QuestionMarkImage");
                Transform mixturePrefabImage = slot.transform.Find("MixturePrefab");

                if (questionMarkImage != null && !unlockedItems.Contains(slot.name))
                {
                    questionMarkImage.gameObject.SetActive(true); // Show question mark for locked items
                }

                if (mixturePrefabImage != null && !unlockedItems.Contains(slot.name))
                {
                    mixturePrefabImage.gameObject.SetActive(false); // Ensure mixture image stays hidden for locked items
                }
            }
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
            // Check for interactions with UI or game objects
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
            // Handle interactions for mouse clicks
            GameObject clickedObject = hit.collider.gameObject;
            HandleItemInteraction(clickedObject);
        }
    }

    // This method will handle item interactions, such as when a player selects or unlocks an item
    void HandleItemInteraction(GameObject clickedObject)
    {
        // Assuming clickedObject is an inventory slot or an item in the game
        if (unlockedItems.Contains(clickedObject.name))
        {
            // If the item is unlocked, display its description and image
            selectedItemImage.SetActive(true); // Show the selected item image
            selectedItemImage.GetComponent<Image>().sprite = GetItemSprite(clickedObject.name); // Show the sprite
            selectedItemDescription.text = itemDescriptions[clickedObject.name]; // Show the description
        }
    }

    // This method unlocks items after DFA approval and reveals them in the inventory
    public void UnlockItem(string itemName, Sprite itemSprite)
    {
        if (!unlockedItems.Contains(itemName))
        {
            unlockedItems.Add(itemName); // Add item to the unlocked list

            // Find the corresponding item slot in the UI and update it
            foreach (GameObject slot in itemSlots)
            {
                if (slot.name == itemName)
                {
                    // Reveal the mixture image and hide the question mark image
                    Transform questionMarkImage = slot.transform.Find("QuestionMarkImage");
                    Transform mixturePrefabImage = slot.transform.Find("MixturePrefab");

                    if (questionMarkImage != null)
                    {
                        questionMarkImage.gameObject.SetActive(false); // Hide the question mark
                    }

                    if (mixturePrefabImage != null)
                    {
                        mixturePrefabImage.gameObject.SetActive(true); // Show the mixture image
                        mixturePrefabImage.GetComponent<Image>().sprite = itemSprite; // Assign the correct sprite
                    }
                }
            }
        }
    }

    // Helper method to get the item sprite
    private Sprite GetItemSprite(string itemName)
    {
        // Find the item GameObject by name and get its sprite
        foreach (GameObject item in itemSlots)
        {
            if (item.name == itemName)
            {
                Transform mixturePrefabImage = item.transform.Find("MixturePrefab");
                if (mixturePrefabImage != null)
                {
                    return mixturePrefabImage.GetComponent<Image>().sprite;
                }
            }
        }
        return null;
    }
}
