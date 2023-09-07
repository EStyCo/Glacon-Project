using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIHardController : MonoBehaviour
{
    public LayerMask planetLayer;
    private bool isStartBattle = true;

    void Start()
    {
        StartCoroutine(SendUnitsPeriodically());
    }

    private void Update()
    {
        CheckStartBattle();
        if (!HasEnemyPlanets())
        {
            SceneManager.LoadScene(3);
        }
    }

    private bool HasEnemyPlanets()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                                 .Select(go => go.GetComponent<Planet>())
                                 .Where(planet => planet != null)
                                 .ToArray();
        return enemyPlanets.Length > 0;
    }
    private void CheckStartBattle()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                             .Select(go => go.GetComponent<Planet>())
                             .Where(planet => planet != null)
                             .ToArray();
        if (enemyPlanets.Length >= 3) isStartBattle = false;
    } //Проверка на "Начальную" стадию битвы.
    private System.Collections.IEnumerator SendUnitsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));

            Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .OrderByDescending(planet => planet.currentUnitCount)
                                     .ToArray();

            List<Planet> enemyListPlanets = new List<Planet>();

            Planet targetPlanet = ChooseTargetPlanet();

            int countTargetUnits = targetPlanet.currentUnitCount;
            int countEnemyUnits = 0;

            foreach (Planet planet in enemyPlanets)
            {
                enemyListPlanets.Add(planet);
                countEnemyUnits += Mathf.FloorToInt(planet.currentUnitCount / 2f);

                if (countEnemyUnits > countTargetUnits) break;
            }
            foreach (Planet enemyPlanet in enemyListPlanets)
            {
                if (targetPlanet != null) enemyPlanet.SendUnitsToPlanet(targetPlanet);
            }

            enemyListPlanets.Clear();
        }
    }
    private Planet ChooseTargetPlanet()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();

        Planet[] allPlanets = FindObjectsOfType<Planet>();

        Planet[] targetPlanets = allPlanets.Where(planet => planet.tag == "NeutralPlanet" || planet.tag == "PlayerPlanet").ToArray();

        Planet[] targetNeutralPlanets = allPlanets.Where(planet => planet.tag == "NeutralPlanet").ToArray();

        Planet[] targetNeutralLargePlanets = allPlanets
                        .Where(planet => planet.tag == "NeutralPlanet" && planet.selectedSize == Planet.Size.large)
                        .OrderBy(planet => planet.currentUnitCount)
                        .ToArray();
        if (isStartBattle)
        {
            targetPlanets = targetNeutralLargePlanets.OrderBy(planet => planet.currentUnitCount).ToArray();

            return targetPlanets[0];
        }
        else if (targetPlanets.Length > 0)
        {
            targetPlanets = targetPlanets.OrderBy(planet => planet.currentUnitCount).ToArray();

            return targetPlanets[0];
        }
        return null;
    }



}
