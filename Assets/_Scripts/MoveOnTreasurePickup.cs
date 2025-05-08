using UnityEngine;

public class MoveOnTreasurePickup : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 moveOffset = new Vector3(0, 2, 0);
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool hasMoved = false;
    private bool movementStarted = false;

    void Update()
    {
        if (!hasMoved && PickupTreasure.TreasurePickedUp)
        {
            targetPosition = transform.position + moveOffset;
            movementStarted = true;
            hasMoved = true;
        }

        if (movementStarted)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Stop when reached
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movementStarted = false;
            }
        }
    }
}