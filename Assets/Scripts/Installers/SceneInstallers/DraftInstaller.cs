using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class DraftInstaller : MonoInstaller
{
    [SerializeField] private Draft draft;

    public override void InstallBindings()
    {
        Container.Bind<Draft>().FromInstance(draft).AsSingle().NonLazy();
    }
}
