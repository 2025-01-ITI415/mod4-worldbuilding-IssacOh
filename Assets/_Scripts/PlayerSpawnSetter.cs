using UnityEngine;

public class PlayerSpawnSetter : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Spawning player at: " + SceneData.spawnPosition);
        transform.position = SceneData.spawnPosition;
    }
}