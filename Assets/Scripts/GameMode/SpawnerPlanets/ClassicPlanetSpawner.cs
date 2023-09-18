using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Unity.VisualScripting;

public class Classic : PlanetSpawner
{
    public GameObject enemyPlanetPrefab;

    protected override void GetGM()
    {
        this.enabled = false;
        if (GameModeManager.Instance.currentGameMode == GameModeManager.GameMode.Classic)
        { 
            this.enabled = true;
        }
    }

    protected override void GeneratePlanets()
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
}
