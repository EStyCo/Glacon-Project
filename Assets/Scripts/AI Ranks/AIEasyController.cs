using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class AIEasyController : MonoBehaviour
{
    private string tagPlanet;

    private bool isStartBattle = true;

    void Start()
    {
        tagPlanet = gameObject.tag;
        StartCoroutine(SendUnitsPeriodically());
    }

    private void Update()
    {
        CheckStartBattle();
    }

    private void CheckStartBattle()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
                             .Select(go => go.GetComponent<Planet>())
                             .Where(planet => planet != null)
                             .ToArray();
        if (enemyPlanets.Length >= 4) isStartBattle = false;
    } //Проверка на "Начальную" стадию битвы.

    private Planet[] FindMainPlanets()
    { 
        Planet[] mainPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();
        return mainPlanets;
    }

    private System.Collections.IEnumerator SendUnitsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));

            Planet[] mainPlanets = FindMainPlanets();

            if (mainPlanets.Length == 0)
                yield break;

            List<Planet> enemyListPlanets = new List<Planet>();

            Planet targetPlanet = ChooseTargetPlanet(mainPlanets);

            int countTargetUnits = (targetPlanet != null) ? targetPlanet.currentUnitCount : 0;
            int countEnemyUnits = 0;

            foreach (Planet planet in mainPlanets)
            {
                enemyListPlanets.Add(planet);
                countEnemyUnits += Mathf.FloorToInt(planet.currentUnitCount / 2f);

                if (countEnemyUnits > countTargetUnits) break;
            }
            foreach (Planet enemyPlanet in enemyListPlanets)
            {
                if (targetPlanet != null) enemyPlanet.SendShipsToPlanet(targetPlanet);
            }

            enemyListPlanets.Clear();
        }
    }

    private Planet ChooseTargetPlanet(Planet[] mainPlanets)
    {
        Planet targetPlanet;

        Planet[] allPlanets = FindObjectsOfType<Planet>();

        Planet[] targetPlanets = allPlanets.Where(planet => planet.tag != tagPlanet).ToArray();

        Planet[] targetNeutralPlanets = allPlanets.Where(planet => planet.tag == "NeutralPlanet").ToArray();

        Planet[] targetNeutralLargePlanets = allPlanets
                        .Where(planet => planet.tag == "NeutralPlanet" && planet.selectedSize == Planet.Size.large)
                        .OrderBy(planet => planet.currentUnitCount)
                        .ToArray();
        if (isStartBattle && targetNeutralLargePlanets.Length != 0)
        {
            Vector2 averagePosition = AveragePosition(mainPlanets);
            targetPlanet = FindClosestPlanet(averagePosition, targetNeutralLargePlanets);

            return targetPlanet;
        }
        else if (isStartBattle && targetNeutralPlanets.Length != 0)
        {
            Vector2 averagePosition = AveragePosition(mainPlanets);
            targetPlanet = FindClosestPlanet(averagePosition, targetNeutralPlanets);

            return targetPlanet;
        }
        else 
        {
            Vector2 averagePosition = AveragePosition(mainPlanets);
            targetPlanet = FindClosestPlanet(averagePosition, targetPlanets);

            return targetPlanet;
        }
    }

    private Vector2 AveragePosition(Planet[] mainPlanets)
    {
        float averageX = 0;
        float averageY = 0;

        foreach (Planet planet in mainPlanets)
        {
            averageX += planet.transform.position.x;
            averageY += planet.transform.position.y;
        }
        averageX = averageX / mainPlanets.Length;
        averageY = averageY / mainPlanets.Length;

        Vector2 averagePosition = new Vector2(averageX, averageY);

        return averagePosition;
    }

    private Planet FindClosestPlanet(Vector2 averagePosition, Planet[] planets)
    {
        if (planets.Length == 0)
        {
            return null;
        }

        Planet closestPlanet = null;
        float closestDistance = float.MaxValue;

        foreach (Planet planet in planets)
        {
            Vector2 planetPosition = planet.transform.position;

            float distance = Vector2.Distance(averagePosition, planetPosition);

            if (distance < closestDistance)
            {
                closestPlanet = planet;
                closestDistance = distance;
            }
        }

        return closestPlanet;
    }
}