using UnityEngine;
using Zenject;

public class Tutorial : Spawner
{
    [Inject(Id = "Enemy1Unit")] private GameObject enemy1UnitPrefab;
    [Inject(Id = "Enemy2Unit")] private GameObject enemy2UnitPrefab;
    [Inject(Id = "Enemy3Unit")] private GameObject enemy3UnitPrefab;
    [Inject(Id = "Enemy1Cruiser")] private GameObject enemy1CruiserPrefab;
    [Inject(Id = "Enemy2Cruiser")] private GameObject enemy2CruiserPrefab;
    [Inject(Id = "Enemy3Cruiser")] private GameObject enemy3CruiserPrefab;

    [Inject] protected GameManager gameManager;
    [Inject] protected BalancePower balancePower;
    [Inject] protected Growth growth;
    [Inject] protected Draft draft;

    protected override void GenerateObjects()
    {
        Vector2 playerSpawnPoint = GetRandomSpawnPoint(false);

        GameObject playerPlanet = diContainer.InstantiatePrefab(planetPrefab, playerSpawnPoint, Quaternion.identity, t);
        ShipDesign.ChangePlayerSkin(gameManager, playerPlanet, playerUnitPrefab, playerCruiserPrefab);

        balancePower.listPlanets.Add(playerPlanet.GetComponent<Planet>());
        spawnPoints.Add(playerSpawnPoint);
        listPlanets.Add(playerPlanet);

        SpawnEnemyPlanets();
        SpawnNeutralPlanets(balancePower, 3);

        balancePower.SplitPlanets();
    }

    private void SpawnEnemyPlanets()
    {
        Color enemyColor = new Color(243 / 255f, 71 / 255f, 35 / 255f, 255 / 255f);

        Vector2 enemySpawnPoint = GetRandomSpawnPoint(false);
        GameObject newPlanet = diContainer.InstantiatePrefab(planetPrefab, enemySpawnPoint, Quaternion.identity, t);
        newPlanet.tag = "Enemy" + 1.ToString();
        newPlanet.GetComponent<SpriteRenderer>().color = enemyColor;
        listPlanets.Add(newPlanet);

        GameObject unitPrefab = enemy1UnitPrefab;
        GameObject cruiserPrefab = enemy1CruiserPrefab;

        ShipDesign.ChangeEnemySkin(newPlanet, unitPrefab, cruiserPrefab);


        newPlanet.AddComponent<AIEasyController>();

        balancePower.listPlanets.Add(newPlanet.GetComponent<Planet>());
        spawnPoints.Add(enemySpawnPoint);

    }

    public void NewGeneration()
    {
        spawnPoints.Clear();
        balancePower.StopScript();
        balancePower.DestroyShips();

        DeletePlanets();
        GenerateObjects();
    }

    public void NewNew()
    {
        SpawnNeutralPlanets(balancePower, 1);
    }
}
