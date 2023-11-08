using System.Collections;
using UnityEngine;
using Zenject;

public class AirCraftSpawner : MonoBehaviour
{
    [Inject] DiContainer diContainer;
    [Inject] private ShipConstructor shipConstructor;

    [HideInInspector] public GameObject unitPrefab;
    [HideInInspector] public GameObject cruiserPrefab;

    private Cruiser cruiser;
    private Color mainColor;

    void Start()
    {
        cruiser = GetComponentInParent<Cruiser>();
        mainColor = cruiser.mainColor;
        unitPrefab = GetComponentInParent<Cruiser>().unitPrefab;
        cruiserPrefab = GetComponentInParent<Cruiser>().cruiserPrefab;
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        while (true)
        {
            Vector2 objectPosition = transform.position;
            Quaternion objectRotation = transform.rotation;

            Vector2 leftOffset = objectRotation * Vector2.left * 0.36f;
            Vector2 upOffset = objectRotation * Vector2.up * 0.15f;
            Vector2 offset = leftOffset + upOffset;
            Vector2 position = objectPosition + offset;
            SpawnUnitsAtPosition(position, cruiser.targetPlanet, unitPrefab);

            Vector2 rightOffset = objectRotation * Vector2.right * 0.36f;
            offset = rightOffset + upOffset;
            position = objectPosition + offset;
            SpawnUnitsAtPosition(position, cruiser.targetPlanet, unitPrefab);

            yield return new WaitForSeconds(1.6f);
        }
    }

    public void SpawnUnitsAtPosition(Vector3 spawnPosition, Planet targetPlanet, GameObject prefab)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = diContainer.InstantiatePrefab(prefab, spawnPosition, spawnRotation, cruiser.transform.parent);
        unitInstance.tag = gameObject.transform.parent.tag;

        shipConstructor.ChangeUnit(unitInstance);

        unitInstance.GetComponent<Unit>().unitPrefab = unitPrefab;
        unitInstance.GetComponent<Unit>().cruiserPrefab = cruiserPrefab;
        Unit unitMovement = unitInstance.GetComponent<Unit>();

        if (unitMovement != null)
        {
            unitMovement.tagUnit = unitInstance.tag.ToString();
            unitInstance.GetComponent<SpriteRenderer>().color = mainColor;

            unitMovement.target = targetPlanet.transform;
            unitMovement.SetTarget(targetPlanet);
        }
    }
}
