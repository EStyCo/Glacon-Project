using UnityEngine;
using Zenject;
public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject playerUnitPrefab;
    [SerializeField] private GameObject enemy1UnitPrefab;
    [SerializeField] private GameObject enemy2UnitPrefab;
    [SerializeField] private GameObject enemy3UnitPrefab;
    [SerializeField] private GameObject playerCruiserPrefab;
    [SerializeField] private GameObject enemy1CruiserPrefab;
    [SerializeField] private GameObject enemy2CruiserPrefab;
    [SerializeField] private GameObject enemy3CruiserPrefab;
    [SerializeField] private GameObject bullet;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId("Portal").FromInstance(portalPrefab);
        Container.Bind<GameObject>().WithId("BlackHole").FromInstance(blackHolePrefab);
        Container.Bind<GameObject>().WithId("Planet").FromInstance(planetPrefab);
        Container.Bind<GameObject>().WithId("PlayerUnit").FromInstance(playerUnitPrefab);
        Container.Bind<GameObject>().WithId("Enemy1Unit").FromInstance(enemy1UnitPrefab);
        Container.Bind<GameObject>().WithId("Enemy2Unit").FromInstance(enemy2UnitPrefab);
        Container.Bind<GameObject>().WithId("Enemy3Unit").FromInstance(enemy3UnitPrefab);
        Container.Bind<GameObject>().WithId("PlayerCruiser").FromInstance(playerCruiserPrefab);
        Container.Bind<GameObject>().WithId("Enemy1Cruiser").FromInstance(enemy1CruiserPrefab);
        Container.Bind<GameObject>().WithId("Enemy2Cruiser").FromInstance(enemy2CruiserPrefab);
        Container.Bind<GameObject>().WithId("Enemy3Cruiser").FromInstance(enemy3CruiserPrefab);
        Container.Bind<GameObject>().WithId("Bullet").FromInstance(bullet);
    }

}
