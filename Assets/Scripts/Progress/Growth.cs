using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //Debugger.Log($"Метод - LifeCycle | Запустился");

        while (true)
        {
            yield return new WaitForSeconds(1f);

            CheckMembers();
        }

        //Debugger.Log($"Метод - LifeCycle | Закончился");
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
        {
            StartCoroutine(GrowthStarting("PlayerPlanet", playerPlanet));
        }
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
            if (planet.CompareTag(tag))
                listPlanet.Add(planet);
        }

        //Debugger.Log($"Метод - SplitPlanet | По тэгу {tag}. Планет: {listPlanet.Count}");
    }

    private IEnumerator GrowthStarting(string tag, List<GameObject> listPlanet)
    {
        EnableFlag(tag);

        int timer = UnityEngine.Random.Range(shipConstructor.timerStart, shipConstructor.timerEnd);

        yield return new WaitForSeconds(timer);

        if (listPlanet.Count <= 1)
        {
            DisableFlag(tag);
            yield break;
        }

        SplitPlanet(tag, listPlanet);
        GameObject[] randomPlanets = GetRandomPlanets(listPlanet);

        if (randomPlanets == null) yield break;

        GameObject chosenPlanet = randomPlanets[0];
        MakeShip makeShips = chosenPlanet.GetComponent<MakeShip>();

        Planet targetPlanet = randomPlanets[1].GetComponent<Planet>();
        makeShips.SpawnGrowthingCruiser(targetPlanet);
    }

    private GameObject[] GetRandomPlanets(List<GameObject> list)
    {
        GameObject[] randomPlanets = new GameObject[2];
        var toPick = list.ToList();

        if (toPick.Count > 0)
        {
            System.Random random = new System.Random();
            var first = toPick.OrderBy(x => random.Next()).First();

            toPick.Remove(first);

            if (toPick.Count == 0)
                return null;

            var second = toPick.OrderBy(x => random.Next()).First();

            randomPlanets[0] = first;
            randomPlanets[1] = second;
        }

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

}
