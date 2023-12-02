using System.Collections;
using UnityEngine;

public class SandBox : GameState
{
    protected override void LoseGame()
    {
        endGameObject.SetActive(true);

        endGameWindow.EndGameSandBox(false);

        StopCoroutine(winGame);
    }

    protected override void WinGame()
    {
        endGameObject.SetActive(true);

        endGameWindow.EndGameSandBox(true);

        selectManager.isPaused = true;
        StopCoroutine(loseGame);
    }
}
