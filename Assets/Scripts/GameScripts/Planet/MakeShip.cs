using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShip : MonoBehaviour
{
    private Planet planet;
    public GameObject unitPrefab;
    public GameObject cruiserPrefab;

    private const float spawnDistance = 0.25f;
    void Start()
    {
        planet = GetComponent<Planet>();
    }

    public System.Collections.IEnumerator SpawnUnitsWithDelay(Planet targetPlanet, int unitsToSend)
    {
        unitPrefab = planet.unitPrefab;
        cruiserPrefab = planet.cruiserPrefab;

        int cruisers = unitsToSend / 10;
        int units = unitsToSend % 10;

        for (int i = 0; i < cruisers; i++)
        {
            if (planet.currentUnitCount > 10)
            {
                SendCruisers(targetPlanet);
                planet.currentUnitCount -= 10;
                yield return new WaitForSeconds(0.08f);
            }
        }

        for (int i = 0; i < units - 1; i++)
        {
            if (planet.currentUnitCount > 1)
            {
                SendUnits(targetPlanet);
                planet.currentUnitCount--;
                yield return new WaitForSeconds(0.08f);
            }
        }
    }

    private void SendCruisers(Planet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position) * transform.localScale.normalized.y;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);

        SpawnCruisersAtPosition(spawnPosition, targetPlanet, cruiserPrefab);
    }

    private void SendUnits(Planet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position) * transform.localScale.normalized.y;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);

        SpawnUnitsAtPosition(spawnPosition, targetPlanet, unitPrefab);
    }

    private Vector3 CalculateSpawnPosition(Vector3 directionToTarget)
    {
        return transform.position + directionToTarget.normalized * spawnDistance;
    }

    private void SpawnUnitsAtPosition(Vector3 spawnPosition, Planet targetPlanet, GameObject prefab)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(prefab, spawnPosition, spawnRotation);
        unitInstance.GetComponent<Unit>().unitPrefab = unitPrefab;
        unitInstance.GetComponent<Unit>().cruiserPrefab = cruiserPrefab;
        unitInstance.transform.SetParent(planet.unitsParent.transform, false);
        Unit unitMovement = unitInstance.GetComponent<Unit>();

        if (unitMovement != null)
        {
            unitInstance.tag = gameObject.tag;
            unitMovement.tagUnit = unitInstance.tag.ToString();
            unitInstance.GetComponent<SpriteRenderer>().color = planet.planetRenderer.color;

            unitMovement.target = targetPlanet.transform;
            unitMovement.SetTarget(targetPlanet);
        }
    }

    private void SpawnCruisersAtPosition(Vector3 spawnPosition, Planet targetPlanet, GameObject prefab)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(prefab, spawnPosition, spawnRotation);
        unitInstance.GetComponent<Cruiser>().unitPrefab = unitPrefab;
        unitInstance.GetComponent<Cruiser>().cruiserPrefab = cruiserPrefab;
        unitInstance.transform.SetParent(planet.unitsParent.transform, false);
        Cruiser unitMovement = unitInstance.GetComponent<Cruiser>();

        if (unitMovement != null)
        {
            unitInstance.tag = gameObject.tag;
            unitMovement.tagUnit = unitInstance.tag.ToString();
            unitInstance.GetComponent<SpriteRenderer>().color = planet.planetRenderer.color;

            unitMovement.target = targetPlanet.transform;
            unitMovement.SetTarget(targetPlanet);
        }
    }
}
