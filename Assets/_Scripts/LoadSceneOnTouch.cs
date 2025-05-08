using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnPositionInNewScene;
    public Vector3 spawnRotationEuler; // Use inspector to input e.g., (0, 90, 0)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneData.spawnPosition = spawnPositionInNewScene;
            SceneData.spawnRotation = Quaternion.Euler(spawnRotationEuler);
            SceneData.useCustomSpawn = true;
            SceneManager.LoadScene(sceneToLoad);
            Time.timeScale = 1f;
        }
    }
}
