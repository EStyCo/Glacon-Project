using Game;
using System;
using UnityEngine;
using Zenject;

public class Classic : Spawner
{
    [Inject(Id = "Enemy1Unit")] private GameObject enemy1UnitPrefab;
    [Inject(Id = "Enemy2Unit")] private GameObject enemy2UnitPrefab;
    [Inject(Id = "Enemy3Unit")] private GameObject enemy3UnitPrefab;
    [Inject(Id = "Enemy1Cruiser")] private GameObject enemy1CruiserPrefab;
    [Inject(Id = "Enemy2Cruiser")] private GameObject enemy2CruiserPrefab;
    [Inject(Id = "Enemy3Cruiser")] private GameObject enemy3CruiserPrefab;

    [Inject] protected BalancePower balancePower;
    [Inject] protected Growth growth;
    [Inject] protected Draft draft;

    private Color[] enemyColors = new Color[4];

    protected override void GenerateObjects()
    {
        Vector2 playerSpawnPoint = GetRandomSpawnPoint(true);

        GameObject playerPlanet = diContainer.InstantiatePrefab(planetPrefab, playerSpawnPoint, Quaternion.identity, t);
        ShipDesign.ChangePlayerSkin(playerPlanet, playerUnitPrefab, playerCruiserPrefab);

        balancePower.listPlanets.Add(playerPlanet.GetComponent<Planet>());
        spawnPoints.Add(playerSpawnPoint);

        SpawnEnemyPlanets();
        SpawnNeutralPlanets(balancePower);

        Invoke("StartGrowthScript", 2f);
        Invoke("StartDraftScript", 2f);

        balancePower.SplitPlanets();
    }

    private void SpawnEnemyPlanets()
    {
        enemyColors[1] = new Color(1.0f, 0.0f, 0.0f); 
        enemyColors[2] = new Color(0.0f, 1.0f, 0.0f); 
        enemyColors[3] = new Color(0.0f, 0.0f, 1.0f);

        int count = gameModeManager.countEnemyPlanets;

        if (count > 3) count = 3;

        for (int i = 1; i <= count; i++)
        {
            Vector2 enemySpawnPoint = GetRandomSpawnPoint(true);
            GameObject newPlanet = diContainer.InstantiatePrefab(planetPrefab, enemySpawnPoint, Quaternion.identity, t);
            newPlanet.tag = "Enemy" + i.ToString();
            newPlanet.GetComponent<SpriteRenderer>().color = enemyColors[i];

            GameObject unitPrefab = GetEnemyPrefabUnit(i);
            GameObject cruiserPrefab = GetEnemyPrefabCruiser(i);

            ShipDesign.ChangeEnemySkin(newPlanet, unitPrefab, cruiserPrefab);

            Type scriptToAdd = GetDifficulty();

            if (scriptToAdd != null)
            {
                GameObject newObject = new GameObject("Enemy" + i.ToString());
                newObject.AddComponent(scriptToAdd);
                newObject.transform.SetParent(gameObject.transform);

                newObject.tag = "Enemy" + i.ToString();
            }

            balancePower.listPlanets.Add(newPlanet.GetComponent<Planet>());
            spawnPoints.Add(enemySpawnPoint);
        }
    }

    private GameObject GetEnemyPrefabUnit(int index)
    {
        switch (index)
        {
            case 1:
                return enemy1UnitPrefab;

            case 2:
                return enemy2UnitPrefab;

            case 3:
                return enemy3UnitPrefab;

            default:
                return null;
        }
    }

    private GameObject GetEnemyPrefabCruiser(int index)
    {
        switch (index)
        {
            case 1:
                return enemy1CruiserPrefab;

            case 2:
                return enemy2CruiserPrefab;

            case 3:
                return enemy3CruiserPrefab;

            default:
                return null;
        }
    }

    private Type GetDifficulty()
    {
        switch (gameModeManager.currentDifficulty)
        {
            case GameModeManager.Difficulty.Easy:
                return typeof(AIEasyController);

            case GameModeManager.Difficulty.Medium:
                return typeof(AIMediumController);

            case GameModeManager.Difficulty.Hard:
                return typeof(AIHardController);

            default:
                return null;
        }
    }

    private void StartGrowthScript()
    {
        growth.CheckMembers();
    }

    private void StartDraftScript()
    {
        draft.CheckMembers();
    }
}
