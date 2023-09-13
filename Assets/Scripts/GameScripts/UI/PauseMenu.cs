using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public UnityEngine.UI.Slider volumeSlider;

    public float tempVolume;

    private void Start()
    {
        volumeSlider.value = MusicManager.Instance.audioSource.volume;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SelectManager.Instance.isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        pauseMenu.SetActive(false); 
        SelectManager.Instance.isPaused = false;
        SoundManager.Instance.GoNoise();
    }

    public void PauseGame()
    {
        volumeSlider.value = MusicManager.Instance.audioSource.volume;

        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
        SelectManager.Instance.isPaused = true;
        SoundManager.Instance.GoNoise();
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        SelectManager.Instance.isPaused = false;
        Time.timeScale = 1f;
        SoundManager.Instance.GoNoise();
    }

    public void RestartGame()
    {
        ResumeGame();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        SoundManager.Instance.GoNoise();
    }

    public void UpdateVolume()
    {
        float newVolume = volumeSlider.value;
        MusicManager.Instance.SetVolume(newVolume);
    }
    public void Soundless()
    {
        if (MusicManager.Instance.audioSource.volume != 0)
        {
            tempVolume = MusicManager.Instance.audioSource.volume;
            MusicManager.Instance.audioSource.volume = 0;
        }
        else
        {
            MusicManager.Instance.audioSource.volume = tempVolume;
            volumeSlider.value = MusicManager.Instance.audioSource.volume;
        }
    }
    public void NextSong()
    {
        MusicManager.Instance.NextSong();
    }
}
