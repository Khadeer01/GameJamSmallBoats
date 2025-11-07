using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;      // Scene name (must match exactly)
        public AudioClip background;  // Music clip for that scene
    }

    [Header("Scene Music Settings")]
    public SceneMusic[] sceneMusics;   // Assign scene/music pairs here

    [Header("Transition Settings")]
    [Range(0f, 5f)] public float fadeDuration = 1.5f; // Duration of fade in/out
    [Range(0f, 1f)] public float musicVolume = 0.8f;  // Default volume

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // Singleton pattern to persist between scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = musicVolume;

        // Subscribe to scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Play music for the first loaded scene
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        foreach (SceneMusic sm in sceneMusics)
        {
            if (sm.sceneName == sceneName)
            {
                if (audioSource.clip != sm.background)
                {
                    if (fadeCoroutine != null)
                        StopCoroutine(fadeCoroutine);

                    fadeCoroutine = StartCoroutine(FadeToNewTrack(sm.background));
                }
                return;
            }
        }

        // No match found = stop playing
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutAndStop());
    }

    private IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        // Fade out current track
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();

        // Switch clip and fade in
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, musicVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = musicVolume;
    }

    private IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = musicVolume;
    }
    public static void StopMusic()
    {
        if (Instance?.audioSource.isPlaying == true)
            Instance.audioSource.Stop();
    }

    public static void PlayMusic()
    {
        if (Instance?.audioSource.isPlaying == false)
            Instance.audioSource.Play();
    }

    public static void ChangeVolume(float volume)
    {
        if (Instance != null)
            Instance.audioSource.volume = Mathf.Clamp01(volume);
    }
}
