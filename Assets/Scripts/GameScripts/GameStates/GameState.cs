using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] protected SelectManager selectManager;

    [Header("Windows")]
    [SerializeField] protected GameObject endGameObject;
    [SerializeField] protected EndGameWindow endGameWindow;

    public List<GameObject> listPlanet = new List<GameObject>();

    protected Coroutine loseGame;
    protected Coroutine winGame;

    public void GetPlanet(GameObject planet)
    {
        listPlanet.Add(planet);
    }

    public void StartScript()
    {
        loseGame = StartCoroutine(CheckLoseGame());
        winGame = StartCoroutine(CheckWinGame());
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
                LoseGame();
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
                WinGame();
                break;
            }

            yield return new WaitForSeconds(5f);
        }
    }

    protected abstract void WinGame();
    protected abstract void LoseGame();
}
