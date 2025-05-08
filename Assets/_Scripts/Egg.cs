using UnityEngine;
using TMPro;

public class Egg : MonoBehaviour
{
    public GameObject promptUI; // The UI prompt that appears when the player is near the egg
    private TextMeshProUGUI promptText; // The prompt's text component
    public int eggID; // A unique identifier for each egg

    void Start()
    {
        promptText = promptUI.GetComponentInChildren<TextMeshProUGUI>(); // Get the prompt text component
        promptUI.SetActive(false); // Initially hide the prompt
    }

    void Update()
    {
        // If the egg is already collected, don't display prompt
        if (GameManager.Instance.IsEggCollected(eggID)) return;

        // Display the prompt if the player is close enough
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance <= 5f)
        {
            ShowPrompt();
        }
        else
        {
            HidePrompt();
        }
    }


    public void Collect()
    {
        GameManager.Instance.CollectEgg(eggID); // Notify the GameManager that this egg has been collected
        HidePrompt(); // Hide the prompt
        gameObject.SetActive(false); // Deactivate the egg object
    }

    public void ShowPrompt()
    {
        promptUI.SetActive(true); // Show the prompt UI when within range
    }

    public void HidePrompt()
    {
        promptUI.SetActive(false); // Hide the prompt UI if out of range
    }
}
