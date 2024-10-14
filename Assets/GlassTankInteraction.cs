using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GlassTankInteraction : MonoBehaviour
{
    public TMP_Text resultText;
    public Button homogeneousButton;
    public Button heterogeneousButton;
    public GameObject useCategoryButtons;
    public GameObject shelfItems; // Parent GameObject for all shelf items

    private string correctUseCategory = ""; // Dynamically set
    private string correctAppearance = ""; // Dynamically set
    private string mixtureType = ""; // True Solution, Suspension, or Colloid

    private bool isProcessing = false;
    private bool appearanceGuessed = false;
    private string guessedAppearance = "";

    private bool dfaAnalysisStarted = false;

    private int daysToCheck = 0;

    // On object click (start mixing process)
    private void OnMouseDown()
    {
        if (!isProcessing && !dfaAnalysisStarted)
        {
            StartCoroutine(CentrifugeProcess());
        }
    }

    void ShowMixtureSprite(string itemName)
    {
        Transform mixtureItem = shelfItems.transform.Find(itemName);
        if (mixtureItem != null)
        {
            mixtureItem.gameObject.SetActive(true); // Show the sprite after mixing
        }
    }

    IEnumerator CentrifugeProcess()
    {
        isProcessing = true;
        appearanceGuessed = false; // Reset appearance guess
        dfaAnalysisStarted = false; // Reset DFA analysis flag

        resultText.text = "Mixing...";
        yield return new WaitForSeconds(3f); // Simulate mixing

        // Show the mixture sprite (Vinegar or other) after mixing
        ShowMixtureSprite("VinegarSprite"); // Pass the correct sprite name dynamically

        ShowAppearanceButtons(); // Ask for the appearance
    }

    void ShowAppearanceButtons()
    {
        resultText.text = "What is the appearance?";
        homogeneousButton.gameObject.SetActive(true);
        heterogeneousButton.gameObject.SetActive(true);
    }

    public void OnHomogeneousButtonClick()
    {
        if (!appearanceGuessed)
        {
            guessedAppearance = "homogeneous";
            ProcessAppearanceGuess();
        }
    }

    public void OnHeterogeneousButtonClick()
    {
        if (!appearanceGuessed)
        {
            guessedAppearance = "heterogeneous";
            ProcessAppearanceGuess();
        }
    }

    void ProcessAppearanceGuess()
    {
        homogeneousButton.gameObject.SetActive(false);
        heterogeneousButton.gameObject.SetActive(false);

        appearanceGuessed = true;

        // Correctly handle the appearance guess comparison
        if (guessedAppearance.Equals(correctAppearance))
        {
            resultText.text = $"Analysis Complete: The mixture is {guessedAppearance}.";
            StartCoroutine(ShowUseCategoryButtons()); // Wait and show use categories
        }
        else
        {
            resultText.text = "Wrong appearance guess. Try again.";
            StartCoroutine(AllowRetryAfterWrongAppearance());
        }
    }

    IEnumerator ShowUseCategoryButtons()
    {
        yield return new WaitForSeconds(2f);
        resultText.text = "Send item to DFA for analysis.";
        yield return new WaitForSeconds(2f);
        resultText.text = "What is the use category?";
        useCategoryButtons.SetActive(true);
        isProcessing = false;
    }

    IEnumerator AllowRetryAfterWrongAppearance()
    {
        yield return new WaitForSeconds(3f);
        resultText.text = "Try guessing the appearance again!";
        appearanceGuessed = false;
        ShowAppearanceButtons();
        isProcessing = false;
    }

    public void GuessFood()
    {
        daysToCheck = 3;
        CheckUseCategoryGuess("Food");
    }

    public void GuessMedicine()
    {
        daysToCheck = 4;
        CheckUseCategoryGuess("Medicine");
    }

    public void GuessCosmetics()
    {
        daysToCheck = 1;
        CheckUseCategoryGuess("Cosmetics");
    }

    public void GuessPersonalHygiene()
    {
        daysToCheck = 5;
        CheckUseCategoryGuess("Personal Hygiene");
    }

    public void GuessAgriculture()
    {
        daysToCheck = 6;
        CheckUseCategoryGuess("Agriculture");
    }

    public void GuessHealthCleaning()
    {
        daysToCheck = 2;
        CheckUseCategoryGuess("Health Cleaning");
    }

    private void CheckUseCategoryGuess(string guessedCategory)
    {
        useCategoryButtons.SetActive(false);

        if (guessedCategory == correctUseCategory)
        {
            resultText.text = $"Item sent to DFA for approval. Checking takes {daysToCheck} days.";
            dfaAnalysisStarted = true;
            StartCoroutine(WaitForDFAApproval("VinegarSprite")); // Pass the correct shelf item name here
        }
        else
        {
            resultText.text = "Wrong category. Try again!";
        }
    }

    IEnumerator WaitForDFAApproval(string itemName)
    {
        yield return new WaitForSeconds(daysToCheck);
        resultText.text = $"DFA item approved. The mixture is a {mixtureType}!";
        UnlockItemOnShelf(itemName); // Pass the itemName to unlock the correct shelf item
    }

    // Unlock the item based on the guessed mixture (name of the item to unlock)
    void UnlockItemOnShelf(string itemName)
    {
        Transform shelfItem = shelfItems.transform.Find(itemName); // Find the item by name in the shelfItems parent

        if (shelfItem != null)
        {
            shelfItem.gameObject.SetActive(true); // Activate the correct item on the shelf
        }
    }

    // Method to initialize mixture properties and start centrifuge
    public void StartCentrifugeForMixture(string appearance, string category, string type)
    {
        SetMixtureProperties(appearance, category, type);
        StartCoroutine(CentrifugeProcess());
    }

    void StartCentrifugeForVinegar()
    {
        SetMixtureProperties("homogeneous", "Food", "True Solution"); // Correct properties for vinegar
        StartCoroutine(CentrifugeProcess());
    }


    public void SetMixtureProperties(string appearance, string category, string type)
    {
        correctAppearance = appearance;
        correctUseCategory = category;
        mixtureType = type;
    }
}
