using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    public string sceneToLoad;                 // The name of the scene to load
    public Vector3 spawnPositionInNewScene;    // Position to spawn the player in the new scene

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // If the player touches the trigger
        {
            // Store the desired spawn position
            SceneData.spawnPosition = spawnPositionInNewScene;
            
            // Load the scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}