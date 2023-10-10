using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Zenject;

public class SpeedTime : PlanetSpawner
{
    protected override void GetGM()
    {
/*        if (gameModeManager.currentGameMode != GameModeManager.GameMode.SpeedTime)
        {
            gameObject.GetComponent<SpeedTime>().enabled = false;
        }*/
    }
    protected override void GeneratePlanets()
    {
        int numberOfPlanets = GameManager.Instance.planetCount;

        Vector3 playerSpawnPoint = GetRandomSpawnPoint();
        Instantiate(playerPlanetPrefab, playerSpawnPoint, Quaternion.identity);
        spawnPoints.Add(playerSpawnPoint);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 neutralSpawnPoint = GetRandomSpawnPoint();
/*            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);
            Planet planetScript = newPlanet.GetComponent<Planet>();
            planetScript.selectedSize = (Planet.Size)Random.Range(1, 4);
            spawnPoints.Add(neutralSpawnPoint);*/
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
