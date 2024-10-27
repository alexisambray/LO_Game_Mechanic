using System.Collections;
using System.Collections.Generic;
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

    // New Variables for Inventory Control
    public GameObject inventoryCanvas; // Reference to the Inventory Canvas

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

        // Ensure the inventory canvas is inactive at the start
        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(false); // Set it to inactive initially
        }
        else
        {
            Debug.LogError("Inventory canvas reference not set in the inspector.");
        }

        InstantiateRandomMixture();
    }

    private void Update()
    {
        HandleInteraction();

        // Toggle inventory visibility with the "i" key
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryCanvas != null)
            {
                inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
                Debug.Log($"Inventory Canvas set to: {inventoryCanvas.activeSelf}");
            }
        }
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

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
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
    }

    private void ApplyToolEffect(Tool tool)
    {
        if (selectedObject != null)
        {
            tool.ApplyEffect();
        }
        else
        {
            Debug.Log("No object selected to apply the tool effect.");
        }
    }
}
