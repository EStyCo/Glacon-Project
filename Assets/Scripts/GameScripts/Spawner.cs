using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Spawner : MonoBehaviour
{
    [Inject] protected GameModeManager gameModeManager;
    [Inject(Id = "PlayerPlanet")] protected GameObject playerPlanetPrefab;
    [Inject(Id = "NeutralPlanet")] protected GameObject neutralPlanetPrefab;
    [Inject(Id = "Enemy1Planet")] protected GameObject enemy1PlanetPrefab;
    [Inject(Id = "Enemy2Planet")] protected GameObject enemy2PlanetPrefab;
    [Inject(Id = "Enemy3Planet")] protected GameObject enemy3PlanetPrefab;

    protected List<Vector2> spawnPoints = new List<Vector2>();

    [SerializeField] protected Canvas canvasParent;
    [SerializeField] protected GameObject leftTopCanvas;
    [SerializeField] protected GameObject rightBottomCanvas;
    [SerializeField] protected GameObject leftBottomPM;
    [SerializeField] protected GameObject rightTopSM;

    protected GameObject[] enemyPlanets;

    protected float minDistance = 1.1f;

    private void Awake()
    {

    }
    void Start()
    {
        enemyPlanets = new GameObject[3] { enemy1PlanetPrefab, enemy2PlanetPrefab, enemy3PlanetPrefab };

        GenerateObjects();
    }

    protected abstract void GenerateObjects();

    protected Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomPoint;
        Vector2 newVectorX = canvasParent.transform.InverseTransformPoint(rightBottomCanvas.transform.position) * canvasParent.transform.lossyScale.x;
        Vector2 newVectorY = canvasParent.transform.InverseTransformPoint(leftTopCanvas.transform.position) * canvasParent.transform.lossyScale.x;

        do
        {
            float randomX = UnityEngine.Random.Range(newVectorX.x, newVectorY.x);
            float randomY = UnityEngine.Random.Range(newVectorY.y, newVectorX.y);
            randomPoint = new Vector2(randomX, randomY);
        }
        while (!IsValidSpawnPoint(randomPoint));

        return randomPoint;
    }

    protected bool IsValidSpawnPoint(Vector2 point)
    {
        if (leftBottomPM != null)
        {
            Vector2 PM = leftBottomPM.transform.position;

            if (point.x >= PM.x && point.x <= PM.x + 10f &&
                point.y >= PM.y && point.y <= PM.y + 10f)
            {
                return false; // Точка находится внутри запрещенной зоны, Меню Паузы
            }
        }

        if (rightTopSM != null)
        {
            Vector2 selector = rightTopSM.transform.position;

            if (point.x <= selector.x && point.x >= selector.x - 10f &&
                point.y <= selector.y && point.y >= selector.y - 10f)
            {
                return false; // Точка находится внутри запрещенной зоны, Селектора
            }
        }

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(point, spawnPoint) < minDistance)
            {
                return false;
            }
        }

        return true;
    }

}
