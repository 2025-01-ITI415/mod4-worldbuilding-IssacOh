using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupTreasure : MonoBehaviour
{
    public static bool TreasurePickedUp = false;

    [Header("Treasure Hold Settings")]
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private Vector3 holdOffsetLocalPosition = new Vector3(0, -0.2f, 1.5f);
    [SerializeField] private Vector3 cameraOffsetOnPickup = new Vector3(0, 0, -0.3f);

    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwUpwardForce = 3f;  // Additional upward velocity for better arc

    private bool isHeld = false;
    private Transform playerCamera;
    private Transform holdOffset;
    private Rigidbody rb;
    private Collider treasureCollider;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        treasureCollider = GetComponent<Collider>();
        
        if (rb != null)
        {
            rb.isKinematic = true; // Initially, make it kinematic when held
            rb.useGravity = false; // Disable gravity while held
        }
    }

    void Update()
    {
        if (isHeld && holdOffset != null)
        {
            transform.position = Vector3.Lerp(transform.position, holdOffset.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, holdOffset.rotation, followSpeed * Time.deltaTime);
        }

        // Press Q to throw the treasure
        if (isHeld && Input.GetKeyDown(KeyCode.Q))
        {
            ThrowTreasure();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isHeld && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            playerCamera = other.GetComponentInChildren<Camera>().transform;

            if (playerCamera != null)
                playerCamera.localPosition += cameraOffsetOnPickup;

            SetupHoldOffset(playerCamera);

            isHeld = true;
            TreasurePickedUp = true;

            if (rb != null)
            {
                rb.isKinematic = true;  // Keep it kinematic when held (no physics interaction)
                rb.useGravity = false;  // Disable gravity when held
            }

            if (treasureCollider != null)
                treasureCollider.enabled = false;  // Disable collider to avoid interaction with other objects

            DontDestroyOnLoad(gameObject);  // Keeps treasure across scenes
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isHeld)
        {
            // Find new player camera in new scene
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerCamera = player.GetComponentInChildren<Camera>().transform;
                SetupHoldOffset(playerCamera);
            }
        }
    }

    private void SetupHoldOffset(Transform newCamera)
    {
        if (holdOffset == null)
        {
            GameObject holdPoint = new GameObject("TreasureHoldOffset");
            holdOffset = holdPoint.transform;
        }

        holdOffset.SetParent(newCamera);
        holdOffset.localPosition = holdOffsetLocalPosition;
        holdOffset.localRotation = Quaternion.identity;
    }

    private void ThrowTreasure()
    {
        // Remove the hold and make it physics-based again
        isHeld = false;
        rb.isKinematic = false;  // Allow physics to take over (no longer kinematic)
        rb.useGravity = true;    // Enable gravity so it can fall naturally

        // Enable the collider again for proper interaction with the environment
        if (treasureCollider != null)
            treasureCollider.enabled = true;

        // Set the velocity for the throw (based on the camera's forward direction)
        Vector3 throwDirection = playerCamera.forward;

        // Apply velocity to the Rigidbody for throwing the treasure
        rb.velocity = throwDirection * throwForce + Vector3.up * throwUpwardForce;
    }
}
