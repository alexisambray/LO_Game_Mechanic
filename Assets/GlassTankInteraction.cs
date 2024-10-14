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
}
