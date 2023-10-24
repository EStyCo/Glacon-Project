using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Portals : Spawner
{
    [Inject(Id = "Portal")] private GameObject portalPrefab;

    public List<GameObject> listPortals = new List<GameObject>();

    protected override void GenerateObjects()
    {
        int numbersOfPortals = 2;

        for (int i = 0; i < numbersOfPortals; i++)
        {
            Vector2 newSpawnPoint = GetRandomSpawnPoint(false);

            GameObject portal = Instantiate(portalPrefab, newSpawnPoint, Quaternion.identity);
            portal.transform.SetParent(canvasParent.transform, true);
            spawnPoints.Add(newSpawnPoint);
            listPortals.Add(portal);

            // Подписываемся на событие OnDestroy
            portal.GetComponent<Portal>().OnDestroyEvent += HandlePortalDestroyed;
        }
    }

    private void Update()
    {
        if (listPortals.Count < 2)
        {
            GenerateNewObjects();
        }
    }

    private void GenerateNewObjects()
    {
        if (listPortals.Count < 5)
        {
            int randomIndex = Random.Range(0, 3);

            for (int i = 0; i < randomIndex; i++)
            {
                Vector2 newSpawnPoint = GetRandomSpawnPoint(false);

                GameObject portal = Instantiate(portalPrefab, newSpawnPoint, Quaternion.identity);
                portal.transform.SetParent(canvasParent.transform, true);
                spawnPoints.Add(newSpawnPoint);
                listPortals.Add(portal);

                // Подписываемся на событие OnDestroy для новых порталов
                portal.GetComponent<Portal>().OnDestroyEvent += HandlePortalDestroyed;
            }
        }
    }

    private void HandlePortalDestroyed(Portal portal)
    {
        if (listPortals.Contains(portal.gameObject))
        {
            listPortals.Remove(portal.gameObject);
        }

        GenerateNewObjects();
    }

    private void RefreshListPortals()
    {
        listPortals.Clear();
        Portal[] portals = FindObjectsOfType<Portal>();

        foreach (Portal portal in portals)
        {
            listPortals.Add(portal.gameObject);

            // Подписываемся на событие OnDestroy для существующих порталов
            portal.OnDestroyEvent += HandlePortalDestroyed;
        }
    }
}
