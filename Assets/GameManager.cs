using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }


    //Game state variables
    public bool shelfComplete { get; set; }
    public GameObject selectedObject;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        shelfComplete = false;
        selectedObject = null;
    }

    public void SelectObject(GameObject obj)
    {
        if (obj.CompareTag("Mixture"))
        {
            selectedObject = obj;
            Debug.Log("Object Selected: " + selectedObject.name);
        }
        else
        {
            Debug.Log("Selected object is not selectable");
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the clicked position
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Tool"))
                {
                    // Get the clicked object
                    GameObject clickedObject = hit.collider.gameObject;

                    // Compare stats
                    //CompareStats(clickedObject);
                }
                else
                {
                    //Debug.Log("The clicked object is not an InteractionObject.");
                }
            }
            else
            {
                //Debug.Log("No object selected or ray didn't hit anything");
            }
        }
    }

    private void ApplyToolEffect(Tool tool)
    {
        if(selectedObject != null)
        {
            tool.ApplyEffect(selectedObject);
        }
        else
        {
            //Debug.Log("No object selected to apply the tool effect.");
        }
    }

    //private void CompareStats(GameObject clickedObject)
    //{
    //    // Ensure the selected object is not null
    //    if (selectedObject != null)
    //    {
    //        // Get the stats from the selected object
    //        Mixture mixture = selectedObject.GetComponent<Mixture>();
    //        Tool tool = clickedObject.GetComponent<Tool>();

    //        if (mixture != null && tool != null)
    //        {
    //            // Compare the stats
    //            Debug.Log($"Comparing stats of {selectedObject.name} with {clickedObject.name}:");

    //            if (selectedItem.appearanceFound == clickedItem.appearanceFound)
    //            {
    //                Debug.Log("Appearance Found status matches.");
    //            }
    //            else
    //            {
    //                Debug.Log("Appearance Found status differs.");
    //            }

    //            if (selectedItem.useFound == clickedItem.useFound)
    //            {
    //                Debug.Log("Use Found status matches.");
    //            }
    //            else
    //            {
    //                Debug.Log("Use Found status differs.");
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("sampleItem component not found on one or both objects.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("No object is currently selected in the GameManager.");
    //    }
    //}
}


