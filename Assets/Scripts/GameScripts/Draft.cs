using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Draft : MonoBehaviour
{
    [SerializeField] private RectTransform topLeft;
    [SerializeField] private RectTransform topRight;
    [SerializeField] private RectTransform botLeft;
    [SerializeField] private RectTransform botRight;

    [Inject] private DiContainer diContainer;
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    public List<GameObject> allPlanets = new List<GameObject>();
    public List<GameObject> playerPlanet = new List<GameObject>();
    public List<GameObject> enemy1Planet = new List<GameObject>();
    public List<GameObject> enemy2Planet = new List<GameObject>();
    public List<GameObject> enemy3Planet = new List<GameObject>();

    public void CheckMembers()
    {
        if (player.draftPlanet > 0)
        {
            StartCoroutine(DraftPlanets(player.draftPlanet, "PlayerPlanet", playerPlanet));
        }
        if (enemy1.draftPlanet > 0)
        {
            StartCoroutine(DraftPlanets(enemy1.draftPlanet, "Enemy1", enemy1Planet));
        }
        if (enemy2.draftPlanet > 0)
        {
            StartCoroutine(DraftPlanets(enemy2.draftPlanet, "Enemy3", enemy2Planet));
        }
        if (enemy3.draftPlanet > 0)
        {
            StartCoroutine(DraftPlanets(enemy3.draftPlanet, "Enemy3", enemy3Planet));
        }
    }

    private IEnumerator DraftPlanets(int index, string tagPlanets, List<GameObject> listPlanet)
    {
        yield return new WaitForSeconds(Random.Range(5f, 30f));
        SplitPlanet(tagPlanets, listPlanet);

        while (listPlanet.Count > 0)
        {
            SplitPlanet(tagPlanets, listPlanet);

            SpawnShips(index, listPlanet);

            yield return new WaitForSeconds(Random.Range(5f, 30f));
        }

        yield break;
    }

    private void SpawnShips(int index, List<GameObject> listPlanet)
    {
        foreach (GameObject planet in listPlanet)
        {
            Vector2 spawnPoint = GetRandomPoint();

            for (int i = 0; i < Random.Range(1, 10); i++)
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

    private Vector2 GetRandomPoint()
    {
        Vector2 topPoint = topLeft.position;
        Vector2 botPoint = botLeft.position;
        float randomValue = Random.Range(0f, 1f);

        Vector2 spawnPosition = Vector2.Lerp(botPoint, topPoint, randomValue);
        return spawnPosition;





/*        Vector2 randomPoint;

        do
        {
            float randomX = UnityEngine.Random.Range(-20f, 20f);
            float randomY = UnityEngine.Random.Range(-15f, 15f);
            randomPoint = new Vector2(randomX, randomY);
        }
        while (IsValidPosition(randomPoint));
        return randomPoint;*/
    }

    private bool IsValidPosition(Vector2 randomPoint)
    {
        if (randomPoint.x <= 15f && randomPoint.x >= -15f &&
            randomPoint.y <= 10 && randomPoint.y >= -10f)
        {
            return false;
        }

        return true;
    }

    private void SplitPlanet(string tag, List<GameObject> listPlanet)
    {
        listPlanet.Clear();

        foreach (GameObject planet in allPlanets)
        {
            if (planet.tag == tag)
            {
                listPlanet.Add(planet);
            }
        }
    }

    public void GetPlanet(GameObject planet)
    {
        if (planet != null && !allPlanets.Contains(planet))
        {
            allPlanets.Add(planet);
        }
    }
}
