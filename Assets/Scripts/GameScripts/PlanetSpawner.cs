using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public abstract class PlanetSpawner : MonoBehaviour
{
    protected List<Vector2> spawnPoints = new List<Vector2>();
    [SerializeField] protected Canvas canvasParent;
    [Inject] protected GameModeManager gameModeManager;

    [SerializeField] protected GameObject leftTopCanvas;
    [SerializeField] protected GameObject rightBottomCanvas;
    [SerializeField] protected GameObject leftBottomPM;
    [SerializeField] protected GameObject rightTopSM;

    [Inject(Id = "PlayerPlanet")] protected GameObject playerPlanetPrefab;
    [Inject(Id = "NeutralPlanet")] protected GameObject neutralPlanetPrefab;
    [Inject(Id = "Enemy1Planet")] protected GameObject enemy1PlanetPrefab;
    [Inject(Id = "Enemy2Planet")] protected GameObject enemy2PlanetPrefab;
    [Inject(Id = "Enemy3Planet")] protected GameObject enemy3PlanetPrefab;

    protected GameObject[] enemyPlanets;


    protected float minDistanceBetweenPlanets = 1.1f;

    private void Awake()
    {
        //GetGM();

    }
    void Start()
    {
        enemyPlanets = new GameObject[3] { enemy1PlanetPrefab, enemy2PlanetPrefab, enemy3PlanetPrefab };

        GeneratePlanets();
    }
    protected abstract void GetGM();
    protected abstract void GeneratePlanets();
    protected abstract bool IsValidSpawnPoint(Vector2 point);
    protected Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomPoint;
        /*Vector3 newVector = canvasParent.transform;
        Debug.Log(newVector);*/
        //newVector = canvasParent.transform.InverseTransformPoint(leftTopCanvas.GetPosition()) * canvasParent.transform.lossyScale.x;
        //Vector3 bottomRight = canvasParent.transform.InverseTransformPoint(rightBottomCanvas.GetPosition()) * canvasParent.transform.lossyScale.x;
        Vector2 newVector = canvasParent.transform.InverseTransformPoint(rightBottomCanvas.transform.position) * canvasParent.transform.lossyScale.x;

        do
        {
            float randomX = UnityEngine.Random.Range(newVector.x, newVector.x);
            float randomY = UnityEngine.Random.Range(newVector.y, newVector.y);
            randomPoint = new Vector2(randomX, randomY);
        }
        while (!IsValidSpawnPoint(randomPoint));

        return randomPoint;
    }

}
