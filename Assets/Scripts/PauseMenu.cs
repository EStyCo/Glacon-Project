using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPaused)
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
        GameManager.Instance.isPaused = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true); 
        GameManager.Instance.isPaused = true;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.isPaused = false;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        // RESTART
    }
}
