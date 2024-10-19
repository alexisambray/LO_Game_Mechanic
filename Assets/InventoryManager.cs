using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // use this for UI elements like text and image

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;

    // Inventory slots
    public List<GameObject> itemSlots; //  assign these in the inspector (IemSlot panels)
    public GameObject selectedItemImage; // image to show the selected item
    public Text selectedItemDescription; // description text to show selected item

    private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>();
    private Dictionary<string, string> hiddenDescriptions = new Dictionary<string, string>();
    private List<string> unlockedItems = new List<string>(); // list of unlocked items


    // Start is called before the first frame update
    void Start()
    {
        menuActivated = false;
        InventoryMenu.SetActive(false); // start with inventory menu hidden

        // initialize item descriptions (locked descriptions before DFA approval)
        hiddenDescriptions.Add("Vinegar", "This item is pending DFA approval.");
        hiddenDescriptions.Add("Blush", "This item is pending DFA approval.");
        hiddenDescriptions.Add("Soap", "This item is pending DFA approval.");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory") && menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            InventoryMenu.SetActive(true); // deactivates the menu
            menuActivated = true;
        }
    }
}
