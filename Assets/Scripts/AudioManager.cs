using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("-------- Audio Sources --------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("-------- Audio Clips --------")]
    public AudioClip[] backgroundMusicClips; // Array of background music clips
    public AudioClip pray;
    public AudioClip[] upgradeClips; // Array of upgrade audio clips
    public AudioClip[] collectableClips; // Array of Collectable Clips
    public AudioClip[] multiplierClips; // Array of Multiplier Clips
                                        // Add variables to store the previous audio states
    private bool musicWasPlayingBeforePause;
    private bool sfxWasPlayingBeforePause;

    // Add a flag to track whether the audio is currently paused
    private bool isPaused = false;

    private int currentBackgroundMusicIndex = 0; // Index of the currently playing background music

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Play the initial background music clip
        PlayBackgroundMusic(0);
    }

    // Method to play background music by index
    public void PlayBackgroundMusic(int index)
    {
        if (index >= 0 && index < backgroundMusicClips.Length)
        {
            musicSource.clip = backgroundMusicClips[index];
            musicSource.Play();
            currentBackgroundMusicIndex = index;

            // Start coroutine to switch to the next background music clip after the current one finishes
            StartCoroutine(PlayNextBackgroundMusic());
        }
        else
        {
            Debug.LogWarning("Invalid background music index.");
        }
    }

    // Coroutine to switch to the next background music clip after the current one finishes
    private IEnumerator PlayNextBackgroundMusic()
    {
        // Wait for the current background music clip to finish playing
        yield return new WaitForSeconds(musicSource.clip.length);

        // Check if the current background music clip is the last one in the array
        if (currentBackgroundMusicIndex == backgroundMusicClips.Length - 1)
        {
            // If it is the last one, switch to the first background music clip
            PlayBackgroundMusic(0);
        }
        else
        {
            // Otherwise, switch to the next background music clip
            NextBackgroundMusic();
        }
    }

    // Method to play a specific collectable audio clip
    public void PlayCollectableClip(int index)
    {
        if (index >= 0 && index < collectableClips.Length)
        {
            sfxSource.PlayOneShot(collectableClips[index]);
        }
        else
        {
            Debug.LogWarning("Invalid collectable audio clip index.");
        }
    }

    // Method to play a specific upgrade audio clip
    public void PlayUpgradeClip(int index)
    {
        if (index >= 0 && index < upgradeClips.Length)
        {
            sfxSource.PlayOneShot(upgradeClips[index]);
        }
        else
        {
            Debug.LogWarning("Invalid upgrade audio clip index.");
        }
    }

    // Method to play a specific upgrade audio clip
    public void PlayMultiplierClip(int index)
    {
        if (index >= 0 && index < multiplierClips.Length)
        {
            sfxSource.PlayOneShot(multiplierClips[index]);
        }
        else
        {
            Debug.LogWarning("Invalid multiplier audio clip index.");
        }
    }

    // Method to play the pray audio clip
    public void PlayPrayClip()
    {
        sfxSource.PlayOneShot(pray);
    }

    // Method to switch to the next background music clip
    public void NextBackgroundMusic()
    {
        currentBackgroundMusicIndex = (currentBackgroundMusicIndex + 1) % backgroundMusicClips.Length;
        PlayBackgroundMusic(currentBackgroundMusicIndex);
    }

    // Method to switch to the previous background music clip
    public void PreviousBackgroundMusic()
    {
        currentBackgroundMusicIndex = (currentBackgroundMusicIndex - 1 + backgroundMusicClips.Length) % backgroundMusicClips.Length;
        PlayBackgroundMusic(currentBackgroundMusicIndex);
    }

    // Method to pause audio playback
    public void PauseAudio()
    {
        if (!isPaused)
        {
            // Store the current playback states
            musicWasPlayingBeforePause = musicSource.isPlaying;
            sfxWasPlayingBeforePause = sfxSource.isPlaying;

            // Pause the audio sources
            musicSource.Pause();
            sfxSource.Pause();

            // Set the paused flag to true
            isPaused = true;
        }
    }

    // Method to resume audio playback
    public void ResumeAudio()
    {
        if (isPaused)
        {
            // Resume the audio sources if they were playing before pausing
            if (musicWasPlayingBeforePause)
            {
                musicSource.Play();
            }
            if (sfxWasPlayingBeforePause)
            {
                sfxSource.Play();
            }

            // Reset the playback states
            musicWasPlayingBeforePause = false;
            sfxWasPlayingBeforePause = false;

            // Set the paused flag to false
            isPaused = false;
        }
    }
}
