using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIProgress uiProgress;

    public override void InstallBindings()
    {
        Container.Bind<UIProgress>().FromInstance(uiProgress).AsSingle().NonLazy();
    }
}
