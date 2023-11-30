using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelConfig : MonoBehaviour
{
    [Inject] private ShipConstructor constructor;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    //[Range(0, 3)][SerializeField] private int speed;

    public void Pick1Level()
    {
        ResetProgressEnemy();

        UpgradeEnemy(enemy1, constructor.level1);
        UpgradeEnemy(enemy2, constructor.level1);
        UpgradeEnemy(enemy3, constructor.level1);
    }

    private void UpgradeEnemy(ProgressEnemy enemy, int[] counts)
    {
        int[] values = GetValues();
        GetShipsValue(values[0], enemy) = counts[0];
        GetShipsValue(values[1], enemy) = counts[1];

        values = GetValues();
        GetPlanetsValue(values[0], enemy) = counts[2];
        GetPlanetsValue(values[1], enemy) = counts[3];
    }
    private int[] GetValues()
    {
        int[] values = new int[2];

        do
        {
            values[0] = Random.Range(1, 4);
            values[1] = Random.Range(1, 4);
        }
        while (values[0] == values[1]);

        return values;
    }
    private ref int GetShipsValue(int param, ProgressEnemy enemy)
    {
        switch (param)
        {
            case 1:
                return ref enemy.speedUnit;
            case 2:
                return ref enemy.armorUnit;
            case 3:
                return ref enemy.damageUnit;
            default:
                throw new ArgumentException("Invalid param value", nameof(param));
        }
    }

    private ref int GetPlanetsValue(int param, ProgressEnemy enemy)
    {
        switch (param)
        {
            case 1:
                return ref enemy.armorPlanet;
            case 2:
                return ref enemy.draftPlanet;
            case 3:
                return ref enemy.growthPlanet;
            default:
                throw new ArgumentException("Invalid param value", nameof(param));
        }
    }

    private void ResetProgressEnemy()
    {
        enemy1.ResetProgress();
        enemy2.ResetProgress();
        enemy3.ResetProgress();
    }
}
