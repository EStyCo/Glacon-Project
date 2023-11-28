using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Draft : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private RectTransform[] top = new RectTransform[2];
    [SerializeField] private RectTransform[] bottom = new RectTransform[2];
    [SerializeField] private RectTransform[] left = new RectTransform[2];
    [SerializeField] private RectTransform[] right = new RectTransform[2];

    [Header("Planets")]
    public List<GameObject> allPlanets = new List<GameObject>();
    public List<GameObject> playerPlanet = new List<GameObject>();
    public List<GameObject> enemy1Planet = new List<GameObject>();
    public List<GameObject> enemy2Planet = new List<GameObject>();
    public List<GameObject> enemy3Planet = new List<GameObject>();

    [Inject] private DiContainer diContainer;
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    #region Start

    public void CheckMembers()
    {
        if (player.draftPlanet > 0)
        {
            int chance = shipConstructor.chanceDraft1Level;
            if (player.draftPlanet > 1)
                chance = shipConstructor.chanceDraft2Level;
            StartCoroutine(DraftPlanets(chance, player.draftPlanet, "PlayerPlanet", playerPlanet));
        }

        if (enemy1.draftPlanet > 0)
        {
            int chance = shipConstructor.chanceDraft1Level;
            if (enemy1.draftPlanet > 1)
                chance = shipConstructor.chanceDraft2Level;

            StartCoroutine(DraftPlanets(chance, enemy1.draftPlanet, "Enemy1", enemy1Planet));
        }

        if (enemy2.draftPlanet > 0)
        {
            int chance = shipConstructor.chanceDraft1Level;
            if (enemy2.draftPlanet > 1)
                chance = shipConstructor.chanceDraft2Level;

            StartCoroutine(DraftPlanets(chance, enemy2.draftPlanet, "Enemy2", enemy2Planet));
        }

        if (enemy3.draftPlanet > 0)
        {
            int chance = shipConstructor.chanceDraft1Level;
            if (enemy3.draftPlanet > 1)
                chance = shipConstructor.chanceDraft2Level;

            StartCoroutine(DraftPlanets(chance, enemy3.draftPlanet, "Enemy3", enemy3Planet));
        }
    }

    private IEnumerator DraftPlanets(int percent, int index, string tagPlanets, List<GameObject> listPlanet)
    {
        //Debugger.Log($"Корутина - DraftPlanets | Запущена Тэг:{tagPlanets}. Планет: {listPlanet.Count}");
        int waiting = Random.Range(shipConstructor.firstTimerStart, shipConstructor.firstTimerEnd);
        yield return new WaitForSeconds(waiting);

        while (CheckPlanets(tagPlanets))
        {
            //Debugger.Log($"Итерация цикла в корутине DraftPlanets | Тэг:{tagPlanets}. Планет: {listPlanet.Count}");
            SplitPlanet(tagPlanets, listPlanet, percent);

            SpawnShips(listPlanet, index);

            waiting = Random.Range(shipConstructor.cycleTimerStart, shipConstructor.cycleTimerEnd);
            yield return new WaitForSeconds(waiting);
        }

        //Debugger.Log($"Корутина - DraftPlanets | Завершилась Тэг:{tagPlanets}. Планет: {listPlanet.Count}");
        //yield break;
    }

    #endregion

    private bool CheckPlanets(string tag)
    {
        foreach (GameObject planet in allPlanets)
        {
            if (planet.CompareTag(tag))
            {
                //Debugger.Log($"Метод - CheckPlanets | Вернул true, по тэгу {tag}");
                return true;
            }
        }

        return false;
    }

    private void SpawnShips(List<GameObject> listPlanet, int index)
    {
        if (listPlanet.Count <= 0) return;

        int[] unitsCount = GetUnitsCount(index);

        if (index == 3) SpawnCruisers(listPlanet);

        foreach (GameObject planet in listPlanet)
        {
            Vector2 spawnPoint = GetRandomPoint();

            for (int i = 0; i < Random.Range(unitsCount[0], unitsCount[1]); i++)
            {
                GameObject unit = planet.GetComponent<Planet>().unitPrefab;
                GameObject cruiser = planet.GetComponent<Planet>().cruiserPrefab;
                GameObject parent = planet.GetComponent<Planet>().unitsParent;
                GameObject unitInstance = diContainer.InstantiatePrefab(unit, spawnPoint, Quaternion.identity, parent.transform);

                shipConstructor.ChangeUnit(unitInstance);

                unitInstance.GetComponent<Unit>().unitPrefab = unit;
                unitInstance.GetComponent<Unit>().cruiserPrefab = cruiser;
                Unit unitMovement = unitInstance.GetComponent<Unit>();

                if (unitMovement != null)
                {
                    unitInstance.tag = planet.tag;
                    unitMovement.tagUnit = unitInstance.tag.ToString();
                    unitInstance.GetComponent<SpriteRenderer>().color = planet.GetComponent<Planet>().planetRenderer.color;

                    unitMovement.target = planet.transform;
                    unitMovement.SetTarget(planet.GetComponent<Planet>());
                }
            }
        }
    }

    private int[] GetUnitsCount(int index)
    {
        int[] unitsCount = new int[2];

        if (index < 0)
        {
            unitsCount[0] = shipConstructor.units1LevelStart;
            unitsCount[1] = shipConstructor.units1LevelEnd;
        }
        else if (index >= 2)
        {
            unitsCount[0] = shipConstructor.units2LevelStart;
            unitsCount[1] = shipConstructor.units2LevelEnd;
        }

        return unitsCount;
    }

    private void SpawnCruisers(List<GameObject> listPlanet)
    {
        foreach (GameObject planet in listPlanet)
        {
            int randomCount = Random.Range(0, 101);
            if (randomCount < shipConstructor.chanceSpawnCruisers)
            {
                Vector2 spawnPoint = GetRandomPoint();

                GameObject unit = planet.GetComponent<Planet>().unitPrefab;
                GameObject cruiser = planet.GetComponent<Planet>().cruiserPrefab;
                GameObject parent = planet.GetComponent<Planet>().unitsParent;
                GameObject cruiserInstance = diContainer.InstantiatePrefab(cruiser, spawnPoint, Quaternion.identity, parent.transform);

                shipConstructor.ChangeCruiser(cruiserInstance);

                cruiserInstance.GetComponent<Cruiser>().unitPrefab = unit;
                cruiserInstance.GetComponent<Cruiser>().cruiserPrefab = cruiser;
                Cruiser cruiserEx = cruiserInstance.GetComponent<Cruiser>();

                if (cruiserEx != null)
                {
                    cruiserInstance.tag = planet.tag;
                    cruiserEx.tagUnit = cruiserInstance.tag.ToString();
                    cruiserInstance.GetComponent<SpriteRenderer>().color = planet.GetComponent<Planet>().planetRenderer.color;

                    cruiserEx.target = planet.transform;
                    cruiserEx.SetTarget(planet.GetComponent<Planet>());
                }

            }
        }
    }

    private Vector2 GetRandomPoint()
    {
        RectTransform[] arrayPoints = RandomSide();
        Vector2 topPoint = arrayPoints[0].position;
        Vector2 botPoint = arrayPoints[1].position;
        float randomValue = Random.Range(0f, 1f);

        Vector2 spawnPosition = Vector2.Lerp(botPoint, topPoint, randomValue);
        return spawnPosition;
    }

    private RectTransform[] RandomSide()
    {
        int randomIndex = Random.Range(0, 4);

        switch (randomIndex)
        {
            case 0:
                return top;

            case 1:
                return bottom;

            case 2:
                return left;

            case 3:
                return right;

            default:
                return null;
        }
    }

    private void SplitPlanet(string tag, List<GameObject> listPlanet, int percent)
    {
        listPlanet.Clear();

        foreach (GameObject planet in allPlanets)
        {
            int randomCount = Random.Range(0, 101);

            if (planet.CompareTag(tag) && randomCount < percent)
            {
                listPlanet.Add(planet);
            }
        }
        //Debugger.Log($"Метод - SplitPlanet | Тэг {tag}. Планеты: {listPlanet.Count}");
    }

    public void GetPlanet(GameObject planet)
    {
        if (planet != null && !allPlanets.Contains(planet))
        {
            allPlanets.Add(planet);
        }
    }
}
