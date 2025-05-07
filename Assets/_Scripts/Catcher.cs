using UnityEngine;

public class Catcher : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 lookDirection = Vector3.forward;
    public Transform visualToRotate;
    public float cooldownDuration = 1f;
    public AudioClip teleportSound; // Assign your sound here

    private AudioSource audioSource;
    private bool isCooldown = false;

    private void Awake()
    {
        // Try to get an AudioSource attached to this object, or add one
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCooldown && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithCooldown(other));
        }
    }

    private System.Collections.IEnumerator TeleportWithCooldown(Collider player)
    {
        isCooldown = true;

        // Play sound
        if (teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = targetPosition;

        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection.normalized);
            if (visualToRotate != null)
                visualToRotate.rotation = targetRotation;
            else
                player.transform.rotation = targetRotation;
        }

        yield return null;

        if (cc != null) cc.enabled = true;

        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
    }
}