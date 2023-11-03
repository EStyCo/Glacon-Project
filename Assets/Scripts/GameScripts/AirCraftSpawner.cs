using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCraftSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public MakeShip makeShip;
    private Cruiser cruiser;

    void Start()
    {
        cruiser = GetComponentInParent<Cruiser>();
        unitPrefab = GetComponentInParent<Cruiser>().unitPrefab;
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    { 
        while (true) 
        {
            Vector2 position = new Vector2 (transform.position.x + 0.1f, transform.position.y - 0.4f);
            makeShip.SpawnUnitsAtPosition(position, cruiser.targetPlanet, unitPrefab);

            position = new Vector2(transform.position.x + 0.1f, transform.position.y + 0.4f);

            makeShip.SpawnUnitsAtPosition(position, cruiser.targetPlanet, unitPrefab);

            yield return new WaitForSeconds(1.6f);
        }
    }
}
