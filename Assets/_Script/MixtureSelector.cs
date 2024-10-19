using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MixtureSelector : MonoBehaviour
{
    // Update is called once per frame
    //for 2D Sprites
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

    //        if (hit.collider != null)
    //        {
    //            //Debug.Log($"Hit Object: {hit.collider.gameObject.name}");
    //            if (hit.collider.gameObject.CompareTag("Mixture"))
    //            {
    //                // Use the GameManager to select the object
    //                GameManager.Instance.SelectObject(hit.collider.gameObject);
    //            }
    //            else
    //            {
    //                Debug.Log("Hit object is not a Mixture.");
    //            }
    //        }
    //    }
    //    else
    //    {
    //       // Debug.Log("No objects hit by the raycast.");
    //    }
    //}

    public GraphicRaycaster raycaster; // Reference to the canvas's GraphicRaycaster
    public EventSystem eventSystem; // Reference to the EventSystem

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Set up the PointerEventData with the current mouse position
            PointerEventData pointerEventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            // Create a list of RaycastResults to store the results
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast using the GraphicRaycaster and mouse click position
            raycaster.Raycast(pointerEventData, results);

            // Process the raycast results
            foreach (RaycastResult result in results)
            {
                GameObject hitObject = result.gameObject;

                // Check if the hit object is a Mixture (using the correct tag)
                if (hitObject.CompareTag("Mixture"))
                {
                    // Use the GameManager to select the object
                    GameManager.Instance.SelectObject(hitObject);
                    //Debug.Log($"Hit Object: {hitObject.name}");
                    break;
                }
                else
                {
                    //Debug.Log("Hit object is not a Mixture.");
                }
            }
        }
    }
}
