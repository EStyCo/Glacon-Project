using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneDebugProgress : MonoBehaviour
{
    [Inject] private ProgressPlayer p;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    void Start()
    {
        Debugger.Log($"SpeedU:{p.speedUnit}, ArmorU:{p.armorUnit}, DamageU:{p.damageUnit}, ArmorP:{p.armorPlanet}, Draft:{p.draftPlanet}, Growth:{p.growthPlanet}");

        LogProgress(enemy1);
        LogProgress(enemy2);
        LogProgress(enemy3);
    }

    private void LogProgress(ProgressEnemy enemy)
    {
        Debugger.Log($"SpeedU:{enemy.speedUnit}, ArmorU:{enemy.armorUnit}, DamageU:{enemy.damageUnit}, ArmorP:{enemy.armorPlanet}, Draft:{enemy.draftPlanet}, Growth:{enemy.growthPlanet}");
    }
}
