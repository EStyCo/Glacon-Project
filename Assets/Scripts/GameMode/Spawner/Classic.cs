using Game;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class Classic : Spawner
{
    protected override void GenerateObjects()
    {
        int numberOfPlanets = GameManager.Instance.planetCount;

        Vector2 playerSpawnPoint = GetRandomSpawnPoint();

        GameObject playerPlanet = Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity);
        playerPlanet.transform.SetParent(canvasParent.transform, true);
        spawnPoints.Add(playerSpawnPoint);

        SpawnEnemyPlanets();

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector2 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject neutralPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            
            neutralPlanet.transform.SetParent(canvasParent.transform, true);
            neutralPlanet.GetComponent<Planet>().selectedSize = (Planet.Size)Random.Range(1, 4);
            spawnPoints.Add(neutralSpawnPoint);
        }
    }
    private void SpawnEnemyPlanets()
    {
        int count = gameModeManager.countEnemyPlanets;
        
        if (count > 3) count = 3;

        for (int i = 0; i < count; i++)
        {
            Vector2 enemySpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(enemyPlanets[i], enemySpawnPoint, Quaternion.identity);
            newPlanet.transform.SetParent(canvasParent.transform, true);
            spawnPoints.Add(enemySpawnPoint);
        }
    }
}
