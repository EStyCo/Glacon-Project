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
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
        SelectManager.Instance.isPaused = true;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        SelectManager.Instance.isPaused = false;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        // RESTART
    }
}
