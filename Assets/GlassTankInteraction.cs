using TMPro;
using UnityEngine;

using System.Collections;
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

    // Example: Correct use for the mixture (can be dynamic or come from an external source)
    private string correctUse = "Food/Beverage"; // Set this dynamically based on the mixture

    void Start()
    {
        // Initially hide the buttons
        HideUseButtons();

        // Add listeners for the buttons
        foodButton.onClick.AddListener(() => GuessUse("Food/Beverage"));
        medicineButton.onClick.AddListener(() => GuessUse("Medicine"));
        healthCleaningButton.onClick.AddListener(() => GuessUse("Health Cleaning"));
        cosmeticsButton.onClick.AddListener(() => GuessUse("Cosmetics"));
        personalHygieneButton.onClick.AddListener(() => GuessUse("Personal Hygiene"));
        agricultureButton.onClick.AddListener(() => GuessUse("Agriculture"));
    }

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

    // Function to handle the player's guess
    void GuessUse(string selectedUse)
    {
        // Hide buttons after selection
        HideUseButtons();

        // Check if the guessed use is correct
        if (selectedUse == correctUse)
        {
            resultText.text = "Correct! The mixture is used for " + correctUse + ".";
        }
        else
        {
            resultText.text = "Incorrect! The mixture is not used for " + selectedUse + ".";
        }

        // You can implement additional logic here, like sending the mixture for verification
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

    // Function to hide the buttons
    void HideUseButtons()
    {
        foodButton.gameObject.SetActive(false);
        medicineButton.gameObject.SetActive(false);
        healthCleaningButton.gameObject.SetActive(false);
        cosmeticsButton.gameObject.SetActive(false);
        personalHygieneButton.gameObject.SetActive(false);
        agricultureButton.gameObject.SetActive(false);
    }
}
