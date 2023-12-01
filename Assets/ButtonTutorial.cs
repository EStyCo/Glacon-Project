using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ButtonTutorial : MonoBehaviour
{
    [Inject] private GameModeManager gameModeManager;

    public void ChangeGameMode()
    {
        gameModeManager.currentGameMode = GameModeManager.GameMode.Tutorial;
        SceneManager.LoadScene(2);
    }
}
