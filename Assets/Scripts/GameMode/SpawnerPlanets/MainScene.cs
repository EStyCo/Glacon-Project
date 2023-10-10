/*using Game;
using UnityEngine;

public class MainSceneSpawner : PlanetSpawner
{
    private string[] tags = new string[5] { "Enemy1", "Enemy2", "Enemy3", "NeutralPlanet", "PlayerPlanet", };

    [SerializeField] private GameObject blackHole;
    [SerializeField] private GameObject parentBlackHole;

    protected override void GetGM()
    { }

    protected override void GeneratePlanets()
    {
        int numberOfPlanets = Random.Range(6, 10);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector2 neutralSpawnPoint = GetRandomSpawnPoint();
            GameObject newPlanet = Instantiate(neutralPlanetPrefab, neutralSpawnPoint, Quaternion.identity);

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
            Vector2 SpawnPoint = GetRandomSpawnPoint();
            GameObject newBlackHole = Instantiate(blackHole, SpawnPoint, Quaternion.identity);


            newBlackHole.transform.SetParent(parentBlackHole.transform, true);
            spawnPoints.Add(SpawnPoint);
        }
    }

    protected override bool IsValidSpawnPoint(Vector2 point)
    {
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
*/