using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManagerInstaller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public override void InstallBindings()
    {
        Container.Bind<BalancePower>().FromInstance(balancePower).AsSingle().NonLazy();
    }
}
