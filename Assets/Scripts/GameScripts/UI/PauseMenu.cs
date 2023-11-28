using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator menuAnimator;

    public UnityEngine.UI.Button buttonPause;

    public UnityEngine.UI.Slider volumeSlider;

    public float tempVolume;

    private void Start()
    {
        tempVolume = volumeSlider.value;
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
    public void ShowPauseMenu()
    {

    }
    public void ResumeGame()
    {
        menuAnimator.Play("UnShowPM");

        Time.timeScale = 1f;
        SelectManager.Instance.isPaused = false;
        SoundManager.Instance.GoNoise();
        buttonPause.enabled = true;
    }

    public void PauseGame()
    {
        menuAnimator.Play("ShowPM");

        StartCoroutine(SetTimeScale());

        SelectManager.Instance.isPaused = true;
        SoundManager.Instance.GoNoise();
        buttonPause.enabled = false;

    }
    IEnumerator SetTimeScale()
    {
        yield return new WaitForSeconds(1.05f);

        Time.timeScale = 0f;
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
        buttonPause.enabled = true;
    }
}
