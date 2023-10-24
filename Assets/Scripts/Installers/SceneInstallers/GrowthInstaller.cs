using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GrowthInstaller : MonoInstaller
{
    [SerializeField] private Growth growth;
    public override void InstallBindings()
    {
        Container.Bind<Growth>().FromInstance(growth).AsSingle().NonLazy();
    }
}
