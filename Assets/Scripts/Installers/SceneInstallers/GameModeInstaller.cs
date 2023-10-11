using UnityEngine;
using Zenject;
public class GameModeInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPlanetPrefab;
    [SerializeField] private GameObject neutralPlanetPrefab;
    [SerializeField] private GameObject enemy1PlanetPrefab;
    [SerializeField] private GameObject enemy2PlanetPrefab;
    [SerializeField] private GameObject enemy3PlanetPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("PlayerPlanet").FromInstance(playerPlanetPrefab);
        Container.Bind<GameObject>().WithId("NeutralPlanet").FromInstance(neutralPlanetPrefab);
        Container.Bind<GameObject>().WithId("Enemy1Planet").FromInstance(enemy1PlanetPrefab);
        Container.Bind<GameObject>().WithId("Enemy2Planet").FromInstance(enemy2PlanetPrefab);
        Container.Bind<GameObject>().WithId("Enemy3Planet").FromInstance(enemy3PlanetPrefab);
    }

}
