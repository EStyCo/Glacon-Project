using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BalancePowerInstaller : MonoInstaller
{
    [SerializeField] private BalancePower balancePower;

    public override void InstallBindings()
    {
        Container.Bind<BalancePower>().FromInstance(balancePower).AsSingle().NonLazy();
    }
}
