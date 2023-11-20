using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Growth : MonoBehaviour
{
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    public bool isPlayer = false;
    public bool isEnemy1 = false;
    public bool isEnemy2 = false;
    public bool isEnemy3 = false;

    public List<GameObject> allPlanets = new List<GameObject>();
    public List<GameObject> playerPlanet = new List<GameObject>();
    public List<GameObject> enemy1Planet = new List<GameObject>();
    public List<GameObject> enemy2Planet = new List<GameObject>();
    public List<GameObject> enemy3Planet = new List<GameObject>();

    private Coroutine lifeCycle;

    public void GetPlanet(GameObject planet) { if (planet != null && !allPlanets.Contains(planet)) allPlanets.Add(planet); }

    private void Start()
    {
        lifeCycle = StartCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            CheckMembers();
        }
    }

    public void CheckMembers()
    {
        if (AbilityGrowth(player)) GrowthPlayer();

        if (AbilityGrowth(enemy1)) GrowthEnemy1();

        if (AbilityGrowth(enemy2)) GrowthEnemy2();

        if (AbilityGrowth(enemy3)) GrowthEnemy3();
    }

    private void GrowthPlayer()
    {
        SplitPlanet("PlayerPlanet", playerPlanet);

        if (playerPlanet.Count >= 2 && !isPlayer)
            StartCoroutine(GrowthStarting("PlayerPlanet", playerPlanet));
    }

    private void GrowthEnemy1()
    {
        SplitPlanet("Enemy1", enemy1Planet);

        if (enemy1Planet.Count >= 2 && !isEnemy1)
            StartCoroutine(GrowthStarting("Enemy1", enemy1Planet));
    }

    private void GrowthEnemy2()
    {
        SplitPlanet("Enemy2", enemy2Planet);

        if (enemy2Planet.Count >= 2 && !isEnemy2)
            StartCoroutine(GrowthStarting("Enemy2", enemy2Planet));
    }

    private void GrowthEnemy3()
    {
        SplitPlanet("Enemy3", enemy3Planet);

        if (enemy3Planet.Count >= 2 && !isEnemy3)
            StartCoroutine(GrowthStarting("Enemy3", enemy3Planet));
    }

    private void SplitPlanet(string tag, List<GameObject> listPlanet)
    {
        listPlanet.Clear();

        foreach (GameObject planet in allPlanets)
        {
            if (planet.tag == tag)
                listPlanet.Add(planet);
        }
    }

    private IEnumerator GrowthStarting(string tag, List<GameObject> listPlanet)
    {
        int timer = Random.Range(shipConstructor.timerStart, shipConstructor.timerEnd);
        EnableFlag(tag);

        yield return new WaitForSeconds(timer);

        if (listPlanet.Count <= 0) yield return null;

        int[] randomPlanets = GetRandomPlanets(listPlanet);

        GameObject chosenPlanet = listPlanet[randomPlanets[0]];
        MakeShip makeShips = chosenPlanet.GetComponent<MakeShip>();

        Planet targetPlanet = listPlanet[randomPlanets[1]].GetComponent<Planet>();
        makeShips.SpawnGrowthingCruiser(targetPlanet);
    }

    #region Trash

    private int[] GetRandomPlanets(List<GameObject> list)
    {
        int[] randomPlanets = new int[2];
        int randomStart = Random.Range(0, list.Count);
        int randomEnd = Random.Range(0, list.Count);

        while (randomStart == randomEnd)
            randomEnd = Random.Range(0, list.Count);

        randomPlanets[0] = randomStart;
        randomPlanets[1] = randomEnd;

        return randomPlanets;
    }

    private void EnableFlag(string tag)
    {
        switch (tag)
        {
            case "PlayerPlanet":
                isPlayer = true;
                break;

            case "Enemy1":
                isEnemy1 = true;
                break;

            case "Enemy2":
                isEnemy2 = true;
                break;

            case "Enemy3":
                isEnemy3 = true;
                break;

            default:
                break;
        }
    }

    public void DisableFlag(string tag)
    {
        switch (tag)
        {
            case "PlayerPlanet":
                isPlayer = false;
                break;

            case "Enemy1":
                isEnemy1 = false;
                break;

            case "Enemy2":
                isEnemy2 = false;
                break;

            case "Enemy3":
                isEnemy3 = false;
                break;

            default:
                break;
        }
    }

    private bool AbilityGrowth(ProgressPlayer script)
    {
        if (script.growthPlanet == 3)
            return true;
        else
            return false;
    }

    private bool AbilityGrowth(ProgressEnemy1 script)
    {
        if (script.growthPlanet == 3)
            return true;
        else
            return false;
    }

    private bool AbilityGrowth(ProgressEnemy2 script)
    {
        if (script.growthPlanet == 3)
            return true;
        else
            return false;
    }

    private bool AbilityGrowth(ProgressEnemy3 script)
    {
        if (script.growthPlanet == 3)
            return true;
        else
            return false;
    }

    #endregion
}
