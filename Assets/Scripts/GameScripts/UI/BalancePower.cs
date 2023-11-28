using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePower : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private UnityEngine.UI.Image playerFill;
    [SerializeField] private UnityEngine.UI.Image enemy1Fill;
    [SerializeField] private UnityEngine.UI.Image enemy2Fill;
    [SerializeField] private UnityEngine.UI.Image enemy3Fill;


    public List<Planet> listPlanets;

    public List<Planet> playerPlanet = new List<Planet>();
    public List<Planet> enemy1Planet = new List<Planet>();
    public List<Planet> enemy2Planet = new List<Planet>();
    public List<Planet> enemy3Planet = new List<Planet>();

    private float units = 0;
    private int flyingPlayer = 0;
    private int flyingEnemy1 = 0;
    private int flyingEnemy2 = 0;
    private int flyingEnemy3 = 0;

    private bool isPlayer = false;
    private bool isEnemy1 = false;
    private bool isEnemy2 = false;
    private bool isEnemy3 = false;

    #region Update Data

    private IEnumerator UpdateData()
    {
        while (true)
        {
            units = 0;
            float player = flyingPlayer;
            float enemy1 = flyingEnemy1;
            float enemy2 = flyingEnemy2;
            float enemy3 = flyingEnemy3;

            ClearAllLists();

            foreach (Planet planet in listPlanets)
            {
                switch (planet.gameObject.tag)
                {
                    case "PlayerPlanet":
                        playerPlanet.Add(planet);
                        player += planet.currentUnitCount;

                        if (planet.currentUnitCount == 0)
                            yield return null;
                        break;

                    case "Enemy1":
                        enemy1Planet.Add(planet);
                        enemy1 += planet.currentUnitCount;

                        if (planet.currentUnitCount == 0)
                            yield return null;
                        break;

                    case "Enemy2":
                        enemy2Planet.Add(planet);
                        enemy2 += planet.currentUnitCount;

                        if (planet.currentUnitCount == 0)
                            yield return null;
                        break;

                    case "Enemy3":
                        enemy3Planet.Add(planet);
                        enemy3 += planet.currentUnitCount;

                        if (planet.currentUnitCount == 0)
                            yield return null;
                        break;

                    default:
                        break;
                }
            }

            units = player + enemy1 + enemy2 + enemy3;

            enemy1 += player;
            enemy2 += enemy1;

            /*Debug.Log("FlyPlayer: " + flyingPlayer);
            Debug.Log("FlyEnemy1: " + flyingEnemy1);
            Debug.Log("FlyEnemy2: " + flyingEnemy2);
            Debug.Log("FlyEnemy3: " + flyingEnemy3);

            Debug.Log("Player: " + player);
            Debug.Log("Enemy1: " + enemy1);
            Debug.Log("Enemy2: " + enemy2);
            Debug.Log("Enemy3: " + enemy3);*/

            SetColor();
            SetFilling(player, enemy1, enemy2);

            yield return new WaitForSeconds(0.35f);
        }
    } 

    #endregion

    #region Split planets and calculate ships

    public void SplitPlanets()
    {
        StartCoroutine(UpdateData());
    }

    private void ClearAllLists()
    {
        playerPlanet.Clear();
        enemy1Planet.Clear();
        enemy2Planet.Clear();
        enemy3Planet.Clear();
    }

    public void GetFlyingShips(int count, string tag, bool isAdd)
    {
        switch (tag)
        {
            case "PlayerPlanet":
                if (isAdd)
                    flyingPlayer += count;
                else
                    flyingPlayer -= count;
                break;

            case "Enemy1":
                if (isAdd)
                    flyingEnemy1 += count;
                else
                    flyingEnemy1 -= count;
                break;

            case "Enemy2":
                if (isAdd)
                    flyingEnemy2 += count;
                else
                    flyingEnemy2 -= count;
                break;

            case "Enemy3":
                if (isAdd)
                    flyingEnemy3 += count;
                else
                    flyingEnemy3 -= count;
                break;

            default:
                break;
        }
    }

    #endregion

    #region Set Color and Filling

    private void SetColor()
    {
        if (playerPlanet.Count > 0)
        {
            Color newColor = playerPlanet[0].GetComponent<SpriteRenderer>().color;
            playerFill.color = newColor;
        }

        if (enemy1Planet.Count > 0)
        {
            Color newColor = enemy1Planet[0].GetComponent<SpriteRenderer>().color;
            enemy1Fill.color = newColor;
        }

        if (enemy2Planet.Count > 0)
        {
            Color newColor = enemy2Planet[0].GetComponent<SpriteRenderer>().color;
            enemy2Fill.color = newColor;
        }

        if (enemy3Planet.Count > 0)
        {
            Color newColor = enemy3Planet[0].GetComponent<SpriteRenderer>().color;
            enemy3Fill.color = newColor;
        }
    }

    private void SetFilling(float player, float enemy1, float enemy2)
    {
        if (enemy3Planet.Count > 0)
        {
            enemy3Fill.fillAmount = 1f;

            if (enemy2Fill.fillAmount != 0 && enemy1Fill.fillAmount != 0 && playerFill.fillAmount != 0)
            {
                StartCoroutine(SmoothTransitionEnemy2(enemy2 / units));
                StartCoroutine(SmoothTransitionEnemy1(enemy1 / units));
                StartCoroutine(SmoothTransitionPlayer(player / units));
            }
            else
            {
                enemy2Fill.fillAmount = enemy2 / units;
                enemy1Fill.fillAmount = enemy1 / units;
                playerFill.fillAmount = player / units;
            }
            return;
        }
        else if (enemy2Planet.Count > 0)
        {
            enemy2Fill.fillAmount = 1f;

            if (enemy1Fill.fillAmount != 0 && playerFill.fillAmount != 0)
            {
                StartCoroutine(SmoothTransitionEnemy1(enemy1 / units));
                StartCoroutine(SmoothTransitionPlayer(player / units));
            }
            else
            {
                enemy1Fill.fillAmount = enemy1 / units;
                playerFill.fillAmount = player / units;
            }
            return;
        }
        else if (enemy1Planet.Count > 0)
        {
            enemy1Fill.fillAmount = 1f;

            if (playerFill.fillAmount != 0)
            {
                StartCoroutine(SmoothTransitionPlayer(player / units));
            }
            else
            {
                playerFill.fillAmount = player / units;
            }
            return;
        }
    }

    #endregion

    #region SmoothTransition

    IEnumerator SmoothTransitionPlayer(float newFillCount)
    {
        if (!isPlayer)
        {
            isPlayer = true;

            float oldFillCount = playerFill.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                playerFill.fillAmount = Mathf.Lerp(oldFillCount, newFillCount, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            playerFill.fillAmount = newFillCount;
            isPlayer = false;
        }
    }

    IEnumerator SmoothTransitionEnemy1(float newFillCount)
    {
        if (!isEnemy1)
        {
            isEnemy1 = true;

            float oldFillCount = enemy1Fill.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                enemy1Fill.fillAmount = Mathf.Lerp(oldFillCount, newFillCount, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemy1Fill.fillAmount = newFillCount;
            isEnemy1 = false;
        }
    }

    IEnumerator SmoothTransitionEnemy2(float newFillCount)
    {
        if (!isEnemy2)
        {
            isEnemy2 = true;

            float oldFillCount = enemy2Fill.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                enemy2Fill.fillAmount = Mathf.Lerp(oldFillCount, newFillCount, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemy2Fill.fillAmount = newFillCount;
            isEnemy2 = false;
        }
    }

    #endregion

}
