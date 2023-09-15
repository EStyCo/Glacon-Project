using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.UI;

public class PlanetSpawner : MonoBehaviour
{
    private List<Vector2> spawnPoints = new List<Vector2>();

    public GameObject playerPlanetPrefab;
    public GameObject enemyPlanetPrefab;
    public GameObject neutralPlanetPrefab;

    public Canvas speedometer;

    public Canvas canvas;

    public int numberOfPlanets = 13;
    private float minDistanceBetweenPlanets = 1.1f;

    private float canvasX;
    private float canvasY;

    void Start()
    {
        CalculationSpawnArea();
        GenerateSpawnPoints();
    }
    private void CalculationSpawnArea()
    { 
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 canvasSize = canvasRectTransform.rect.size;

        canvasX = canvasSize.x * canvasRectTransform.localScale.x;
        canvasY = canvasSize.y * canvasRectTransform.localScale.y;
        Debug.Log(canvasX);
        Debug.Log(canvasY);

    }
    void GenerateSpawnPoints()
    {
        int numberOfPlanets = GameManager.Instance.planetCount;

        Vector3 playerSpawnPoint = GetRandomSpawnPoint();
        Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity);
        spawnPoints.Add(playerSpawnPoint);

        Vector3 enemySpawnPoint = GetRandomSpawnPoint();
        Instantiate(enemyPlanetPrefab, enemySpawnPoint, Quaternion.identity);
        spawnPoints.Add(enemySpawnPoint);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            Planet planetScript = newPlanet.GetComponent<Planet>();
            planetScript.selectedSize = (Planet.Size)Random.Range(1, 4);
            spawnPoints.Add(neutralSpawnPoint);
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomPoint;

        do
        {
            float randomX = Random.Range((-canvasX+1.2f)/2, (canvasX-1f)/2);
            float randomY = Random.Range((-canvasY+1.2f)/2, (canvasY-3.5f)/2);
            randomPoint = new Vector3(randomX, randomY, 1);
        }
        while (!IsValidSpawnPoint(randomPoint));

        return randomPoint;
    }

    bool IsValidSpawnPoint(Vector2 point)
    {
        if (point.x >= -9f && point.x <= -4f &&
            point.y >= -5f && point.y <= -1f)
        {
            return false; // Точка находится внутри канваса speedometer
        }

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(point, spawnPoint) < minDistanceBetweenPlanets)
            {
                return false; // Точка слишком близко к другой точке спавна
            }
        }

        /*// Проверьте, находится ли точка внутри границ канваса speedometer
        RectTransform speedometerRectTransform = speedometer.GetComponent<RectTransform>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 speedometerSize = speedometerRectTransform.rect.size;

        //float speedometerX = speedometerSize.x * speedometerRectTransform.localScale.x;
        //float speedometerY = speedometerSize.y * speedometerRectTransform.localScale.y;

        float speedometerX = speedometerRectTransform.rect.x * canvasRectTransform.localScale.x;
        float speedometerY = speedometerRectTransform.rect.y * canvasRectTransform.localScale.y;*/


        return true; // Точка допустима
    }







}