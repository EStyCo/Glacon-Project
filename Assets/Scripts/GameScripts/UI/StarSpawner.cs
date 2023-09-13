using System.Collections.Generic;
using UnityEngine;
using Game;

public class StarSpawner : MonoBehaviour
{
    private List<Vector2> spawnPoints = new List<Vector2>();

    public GameObject starPrefab;

    public Canvas canvas;

    private int numberOfStars;
    private float minDistanceBetweenPlanets = 0.2f;

    private float canvasX;
    private float canvasY;

    private void Awake()
    {
        numberOfStars = Random.Range(60, 91);
    }
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


        for (int i = 0; i < numberOfStars; i++)
        {
            Vector3 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject newStar = Instantiate(starPrefab, neutralSpawnPoint, Quaternion.identity);
            
            spawnPoints.Add(neutralSpawnPoint);
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomPoint;

        do
        {
            float randomX = Random.Range((-canvasX - 1f) / 2, (canvasX + 1f) / 2);
            float randomY = Random.Range((-canvasY - 1f) / 2, (canvasY + 1f) / 2);
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
