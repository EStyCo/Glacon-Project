using UnityEngine;

public class MainSceneSpawner : Spawner
{
    private string[] tags = new string[5] { "Enemy1", "Enemy2", "Enemy3", "NeutralPlanet", "PlayerPlanet", };

    [SerializeField] private GameObject mainPlanetPrefab;
    [SerializeField] private GameObject blackHole;
    [SerializeField] private GameObject parentBlackHole;

    protected override void GenerateObjects()
    {
        int numberOfPlanets = Random.Range(6, 10);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector2 neutralSpawnPoint = GetRandomSpawnPoint(false);
            GameObject newPlanet = Instantiate(mainPlanetPrefab, neutralSpawnPoint, Quaternion.identity);

            int randomIndex = Random.Range(0, tags.Length);
            newPlanet.tag = tags[randomIndex];

            newPlanet.transform.SetParent(canvasParent.transform, true);
            MainPlanet planetScript = newPlanet.GetComponent<MainPlanet>();
            planetScript.selectedSize = (MainPlanet.Size)Random.Range(1, 4);
            spawnPoints.Add(neutralSpawnPoint);
        }
        GenerateBlackHole();
    }
    private void GenerateBlackHole()
    {
        int countOfBlachHoles = Random.Range(1, 4);

        for (int i = 0; i < countOfBlachHoles; i++)
        {
            Vector2 SpawnPoint = GetRandomSpawnPoint(false);
            GameObject newBlackHole = Instantiate(blackHole, SpawnPoint, Quaternion.identity);


            newBlackHole.transform.SetParent(parentBlackHole.transform, true);
            spawnPoints.Add(SpawnPoint);
        }
    }
}