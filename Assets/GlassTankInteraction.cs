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
        Debug.Log("Waiting to show use category buttons...");
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        resultText.text = "What is the use category?";
        useCategoryButtons.SetActive(true); // Show the use category buttons
        Debug.Log("Use category buttons are now shown.");
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

    // New GuessHealthCleaning method
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
            resultText.text = "Correct category!";
            // Additional logic for correct category guess can be added here
        }
        else
        {
            resultText.text = "Wrong category. Try again!";
        }
    }

    // Set correct use category for the current mixture
    public void SetCorrectUseCategory(string category)
    {
        correctUseCategory = category; // Call this method before the centrifuge starts
    }
}
