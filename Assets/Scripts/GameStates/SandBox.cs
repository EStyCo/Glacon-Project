using System.Collections;
using UnityEngine;

public class SandBox : GameState
{
    [Header("Windows")]
    [SerializeField] protected GameObject winGameWindow;
    [SerializeField] protected GameObject loseGameWindow;

    private Coroutine loseGame;
    private Coroutine winGame;

    public override void StartScript()
    {
        loseGame = StartCoroutine(CheckLoseGame());
        winGame = StartCoroutine(CheckWinGame());
    }

    protected override void CreateGameState()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator CheckLoseGame()
    {
        while (true)
        {
            bool isLife = false;

            foreach (GameObject planet in listPlanet)
            {
                if (planet.tag == "PlayerPlanet")
                {
                    isLife = true;
                    break;
                }
            }

            if (!isLife)
            {
                loseGameWindow.SetActive(true);
                StopCoroutine(winGame);
                break;
            }

            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator CheckWinGame()
    {
        while (true)
        {
            bool isWin = true;

            foreach (GameObject planet in listPlanet)
            {
                if (planet.tag == "NeutralPlanet") continue;

                if (!planet.CompareTag("PlayerPlanet"))
                {
                    isWin = false;
                    break;
                }
            }

            if (isWin)
            {
                winGameWindow.SetActive(true);
                selectManager.isPaused = true;
                StopCoroutine(loseGame);
                break;
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
