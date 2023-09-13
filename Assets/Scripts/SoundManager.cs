using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;
    public AudioClip destroyNoise;
    public AudioClip goNoise;

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
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayNoise()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        else
        {
            audioSource.clip = destroyNoise;
            audioSource.Play();
        }
    }
    public void GoNoise()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        else
        {
            audioSource.clip = goNoise;
            audioSource.Play();
        }
    }
}
