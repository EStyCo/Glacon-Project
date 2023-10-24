using UnityEngine;
using Zenject;

public class BlackHoles : Spawner
{
    [Inject(Id = "BlackHole")] private GameObject blackHolePrefab;
    protected override void GenerateObjects()
    {
        int numbersOfBlackHoles = Random.Range(1, 4);


        for (int i = 0; i <= numbersOfBlackHoles; i++)
        {
            Vector2 newSpawnPoint = GetRandomSpawnPoint(false);

            GameObject blackHole = Instantiate(blackHolePrefab, newSpawnPoint, Quaternion.identity);
            blackHole.transform.SetParent(canvasParent.transform, true);
            spawnPoints.Add(newSpawnPoint);
        }
    }
}
