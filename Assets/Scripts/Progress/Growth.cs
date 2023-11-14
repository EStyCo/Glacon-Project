using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Growth : MonoBehaviour
{
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
        if (AbilityGrowth(player))
        {
            GrowthPlayer();
        }
        if (AbilityGrowth(enemy1))
        {
            GrowthEnemy1();
        }
        if (AbilityGrowth(enemy2))
        {
            GrowthEnemy2();
        }
        if (AbilityGrowth(enemy3))
        {
            GrowthEnemy3();
        }
    }

    public void GetPlanet(GameObject planet)
    {
        if (planet != null && !allPlanets.Contains(planet))
        {
            allPlanets.Add(planet);
        }
    }

    private void GrowthPlayer()
    {
        SplitPlanet("PlayerPlanet", playerPlanet);

        GrowthStarting("PlayerPlanet", playerPlanet);
    }

    private void GrowthEnemy1()
    {
        SplitPlanet("Enemy1", enemy1Planet);

        GrowthStarting("Enemy1", enemy1Planet);
    }

    private void GrowthEnemy2()
    {
        SplitPlanet("Enemy2", enemy2Planet);

        GrowthStarting("Enemy2", enemy2Planet);
    }

    private void GrowthEnemy3()
    {
        SplitPlanet("Enemy3", enemy3Planet);

        GrowthStarting("Enemy3", enemy3Planet);
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


    private void GrowthStarting(string tag, List<GameObject> listPlanet)
    {
        if (listPlanet.Count <= 0) return;

        foreach (GameObject planet in listPlanet)
        {
            Planet script = planet.GetComponent<Planet>();
            script.growthLevel += 0.15f;
            script.GrowthEffect.Play("GrowthEffect");
        }

        StartCoroutine(TimerGrowth(tag, listPlanet));
    }

    private void GrowthCanceling(string tag, List<GameObject> listPlanet)
    {
        foreach (GameObject planet in listPlanet)
        {
            Planet script = planet.GetComponent<Planet>();
            script.growthLevel -= 0.15f;
            script.GrowthEffect.Play("Idle");
        }

        StartCoroutine(TimerRest(tag));
    }

    private IEnumerator TimerGrowth(string tag, List<GameObject> listPlanet)
    {
        yield return new WaitForSeconds(10f);

        GrowthCanceling(tag, listPlanet);
    }

    private IEnumerator TimerRest(string tag)
    {
        float restTimer = Random.Range(5f, 12f);
        yield return new WaitForSeconds(restTimer);

        switch (tag)
        {
            case "PlayerPlanet":
                GrowthPlayer();
                break;
            case "Enemy1":
                GrowthEnemy1();
                break;
            case "Enemy2":
                GrowthEnemy2();
                break;
            case "Enemy3":
                GrowthEnemy3();
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
