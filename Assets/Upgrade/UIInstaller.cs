using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIMain uiMain;
    [SerializeField] private UIProgress uiProgress;

    public override void InstallBindings()
    {
        Container.Bind<UIMain>().FromInstance(uiMain).AsSingle().NonLazy();
        Container.Bind<UIProgress>().FromInstance(uiProgress).AsSingle().NonLazy();
    }
}
