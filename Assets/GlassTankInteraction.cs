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

    // Reference for items on the shelf (to be unlocked after correct guess)
    public GameObject shelfItem; // Example: Coffee item to be unlocked
    private string correctUseCategory = "Food"; // Default correct use category, update it dynamically as needed

    private bool isProcessing = false; // Prevents clicking multiple times
    private bool appearanceGuessed = false; // To prevent re-asking for appearance
    private string guessedAppearance = "";

    // On object click (start mixing process)
    private void OnMouseDown()
    {
        if (!isProcessing)
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
        // Hide the buttons
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
        resultText.text = "Sending item to DFA for analysis...";
        yield return new WaitForSeconds(3f); // Simulate sending item to DFA

        resultText.text = "What is the use category?";
        useCategoryButtons.SetActive(true); // Show the use category buttons
        isProcessing = false; // Allow further interactions after this stage
    }

    // Category guess functions
    public void GuessFood()
    {
        CheckUseCategoryGuess("Food");
    }

    public void GuessMedicine()
    {
        CheckUseCategoryGuess("Medicine");
    }

    public void GuessCosmetics()
    {
        CheckUseCategoryGuess("Cosmetics");
    }

    public void GuessPersonalHygiene()
    {
        CheckUseCategoryGuess("Personal Hygiene");
    }

    public void GuessAgriculture()
    {
        CheckUseCategoryGuess("Agriculture");
    }

    public void GuessHealthCleaning()
    {
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
            resultText.text = "DFA item approved. Item is unlocked in the shelf!";
            UnlockItemOnShelf(); // Unlock item in the shelf
        }
        else
        {
            resultText.text = "Wrong category. Try again!";
        }
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
