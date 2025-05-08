using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;        
    public AudioClip[] audioClips;         
    public float minTimeBetweenPlays = 1f; 
    public float maxTimeBetweenPlays = 5f; 

    [Header("Initial Delay Settings")]
    public float initialDelay = 10f;           // Delay before first sound
    public bool useFadeIn = false;            // Toggle fade-in
    public float fadeDuration = 1f;           // Fade duration in seconds

    private float nextPlayTime;
    private bool hasStarted = false;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        nextPlayTime = Time.time + initialDelay;
    }

    void Update()
    {
        if (Time.time >= nextPlayTime)
        {
            PlayRandomClip();
        }
    }

    void PlayRandomClip()
    {
        if (audioClips.Length == 0) return;

        AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

        if (useFadeIn)
            StartCoroutine(FadeInAndPlay(randomClip));
        else
            audioSource.PlayOneShot(randomClip);

        nextPlayTime = Time.time + Random.Range(minTimeBetweenPlays, maxTimeBetweenPlays);
    }

    System.Collections.IEnumerator FadeInAndPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.Play();

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
    }
}
