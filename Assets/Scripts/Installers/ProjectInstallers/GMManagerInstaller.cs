using System;
using UnityEngine;
using Zenject;
public class GameModeManagerInstaller : MonoInstaller
{
    [SerializeField] private GameModeManager gameModeManager;
    public override void InstallBindings()
    {
        Container.Bind<GameModeManager>().FromInstance(gameModeManager).AsSingle().NonLazy();
    }

}