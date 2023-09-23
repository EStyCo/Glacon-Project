using Game;
using UnityEngine;

public class Classic : PlanetSpawner
{
    public GameObject[] enemyPlanets = new GameObject[3];


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
        Vector2 playerSpawnPoint = GetRandomSpawnPoint();
        GameObject playerPlanet = Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity); 
        playerPlanet.transform.parent = canvasParent.transform;

        spawnPoints.Add(playerSpawnPoint);

        SpawnEnemyPlanets();

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector2 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            newPlanet.transform.parent = canvasParent.transform;
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
            Vector2 enemySpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(enemyPlanets[i], enemySpawnPoint, Quaternion.identity);
            newPlanet.transform.parent = canvasParent.transform;
            spawnPoints.Add(enemySpawnPoint);
        }
    }
}
