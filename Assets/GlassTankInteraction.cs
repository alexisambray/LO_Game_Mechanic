using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro

public class GlassTankInteraction : MonoBehaviour
{
    // Reference to the text that will display results
    public TMP_Text resultText; // Reintroduce this for TextMeshPro UI

    private bool isProcessing = false;

    // On object click
    private void OnMouseDown()
    {
        if (!isProcessing)
        {
            StartCoroutine(CentrifugeProcess());
        }
    }

    IEnumerator CentrifugeProcess()
    {
        isProcessing = true;

        // Display "Mixing..." while processing
        resultText.text = "Mixing...";
        Debug.Log("Mixing... text set");

        // Simulate analysis process
        yield return new WaitForSeconds(3f);

        // Display the analysis result
        resultText.text = "Analysis Complete: Homogeneous.";
        Debug.Log("Analysis Complete text set");

        isProcessing = false;
    }
}
