using UnityEngine;
using System.Collections;

public class MoveAfterDelay : MonoBehaviour
{
    public float delay = 3f;                 // Time before moving
    public float moveDuration = 2f;          // How long the move takes
    public Vector3 moveOffset = new Vector3(0, 5, 0);  // Where to move

    private bool hasMoved = false;

    void Start()
    {
        StartCoroutine(MoveAfterDelayCoroutine());
    }

    IEnumerator MoveAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);  // Wait before moving

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + moveOffset;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            yield return null;
        }

        transform.position = endPos;  // Ensure it ends exactly at the target
        hasMoved = true;
    }
}
