using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMediumController : MonoBehaviour
{
    public string tagPlanet;

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

    private bool HasEnemyPlanets()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
                                 .Select(go => go.GetComponent<Planet>())
                                 .Where(planet => planet != null)
                                 .ToArray();
        return enemyPlanets.Length > 0;
    }
    private void CheckStartBattle()
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
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

            Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();

            foreach (Planet enemyPlanet in enemyPlanets)
            {
                Planet targetPlanet = ChooseTargetPlanet(enemyPlanet);
                if (targetPlanet != null)
                {
                    enemyPlanet.SendShipsToPlanet(targetPlanet);
                }
            }
        }
    }

    private Planet ChooseTargetPlanet(Planet sourcePlanet)
    {
        Planet[] enemyPlanets = GameObject.FindGameObjectsWithTag(tagPlanet)
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .ToArray();

        Planet[] allPlanets = FindObjectsOfType<Planet>();

        Planet[] targetPlanets = allPlanets.Where(planet => planet.tag != tagPlanet).ToArray();

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
