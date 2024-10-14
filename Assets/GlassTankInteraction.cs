using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlassTankInteraction : MonoBehaviour
{
    // Reference to the text that will display results
    public TMP_Text resultText;
    public Button homogeneousButton;
    public Button heterogeneousButton;
    public GameObject useCategoryButtons;
    public GameObject shelfItem; // Example: Vinegar item to be unlocked

    private string correctUseCategory = "Food"; // Correct use category for vinegar
    private bool isProcessing = false; // Prevents clicking multiple times
    private bool appearanceGuessed = false; // To prevent re-asking for appearance
    private string guessedAppearance = "";
    private bool dfaAnalysisStarted = false; // Prevent re-clicking DFA

    // Days required for DFA to process each category
    private int daysToCheck = 0;

    // On object click (start mixing process)
    private void OnMouseDown()
    {
        if (!isProcessing && !dfaAnalysisStarted)
        {
            // Start the centrifuge process
            StartCoroutine(CentrifugeProcess());
        }
    }

    // Coroutine to simulate the centrifuge process
    IEnumerator CentrifugeProcess()
    {
        isProcessing = true;
        appearanceGuessed = false; // Reset appearance guess

        // Start mixing
        resultText.text = "Mixing...";
        yield return new WaitForSeconds(3f); // Simulate mixing

        // Ask for the appearance (homogeneous or heterogeneous)
        ShowAppearanceButtons();
    }

    // Show the appearance guessing buttons
    void ShowAppearanceButtons()
    {
        resultText.text = "What is the appearance?";
        homogeneousButton.gameObject.SetActive(true);
        heterogeneousButton.gameObject.SetActive(true);
    }

    // Handle homogeneous button click
    public void OnHomogeneousButtonClick()
    {
        if (!appearanceGuessed)
        {
            guessedAppearance = "homogeneous";
            ProcessAppearanceGuess();
        }
    }

    // Handle heterogeneous button click
    public void OnHeterogeneousButtonClick()
    {
        if (!appearanceGuessed)
        {
            guessedAppearance = "heterogeneous";
            ProcessAppearanceGuess();
        }
    }

    // Process the guessed appearance
    void ProcessAppearanceGuess()
    {
        // Hide the appearance guessing buttons
        homogeneousButton.gameObject.SetActive(false);
        heterogeneousButton.gameObject.SetActive(false);

        // Mark that the appearance has been guessed
        appearanceGuessed = true;

        // Show the result of appearance guessing
        resultText.text = $"Analysis Complete: The mixture is {guessedAppearance}.";

        // Wait for a short moment before showing the use category buttons
        StartCoroutine(ShowUseCategoryButtons());
    }

    // Coroutine to delay showing the use category buttons
    IEnumerator ShowUseCategoryButtons()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        resultText.text = "Send item to DFA for analysis.";
        yield return new WaitForSeconds(2f); // Simulate sending item to DFA
        resultText.text = "What is the use category?";
        useCategoryButtons.SetActive(true); // Show the use category buttons
        isProcessing = false; // Allow further interactions after this stage
    }

    // Category guess functions with associated days for DFA processing
    public void GuessFood()
    {
        daysToCheck = 3; // Food takes 3 days
        CheckUseCategoryGuess("Food");
    }

    public void GuessMedicine()
    {
        daysToCheck = 4; // Medicine takes 4 days
        CheckUseCategoryGuess("Medicine");
    }

    public void GuessCosmetics()
    {
        daysToCheck = 1; // Cosmetics takes 1 day
        CheckUseCategoryGuess("Cosmetics");
    }

    public void GuessPersonalHygiene()
    {
        daysToCheck = 5; // Personal hygiene takes 5 days
        CheckUseCategoryGuess("Personal Hygiene");
    }

    public void GuessAgriculture()
    {
        daysToCheck = 6; // Agriculture takes 6 days
        CheckUseCategoryGuess("Agriculture");
    }

    public void GuessHealthCleaning()
    {
        daysToCheck = 2; // Health cleaning takes 2 days
        CheckUseCategoryGuess("Health Cleaning");
    }

    // Function to check if the use category guess is correct
    private void CheckUseCategoryGuess(string guessedCategory)
    {
        // Hide use category buttons after guess
        useCategoryButtons.SetActive(false);

        // Check if the guess matches the correct use category
        if (guessedCategory == correctUseCategory)
        {
            resultText.text = $"Item sent to DFA for approval. Checking takes {daysToCheck} days.";
            StartCoroutine(WaitForDFAApproval());
        }
        else
        {
            resultText.text = "Wrong category. Try again!";
        }
    }

    // Coroutine for DFA approval process
    IEnumerator WaitForDFAApproval()
    {
        dfaAnalysisStarted = true;
        yield return new WaitForSeconds(daysToCheck); // Simulate the DFA process
        resultText.text = "DFA item approved. Item is unlocked in the shelf!";
        UnlockItemOnShelf(); // Unlock the item
        dfaAnalysisStarted = false;
    }

    // Method to unlock the item on the shelf
    void UnlockItemOnShelf()
    {
        // Activate the shelf item
        if (shelfItem != null)
        {
            shelfItem.SetActive(true); // This will show the item on the shelf
        }
    }

    // Set correct use category for the current mixture
    public void SetCorrectUseCategory(string category)
    {
        correctUseCategory = category; // Call this method before the centrifuge starts
    }
}
