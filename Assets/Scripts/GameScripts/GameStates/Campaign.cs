using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Campaign : GameState
{
    [Inject] private ProgressPlayer player;

    protected override void LoseGame()
    {
        endGameObject.SetActive(true);
        endGameWindow.EndGameCampaign(false);

        StopCoroutine(winGame);
    }

    protected override void WinGame()
    {
        endGameObject.SetActive(true);
        endGameWindow.EndGameCampaign(true);

        player.CompletedLevel();

        selectManager.isPaused = true;
        StopCoroutine(loseGame);
    }
}
