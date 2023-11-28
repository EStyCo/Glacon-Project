using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIMainMusic : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private MusicManager musicManager;

    private AudioSource musicSource;

    [SerializeField] private Slider slider;

    void Start()
    {
        musicSource = musicManager.GetComponent<AudioSource>();
        musicSource.volume = gameManager.volumeCount;
        slider.value = gameManager.volumeCount;
    }

    public void SetVolume()
    {
        float count = slider.value;

        musicSource.volume = count;
        gameManager.SaveVolume(count);
    }

    public void SoundlessMusic(bool isTrue)
    {
        if (isTrue)
        {
            musicSource.volume = 0;
            gameManager.SaveVolume(0);
        }
        else
        {
            musicSource.volume = 1;
            gameManager.SaveVolume(1);
        }
    }
}
