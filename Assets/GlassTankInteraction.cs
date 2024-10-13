using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro text
using UnityEngine.UI; // For buttons

public class GlassTankInteraction : MonoBehaviour
{
    // Reference to the result text
    public TMP_Text resultText;

    // References to the buttons
    public Button foodButton;
    public Button medicineButton;
    public Button healthCleaningButton;
    public Button cosmeticsButton;
    public Button personalHygieneButton;
    public Button agricultureButton;

    // Prevent clicking while processing
    private bool isProcessing = false;

    // On object click
    private void OnMouseDown()
    {
        if (!isProcessing)
        {
            // Start the analysis coroutine
            StartCoroutine(CentrifugeProcess());
        }
    }

    // Coroutine to simulate the centrifuge process
    IEnumerator CentrifugeProcess()
    {
        isProcessing = true;

        // Display "Mixing..." during the centrifuge process
        resultText.text = "Mixing...";

        // Simulate analysis process (wait 3 seconds)
        yield return new WaitForSeconds(3f);

        // After processing, display the result
        resultText.text = "Analysis Complete: The mixture is homogeneous.";

        // Show the buttons for the player to guess the use
        ShowUseButtons();

        isProcessing = false;
    }

    // Function to activate the buttons
    void ShowUseButtons()
    {
        foodButton.gameObject.SetActive(true);
        medicineButton.gameObject.SetActive(true);
        healthCleaningButton.gameObject.SetActive(true);
        cosmeticsButton.gameObject.SetActive(true);
        personalHygieneButton.gameObject.SetActive(true);
        agricultureButton.gameObject.SetActive(true);
    }
}
