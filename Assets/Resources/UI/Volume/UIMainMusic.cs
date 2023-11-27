using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIMainMusic : MonoBehaviour
{
    [Inject] GameManager gameManager;

    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private Slider slider;

    void Start()
    {
        audioMusic.volume = gameManager.volumeCount;
        slider.value = gameManager.volumeCount;
    }

    public void SetVolume()
    {
        float count = slider.value;

        audioMusic.volume = count;
        gameManager.SaveVolume(count);
    }
}
