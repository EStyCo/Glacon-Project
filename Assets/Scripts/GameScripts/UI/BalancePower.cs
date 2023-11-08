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

    private float units = 0;

    public List<Planet> listPlanets;

    public List<Planet> playerPlanet = new List<Planet>();
    public List<Planet> enemy1Planet = new List<Planet>();
    public List<Planet> enemy2Planet = new List<Planet>();
    public List<Planet> enemy3Planet = new List<Planet>();

    private bool isPlayer = false;
    private bool isEnemy1 = false;
    private bool isEnemy2 = false;
    private bool isEnemy3 = false;

    private void Start()
    {
        //listPlanets = new List<List<Planet>> { playerPlanet, enemy1Planet, enemy2Planet, enemy3Planet };
    }

    public void SplitPlanets()
    {
        StartCoroutine(asd());
    }

    private IEnumerator asd()
    {
        while (true)
        {
            units = 0;
            float player = 0;
            float enemy1 = 0;
            float enemy2 = 0;
            float enemy3 = 0;

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

            SetColor();
            SetFilling(player, enemy1, enemy2);

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void ClearAllLists()
    {
        playerPlanet.Clear();
        enemy1Planet.Clear();
        enemy2Planet.Clear();
        enemy3Planet.Clear();
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

    private void CreateListPlanet()
    {
        RefreshListPlanet(playerPlanet, "PlayerPlanet");
        RefreshListPlanet(enemy1Planet, "Enemy1");
        RefreshListPlanet(enemy2Planet, "Enemy2");
        RefreshListPlanet(enemy3Planet, "Enemy3");
    }

    private void RefreshListPlanet(List<Planet> listPlanet, string tagPlanet)
    {



        /*listPlanet.Clear();

        GameObject[] playerPlanetObjects = GameObject.FindGameObjectsWithTag(tagPlanet);

        foreach (GameObject playerPlanetObject in playerPlanetObjects)
        {
            Planet planetComponent = playerPlanetObject.GetComponent<Planet>();
            if (planetComponent != null && !playerPlanet.Contains(planetComponent))
            {
                listPlanet.Add(planetComponent);
            }
        }

        SetColor();*/
    }

    private void SumUnits()
    {
        /*        int tempUnitsCount = 0;
                for (int i = 0; i < planetLists.Count; i++)
                {
                    foreach (Planet planet in planetLists[i])
                    {
                        //tempUnitsCount += planet.currentUnitCount;
                    }
                }
                int flyedUnits = FindUnits();
                unitsCount = tempUnitsCount + flyedUnits;*/
    }

    private int FindUnits()
    {
        int count = 0;

        Unit[] unitMovements = FindObjectsOfType<Unit>();
        foreach (Unit unitMovementComponent in unitMovements)
        {
            count++;
        }

        return count;
    }

    private int FindUnits(string tagUnit)
    {
        int count = 0;
        GameObject[] objectUnits = GameObject.FindGameObjectsWithTag(tagUnit);

        foreach (GameObject objectUnit in objectUnits)
        {
            Unit unitComponent = objectUnit.GetComponent<Unit>();

            if (unitComponent != null)
            {
                count++;
            }
        }
        return count;
    }

    private void UpdateFill()
    {
        /*SumUnits();
        int playerCount = 0;
        foreach (Planet planet in playerPlanet)
        {
            playerCount += planet.currentUnitCount;
        }
        playerCount += FindUnits("PlayerPlanet");

        int enemy1Count = playerCount;
        foreach (Planet planet in enemy1Planet)
        {
            enemy1Count += planet.currentUnitCount;
        }
        enemy1Count += FindUnits("Enemy1");

        int enemy2Count = enemy1Count;
        foreach (Planet planet in enemy2Planet)
        {
            enemy2Count += planet.currentUnitCount;
        }
        enemy2Count += FindUnits("Enemy2");

        if (planetLists.Count > 3 && (planetLists[3].Count == 0 || !planetLists.Contains(enemy3Planet)))
        {
            if (planetLists.Contains(enemy3Planet))
            {
                planetLists.Remove(enemy3Planet);
            }
            StartCoroutine(SmoothTransition(1f, enemy2Fill, 1.4f));
        }
        else
        {
            float tempEnemy2Count = (float)enemy2Count / (float)unitsCount;
            StartCoroutine(SmoothTransition(tempEnemy2Count, enemy2Fill, 1.4f));
        }

        if (planetLists.Count > 2 && (planetLists[2].Count == 0 || !planetLists.Contains(enemy2Planet)))
        {
            if (planetLists.Contains(enemy2Planet) || planetLists.Contains(enemy3Planet))
            {
                planetLists.Remove(enemy3Planet);
                planetLists.Remove(enemy2Planet);
            }
            StartCoroutine(SmoothTransition(1f, enemy1Fill, 1.4f));
        }
        else
        {
            float tempEnemy1Count = (float)enemy1Count / (float)unitsCount;
            StartCoroutine(SmoothTransition(tempEnemy1Count, enemy1Fill, 1.4f));
        }

        float tempPlayerCount = (float)playerCount / (float)unitsCount;
        StartCoroutine(SmoothTransition(tempPlayerCount, playerFill, 1.4f));*/
    }

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

}
