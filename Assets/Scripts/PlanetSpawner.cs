using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject playerPlanetPrefab;

    public GameObject enemyPlanetPrefab;

    public GameObject neutralPlanetPrefab;

    public BoxCollider2D spawnArea;
    public int numberOfPlanets = 13;
    private float minDistanceBetweenPlanets = 0.9f;

    public Canvas canvas;

    private float canvasX;
    private float canvasY;

    private List<Vector2> spawnPoints = new List<Vector2>();

    void Start()
    {
        CalculationSpawnArea();
        GenerateSpawnPoints();
    }
    private void CalculationSpawnArea()
    { 
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 canvasSize = canvasRectTransform.rect.size;

        canvasX = canvasSize.x *= canvasRectTransform.localScale.x;
        canvasY = canvasSize.y *= canvasRectTransform.localScale.y;
    }
    void GenerateSpawnPoints()
    {
        int numberOfPlanets = GameManager.Instance.planetCount;

        Bounds bounds = spawnArea.bounds;

        Vector3 playerSpawnPoint = GetRandomSpawnPoint(bounds);
        Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity);
        spawnPoints.Add(playerSpawnPoint);

        Vector3 enemySpawnPoint = GetRandomSpawnPoint(bounds);
        Instantiate(enemyPlanetPrefab, enemySpawnPoint, Quaternion.identity);
        spawnPoints.Add(enemySpawnPoint);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 neutralSpawnPoint = GetRandomSpawnPoint(bounds);
            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            Planet planetScript = newPlanet.GetComponent<Planet>();
            planetScript.selectedSize = (Planet.Size)Random.Range(1, 5);
            spawnPoints.Add(neutralSpawnPoint);
        }
    }

    Vector3 GetRandomSpawnPoint(Bounds bounds)
    {
        Vector3 randomPoint;

        do
        {
            float randomX = Random.Range((-canvasX+1)/2, (canvasX-1)/2);
            float randomY = Random.Range((-canvasY+1.2f)/2, (canvasY-1.2f)/2);
            randomPoint = new Vector3(randomX, randomY, 1);
        }
        while (!IsValidSpawnPoint(randomPoint));

        return randomPoint;
    }

    bool IsValidSpawnPoint(Vector2 point)
    {
        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(point, spawnPoint) < minDistanceBetweenPlanets)
            {
                return false;
            }
        }
        return true;
    }

}