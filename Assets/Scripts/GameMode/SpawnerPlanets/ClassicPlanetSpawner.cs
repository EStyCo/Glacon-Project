using Game;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class Classic : PlanetSpawner
{
    protected override void GetGM()
    {
        /*if (gameModeManager.currentGameMode != GameModeManager.GameMode.Classic)
        { 
            gameObject.GetComponent<Classic>().enabled = false;
        }*/
    }

    protected override void GeneratePlanets()
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
            //Planet planetScript = newPlanet.GetComponent<Planet>();
            //planetScript.selectedSize = (Planet.Size)Random.Range(1, 4);
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
    protected override bool IsValidSpawnPoint(Vector2 point)
    {
        Vector2 PM = leftBottomPM.transform.position;
        Vector2 selector = rightTopSM.transform.position;

        if (point.x >= PM.x && point.x <= PM.x + 10f &&
            point.y >= PM.y && point.y <= PM.y + 10f)
        {
            return false; // Точка находится внутри запрещенной зоны, Меню Паузы
        }
        if (point.x <= selector.x && point.x >= selector.x - 10f &&
            point.y <= selector.y && point.y >= selector.y - 10f)
        {
            return false; // Точка находится внутри запрещенной зоны, Селектора
        }
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
