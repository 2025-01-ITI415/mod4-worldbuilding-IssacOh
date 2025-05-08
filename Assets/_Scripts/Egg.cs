using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject promptUI;
    public bool IsCollected { get; private set; } = false;

    public void Activate()
    {
        if (!IsCollected)
            promptUI.SetActive(true); // Show prompt when near
    }

    public void Deactivate()
    {
        promptUI.SetActive(false); // Hide when far
    }

    public void Collect()
    {
        IsCollected = true;
        promptUI.SetActive(false);
        gameObject.SetActive(false); // Or Destroy(gameObject);
        // You can add particle effect, sound, etc. here
    }
}
