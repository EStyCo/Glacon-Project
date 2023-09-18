using Game;
using UnityEngine;

public class Classic : PlanetSpawner
{
    public GameObject enemyPlanetPrefab;
    public GameObject enemyPlanetPrefab2;
    public GameObject enemyPlanetPrefab3;


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

        SpawnEnemyPlanets();

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            Planet planetScript = newPlanet.GetComponent<Planet>();
            planetScript.selectedSize = (Planet.Size)Random.Range(1, 4);
            spawnPoints.Add(neutralSpawnPoint);
        }
    }
    private void SpawnEnemyPlanets()
    {
        int count = GameModeManager.Instance.countEnemyPlanets;
        
        if (count > 3) count = 3;

        for (int i = 0; i < count; i++)
        { 
            
        }

        Vector3 enemySpawnPoint = GetRandomSpawnPoint();
        Instantiate(enemyPlanetPrefab, enemySpawnPoint, Quaternion.identity);
        spawnPoints.Add(enemySpawnPoint);
    }
}
