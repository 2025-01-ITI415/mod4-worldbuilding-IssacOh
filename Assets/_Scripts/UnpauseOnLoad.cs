using UnityEngine;

public class UnpauseOnLoad : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f; // Ensure time is running
    }
    void Update()
{
    {
        Debug.Log("Is game paused? " + (Time.timeScale == 0));
    }
}
}