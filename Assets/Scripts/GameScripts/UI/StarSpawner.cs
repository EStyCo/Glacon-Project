using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    private List<Vector2> spawnPoints = new List<Vector2>();

    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform starsParent;
    [SerializeField] private GameObject starPrefab;

    private int numberOfStars;
    private float minDistanceBetweenPlanets = 0.2f;

    private float canvasX;
    private float canvasY;

    private void Awake()
    {
        numberOfStars = Random.Range(60, 121);
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
            Vector3 SpawnPoint = GetRandomSpawnPoint();
            GameObject newStar = Instantiate(starPrefab, SpawnPoint, Quaternion.identity);
            newStar.transform.SetParent(starsParent);
            
            spawnPoints.Add(SpawnPoint);
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
