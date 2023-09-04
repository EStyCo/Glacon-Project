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

private List<Vector2> spawnPoints = new List<Vector2>();

    void Start()
    {
        GenerateSpawnPoints();
    }

    void GenerateSpawnPoints()
    {
        int numberOfPlanets = GameManager.Instance.planetCount;

        Bounds bounds = spawnArea.bounds;

        Vector2 playerSpawnPoint = GetRandomSpawnPoint(bounds);
        Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity);
        spawnPoints.Add(playerSpawnPoint);

        Vector2 enemySpawnPoint = GetRandomSpawnPoint(bounds);
        Instantiate(enemyPlanetPrefab, enemySpawnPoint, Quaternion.identity);
        spawnPoints.Add(enemySpawnPoint);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector2 neutralSpawnPoint = GetRandomSpawnPoint(bounds);
            Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            spawnPoints.Add(neutralSpawnPoint);
        }
    }

    Vector2 GetRandomSpawnPoint(Bounds bounds)
    {
        Vector2 randomPoint;

        do
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            randomPoint = new Vector2((randomX / 1.5f) + 0.2f, (randomY / 1.1f) - 0.13f);
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