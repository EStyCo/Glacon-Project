using UnityEngine;
using Zenject;

public class MakeShip : MonoBehaviour
{
    [Inject] private DiContainer diContainer;
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

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

        int cruisers = unitsToSend / 20;
        int units = unitsToSend % 20;

        for (int i = 0; i < cruisers; i++)
        {
            if (planet.currentUnitCount > 20)
            {
                SendCruisers(targetPlanet);
                planet.currentUnitCount -= 20;
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
        GameObject unitInstance = diContainer.InstantiatePrefab(prefab, spawnPosition, spawnRotation, planet.unitsParent.transform);

        shipConstructor.ChangeUnit(unitInstance);

        unitInstance.GetComponent<Unit>().unitPrefab = unitPrefab;
        unitInstance.GetComponent<Unit>().cruiserPrefab = cruiserPrefab;
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
        GameObject cruiserInstance = diContainer.InstantiatePrefab(prefab, spawnPosition, spawnRotation, planet.unitsParent.transform);

        shipConstructor.ChangeCruiser(cruiserInstance);

        cruiserInstance.GetComponent<Cruiser>().unitPrefab = unitPrefab;
        cruiserInstance.GetComponent<Cruiser>().cruiserPrefab = cruiserPrefab;
        Cruiser cruiser = cruiserInstance.GetComponent<Cruiser>();

        if (cruiser != null)
        {
            cruiser.tag = gameObject.tag;
            cruiser.tagUnit = cruiserInstance.tag.ToString();
            cruiser.GetComponent<SpriteRenderer>().color = planet.planetRenderer.color;

            cruiser.target = targetPlanet.transform;
            cruiser.SetTarget(targetPlanet);
        }
    }

}
