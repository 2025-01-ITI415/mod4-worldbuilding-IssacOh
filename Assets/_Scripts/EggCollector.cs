using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggCollector : MonoBehaviour
{
    public Text countText; // UI reference for counting collected eggs
    private Egg currentNearbyEgg; // The egg currently detected by the player
    private float detectionDistance = 5f; // Distance within which the player can detect an egg

    void Start()
    {
        // Update the UI text when the game starts
        UpdateCountText();
    }

    void Update()
    {
        // Raycast to detect the egg within the specified range
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, detectionDistance)) // Check for hit within detection distance
        {
            Egg egg = hit.collider.GetComponent<Egg>(); // Get the Egg component from the hit object
            if (egg != null && !GameManager.Instance.IsEggCollected(egg.eggID)) // Check if egg is not collected yet
            {
                // If we are looking at a new egg, show the prompt UI
                if (currentNearbyEgg != egg)
                {
                    if (currentNearbyEgg != null)
                        currentNearbyEgg.promptUI.SetActive(false); // Hide prompt for the previously detected egg

                    egg.promptUI.SetActive(true); // Show prompt for the newly detected egg
                    currentNearbyEgg = egg; // Update the current nearby egg
                }
            }
        }
        else if (currentNearbyEgg != null)
        {
            // If not looking at any egg, hide the prompt for the previous egg
            currentNearbyEgg.promptUI.SetActive(false);
            currentNearbyEgg = null; // Reset the nearby egg reference
        }

        // Collect the egg if the player presses 'E' and an egg is nearby
        if (currentNearbyEgg != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyEgg.Collect(); // Call the Collect method to mark the egg as collected
            currentNearbyEgg = null; // Reset the nearby egg reference
            UpdateCountText(); // Update the UI counter after collection
        }
    }

    // Update the UI count text based on the total collected eggs
    void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = $"Eggs Collected: {GameManager.Instance.TotalEggsCollected}/3"; // Display collected eggs out of the total (3 for this case)
        }
        else
        {
            Debug.LogError("countText is not assigned in the Inspector!"); // Error if the UI text is not assigned
        }
    }
}
