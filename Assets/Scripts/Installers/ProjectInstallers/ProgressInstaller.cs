using UnityEngine;
using Zenject;

public class ProgressInstaller : MonoInstaller
{
    [SerializeField] private ShipConstructor shipConstructor;
    [SerializeField] private ProgressPlayer progressPlayer;
    [SerializeField] private ProgressEnemy1 progressEnemy1;
    [SerializeField] private ProgressEnemy2 progressEnemy2;
    [SerializeField] private ProgressEnemy3 progressEnemy3;

    public override void InstallBindings()
    {
        Container.Bind<ShipConstructor>().FromInstance(shipConstructor).AsSingle().NonLazy();
        Container.Bind<ProgressPlayer>().FromInstance(progressPlayer).AsSingle().NonLazy();
        Container.Bind<ProgressEnemy1>().FromInstance(progressEnemy1).AsSingle().NonLazy();
        Container.Bind<ProgressEnemy2>().FromInstance(progressEnemy2).AsSingle().NonLazy();
        Container.Bind<ProgressEnemy3>().FromInstance(progressEnemy3).AsSingle().NonLazy();
    }
}
