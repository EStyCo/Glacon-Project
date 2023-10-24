using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Zenject;

public class SpeedTime : Spawner
{
    protected override void GenerateObjects()
    {
        Vector3 playerSpawnPoint = GetRandomSpawnPoint(false);
        Instantiate(planetPrefab, playerSpawnPoint, Quaternion.identity);
        spawnPoints.Add(playerSpawnPoint);

        SpawnNeutralPlanets();
    }
}
