using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro text

public class GlassTankInteraction : MonoBehaviour
{
    public TMP_Text resultText; // Reference to the text that will display results
    private bool isProcessing = false; // Prevent clicking while processing
    private int daysToWait = 2; // Placeholder for in-game days waiting for results

    // On object click
    private void OnMouseDown()
    {
        if (!isProcessing)
        {
            StartCoroutine(CentrifugeProcess());
        }
    }

    // Coroutine to simulate the centrifuge process
    IEnumerator CentrifugeProcess()
    {
        isProcessing = true;

        // Show that the centrifuge is processing the mixture
        resultText.text = "Centrifuge: Analyzing mixture...";
        yield return new WaitForSeconds(3f); // Simulate processing time (3 seconds)

        // Allow player to guess appearance after centrifuge analysis
        resultText.text = "Centrifuge Complete: Guess the mixture's appearance.";
        isProcessing = false;

        // Now let the player guess the appearance (homogeneous/heterogeneous)
        // You could implement buttons for guessing or handle user input in another way
        // For simplicity, we can assume a guess function:
        GuessAppearance();
    }

    // Placeholder function to simulate the guessing system
    void GuessAppearance()
    {
        // Let’s assume the player guesses "homogeneous" for now (this would normally be user input)
        string playerGuess = "homogeneous";
        CheckAppearanceGuess(playerGuess);
    }

    // Function to check if the player's guess was correct
    void CheckAppearanceGuess(string guess)
    {
        string correctAnswer = "homogeneous"; // In reality, this would vary per mixture
        if (guess == correctAnswer)
        {
            resultText.text = "Correct! Now send the mixture to DFA for further analysis.";
            // Now the player must guess the use (Food/Beverage, etc.)
            StartCoroutine(SendToDFA());
        }
        else
        {
            resultText.text = "Incorrect guess. Try again.";
        }
    }

    // Simulate sending the mixture to DFA for checking the use
    IEnumerator SendToDFA()
    {
        isProcessing = true;
        resultText.text = "Sending to DFA for analysis...";
        yield return new WaitForSeconds(daysToWait); // Simulate waiting for DFA results (2 days)

        resultText.text = "DFA Results: The mixture is used for Food/Beverage.";
        // Unlock the object in the shelf, for example
        UnlockShelfObject();
        isProcessing = false;
    }

    // Function to unlock the object on the shelf after correct appearance and use guesses
    void UnlockShelfObject()
    {
        resultText.text = "Object unlocked on the shelf!";
        // Here you would handle the logic to display the unlocked item in the shelf
    }
}
