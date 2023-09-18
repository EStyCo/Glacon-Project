using System.Collections.Generic;
using UnityEngine;
using Game;
using Unity.VisualScripting;

public abstract class PlanetSpawner : MonoBehaviour
{
    protected List<Vector2> spawnPoints = new List<Vector2>();

    public GameObject playerPlanetPrefab;
    //public GameObject enemyPlanetPrefab;
    public GameObject neutralPlanetPrefab;

    private float minDistanceBetweenPlanets = 1.1f;

    private float canvasX;
    private float canvasY;

    private void Awake()
    {
        GetGM();
    }
    void Start()
    {
        CalculationSpawnArea();
        GeneratePlanets();
    }
    protected abstract void GetGM();
    private void CalculationSpawnArea()
    { 
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 canvasSize = canvasRectTransform.rect.size;

        canvasX = canvasSize.x * canvasRectTransform.localScale.x;
        canvasY = canvasSize.y * canvasRectTransform.localScale.y;
        Debug.Log(canvasX);
        Debug.Log(canvasY);

    }
    protected abstract void GeneratePlanets();

    protected Vector3 GetRandomSpawnPoint()
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
        if (point.x >= 6.05f && point.x <= 9f &&
            point.y >= 1.75f && point.y <= 5.5f)
        {
            return false; // Точка находится внутри меню паузы
        }
        if (point.x >= -9f && point.x <= -4f &&
            point.y >= -5f && point.y <= -1f)
        {
            return false; // Точка находится внутри спидометра
        }

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(point, spawnPoint) < minDistanceBetweenPlanets)
            {
                return false; // Точка слишком близко к другой точке спавна
            }
        }

        return true; // Точка допустима
    }
}