using UnityEngine;

public class PlayerSpawnSetter : MonoBehaviour
{
    void Awake()
    {
        if (SceneData.useCustomSpawn)
        {
            transform.SetPositionAndRotation(SceneData.spawnPosition, SceneData.spawnRotation);
            SceneData.useCustomSpawn = true; // Clear flag after applying
        }
        else
        {
            GameObject fallbackSpawn = GameObject.FindWithTag("PlayerSpawn");
            if (fallbackSpawn != null)
            {
                transform.position = fallbackSpawn.transform.position;
                transform.rotation = fallbackSpawn.transform.rotation;
            }
        }
    }
}
