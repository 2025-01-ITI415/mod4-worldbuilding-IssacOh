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
    [SerializeField] private float throwUpwardForce = 3f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip beforePickupClip;
    [SerializeField] private AudioClip afterPickupClip;
    [SerializeField] private AudioClip throwClip;

    public GameObject promptUI;

    private bool isHeld = false;
    private Transform playerCamera;
    private Transform holdOffset;
    private Rigidbody rb;
    private Collider treasureCollider;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        
    }

    void Update()
    {
        if (isHeld && holdOffset != null)
        {
            transform.position = Vector3.Lerp(transform.position, holdOffset.position, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, holdOffset.rotation, followSpeed * Time.deltaTime);
            promptUI.SetActive(false);
        }

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

            // Audio: stop before sound, play after sound
            if (audioSource != null)
            {
                audioSource.Stop();
                if (afterPickupClip != null)
                {
                    audioSource.clip = afterPickupClip;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            if (treasureCollider != null)
                treasureCollider.enabled = false;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isHeld)
        {
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
        isHeld = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 throwDirection = playerCamera.forward;
            rb.velocity = throwDirection * throwForce + Vector3.up * throwUpwardForce;
        }

        if (treasureCollider != null)
            treasureCollider.enabled = true;

        if (audioSource != null && throwClip != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.PlayOneShot(throwClip);
        }
    }
    
}
