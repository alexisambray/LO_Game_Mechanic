using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // each item with unique name
    public Sprite itemImage; 
    public Sprite itemSprite; // sprite for the item
    public bool isUnlocked = false;
    public string description; // Description shown after unlocking
    public string hiddenDescription = "This item is pending DFA approval."; // Initial hidden description

    // Method to unlock the item
    public void Unlock()
    {
        isUnlocked = true;
    }
}
