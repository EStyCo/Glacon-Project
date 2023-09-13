using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioClip[] musicClips; // Массив с вашими композициями
    public AudioSource audioSource;
    public float volume;

    private void Awake()
    {
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
        StartMusic();
    }
    public void StartMusic()
    {
        audioSource = GetComponent<AudioSource>();

        int randomIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[randomIndex];

        if ( audioSource.isPlaying && musicClips[randomIndex])
        {
            audioSource.Stop();
            StartMusic();
        }

        volume = audioSource.volume;
        audioSource.Play();
    }
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = volume;
    }
    public void NextSong()
    { 

    }
}
