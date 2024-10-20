using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
// using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MixtureStat;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public bool shelfComplete { get; set; }
    public GameObject selectedObject;
    public Dictionary<string, SharedStats> sharedStatsDict = new Dictionary<string, SharedStats>();

    public GameObject mixturePrefab;
    public int numOfMixtures = 3;
    public Transform mixtureSlotPanel;
    private List<GameObject> instantiatedMixtures = new List<GameObject>();
    public int numOfMixturesCreated = 0;
    public GraphicRaycaster raycaster; // Reference to the canvas's GraphicRaycaster
    public EventSystem eventSystem; // Reference to the EventSystem

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }


    
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

        InstantiateRandomMixture();
    }

    public void InstantiateRandomMixture()
    {
        for (int i = 0; i < numOfMixtures; i++)
        {
            GameObject newMixture = Instantiate(mixturePrefab, mixtureSlotPanel);
            newMixture.transform.localScale = Vector3.one;
            newMixture.name = "Mixture_" + numOfMixturesCreated;

            numOfMixturesCreated++;

            instantiatedMixtures.Add(newMixture);
        }
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
        //if (Input.GetMouseButtonDown(0)) // Left mouse button click
        //{
        //    PointerEventData pointerData = new PointerEventData(EventSystem.current)
        //    {
        //        position = Input.mousePosition // Mouse position in screen space
        //    };

        //    List<RaycastResult> results = new List<RaycastResult>();
        //    //GraphicRaycaster raycaster = mixtureSlotPanel.GetComponent<GraphicRaycaster>(); // Ensure GraphicRaycaster is present

        //    // Raycast to detect UI elements
        //    raycaster.Raycast(pointerData, results);

        //    // Check if we hit any UI elements
        //        foreach (RaycastResult result in results)
        //        {
        //        GameObject hitObject = result.gameObject;
        //        if (hitObject.CompareTag("Tool")) // Check for Tool tag
        //            {
        //                Tool selectedTool = result.gameObject.GetComponent<Tool>();

        //                if (selectedObject != null && selectedTool != null)
        //                {
        //                    ApplyToolEffect(selectedTool);
        //                }
        //                break; // Stop checking after finding the first tool
        //            }
        //        }

        //}

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition // Mouse position in screen space
            };

            List<RaycastResult> results = new List<RaycastResult>();

            // Ensure to get the raycaster from the correct component
            GraphicRaycaster raycaster = mixtureSlotPanel.GetComponentInParent<GraphicRaycaster>();

            if (raycaster == null)
            {
                Debug.LogError("GraphicRaycaster not found on the canvas or its parents.");
                return;
            }

            // Raycast to detect UI elements
            raycaster.Raycast(pointerData, results);

            // Check if we hit any UI elements
            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    GameObject hitObject = result.gameObject;
                    Debug.Log($"Hit: {hitObject.name} with tag {hitObject.tag}");

                    if (hitObject.CompareTag("Tool")) // Check for Tool tag
                    {
                        Tool selectedTool = hitObject.GetComponent<Tool>();

                        if (selectedTool != null)
                        {
                            if (selectedObject != null)
                            {
                                ApplyToolEffect(selectedTool);
                                selectedObject = null;
                            }
                            else
                            {
                                Debug.Log("No object selected to apply the tool effect.");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Hit object does not have a Tool component.");
                        }
                        break; // Stop checking after finding the first tool
                    }
                }
            }
            else
            {
                Debug.Log("No UI elements hit.");
            }
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Cast a ray from the camera to the clicked position
        //    Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        //    if (hit.collider != null)
        //    {
        //        if (hit.collider.gameObject.CompareTag("Tool"))
        //        {
        //            // Get the clicked object
        //            Tool selectedTool = hit.collider.gameObject.GetComponent<Tool>();

        //            if (selectedObject != null && selectedTool != null) {
        //                ApplyToolEffect(selectedTool);

        //                //MixtureStat mixtureStat = selectedObject.GetComponent<Mixture>().mixtureStat;
        //                //mixtureStat.UpdateSharedStats(selectedTool.toolName);
        //            }
        //            // Compare stats
        //            //CompareStats(clickedObject);
        //        }
        //        else
        //        {
        //            //Debug.Log("The clicked object is not an InteractionObject.");
        //        }
        //    }
        //    else
        //    {
        //        //Debug.Log("No object selected or ray didn't hit anything");
        //    }
        //}
    }

    private void ApplyToolEffect(Tool tool)
    {
        if(selectedObject != null)
        {
            tool.ApplyEffect();
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


