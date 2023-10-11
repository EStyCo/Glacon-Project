using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainAI : MonoBehaviour
{
    private bool isStarting = true;
    public LayerMask planetLayer;
    private string tagPlanet;

    void Start()
    {
        tagPlanet = gameObject.tag;
        StartCoroutine(SendUnitsPeriodically());
    }

    private void Update()
    {

    }

    private System.Collections.IEnumerator SendUnitsPeriodically()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 9f));

        while (true)
        {
            if (!isStarting)
            {
                yield return new WaitForSeconds(Random.Range(3f, 12f));
            }

            MainPlanet[] allPlanets = FindObjectsOfType<MainPlanet>();
            MainPlanet targetPlanet = ChooseRandomTargetPlanet(allPlanets);
            MainPlanet planet = gameObject.GetComponent<MainPlanet>();

            if (targetPlanet != null)
            {
                planet.SendUnitsToPlanet(targetPlanet);
            }

            isStarting = false;
        }
    }

    private MainPlanet ChooseRandomTargetPlanet(MainPlanet[] allPlanets)
    {
        List<MainPlanet> targetPlanets = new List<MainPlanet>();
        foreach (MainPlanet planet in allPlanets)
        {
            if (planet != null && planet.tag != tagPlanet)
            {
                targetPlanets.Add(planet);
            }
        }

        if (targetPlanets.Count > 0)
        {
            int randomIndex = Random.Range(0, targetPlanets.Count);
            return targetPlanets[randomIndex];
        }

        return null;
    }
}
