using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of the GameManager
    private List<int> collectedEggs = new List<int>(); // List to keep track of collected eggs
    public int TotalEggsCollected => collectedEggs.Count; // Property to get the total number of collected eggs

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instance
        }
    }

    // Mark an egg as collected by its unique ID
    public void CollectEgg(int eggID)
    {
        if (!collectedEggs.Contains(eggID)) // Check if egg hasn't been collected already
        {
            collectedEggs.Add(eggID);
        }
    }

    // Check if an egg has already been collected
    public bool IsEggCollected(int eggID)
    {
        return collectedEggs.Contains(eggID);
    }
}
