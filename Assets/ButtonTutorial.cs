using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ButtonTutorial : MonoBehaviour
{
    [Inject] private GameModeManager gmManager;

    public void ChangeGameMode()
    {
        gmManager.currentGameMode = GameModeManager.GameMode.Tutorial;
        gmManager.currentState = GameModeManager.State.Disable;

        SceneManager.LoadScene(2);
    }
}
