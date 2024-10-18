using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtureSelector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log($"Hit Object: {hit.collider.gameObject.name}");
                if (hit.collider.gameObject.CompareTag("Mixture"))
                {
                    // Use the GameManager to select the object
                    GameManager.Instance.SelectObject(hit.collider.gameObject);
                }
                else
                {
                    //Debug.Log("Hit object is not a Mixture.");
                }
            }
        }
        else
        {
           // Debug.Log("No objects hit by the raycast.");
        }
    }
}
