using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMediumController : MonoBehaviour
{
    public LayerMask planetLayer;
    private bool isStartBattle = true;

    void Start()
    {
        Debug.Log("Активен Медиум");
        StartCoroutine(SendUnitsPeriodically());
    }

    private void Update()
    {
        CheckStartBattle();
        if (!HasEnemyPlanets())
        {
            SceneManager.LoadScene(3); // Замените "Scene3" на имя сцены 3
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
        if (enemyPlanets.Length >= 4) isStartBattle = false;

    }
    private System.Collections.IEnumerator SendUnitsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3,9));

            Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();

            foreach (Planet enemyPlanet in enemyPlanets)
            {
                Planet targetPlanet = ChooseTargetPlanet(enemyPlanet);
                if (targetPlanet != null)
                {
                    enemyPlanet.SendUnitsToPlanet(targetPlanet);
                }
            }
        }
    }

    private Planet ChooseTargetPlanet(Planet sourcePlanet)
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet")
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();

        Planet[] allPlanets = FindObjectsOfType<Planet>();

        Planet[] targetPlanets = allPlanets.Where(planet => planet.tag == "NeutralPlanet" || planet.tag == "PlayerPlanet").ToArray();

        Planet[] targetNeutralPlanets = allPlanets.Where(planet => planet.tag == "NeutralPlanet").ToArray();

        if (isStartBattle)
        {
            targetPlanets = targetNeutralPlanets.OrderBy(planet => planet.currentUnitCount).ToArray();

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
