using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // Make the canvas face the camera
            Vector3 direction = transform.position - Camera.main.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
