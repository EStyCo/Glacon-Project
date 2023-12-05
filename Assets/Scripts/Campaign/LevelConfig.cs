using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelConfig : MonoBehaviour
{
    private enum Difficult
    {
        easy = 0,
        medium = 1,
        hard = 2
    }

    public enum Pick
    { 
        None = 0,
        level1 = 1, 
        level2 = 2, 
        level3 = 3
    }
    
    [Inject] private ShipConstructor constructor;
    [Inject] private GameModeManager gmManager;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    [Header("Prepared Progress")]
    [SerializeField] private Pick pick;

    [Header("Difficult")]
    [SerializeField] private Difficult difficult;
    [Range(10, 25)][SerializeField] private int countPlanets;
    [Range(1, 3)][SerializeField] private int enemyes;

    [Header("Enemy 1")]
    [Range(0, 3)][SerializeField] private int speed1;
    [Range(0, 3)][SerializeField] private int armor1;
    [Range(0, 3)][SerializeField] private int damage1;
    [Range(0, 3)][SerializeField] private int armorPlanet1;
    [Range(0, 3)][SerializeField] private int growth1;
    [Range(0, 3)][SerializeField] private int draft1;

    [Header("Enemy 1")]
    [Range(0, 3)][SerializeField] private int speed2;
    [Range(0, 3)][SerializeField] private int armor2;
    [Range(0, 3)][SerializeField] private int damage2;
    [Range(0, 3)][SerializeField] private int armorPlanet2;
    [Range(0, 3)][SerializeField] private int growth2;
    [Range(0, 3)][SerializeField] private int draft2;

    [Header("Enemy 1")]
    [Range(0, 3)][SerializeField] private int speed3;
    [Range(0, 3)][SerializeField] private int armor3;
    [Range(0, 3)][SerializeField] private int damage3;
    [Range(0, 3)][SerializeField] private int armorPlanet3;
    [Range(0, 3)][SerializeField] private int growth3;
    [Range(0, 3)][SerializeField] private int draft3;

    public void LoadLevel()
    {
        ResetProgressEnemy();
        
        ChangeSettings();

        if (!CheckPreparedProgress())
        {
            PickCustomProgress();
        }
    }

    private void PickCustomProgress()
    {
        enemy1.speedUnit = speed1;
        enemy1.armorUnit = armor1;
        enemy1.damageUnit = damage1;
        enemy1.armorPlanet = armorPlanet1;
        enemy1.growthPlanet = growth1;
        enemy1.draftPlanet = draft1;
        
        enemy2.speedUnit = speed2;
        enemy2.armorUnit = armor2;
        enemy2.damageUnit = damage2;
        enemy2.armorPlanet = armorPlanet2;
        enemy2.growthPlanet = growth2;
        enemy2.draftPlanet = draft2;
        
        enemy3.speedUnit = speed3;
        enemy3.armorUnit = armor3;
        enemy3.damageUnit = damage3;
        enemy3.armorPlanet = armorPlanet3;
        enemy3.growthPlanet = growth3;
        enemy3.draftPlanet = draft3;
    }

    private void ChangeSettings()
    {
        gmManager.planetCount = countPlanets;
        gmManager.countEnemyPlanets = enemyes;
        GetDifficult();
    }

    private void GetDifficult()
    {
        switch (difficult)
        {
            case Difficult.easy:
                gmManager.ChangeDifficulty(1);
                break;
            case Difficult.medium:
                gmManager.ChangeDifficulty(2);
                break;
            case Difficult.hard:
                gmManager.ChangeDifficulty(3);
                break;
                default:
                    break;
        }
    }

    private void PickLevelProgress(int[] counts)
    {
        UpgradeEnemy(enemy1, counts);
        UpgradeEnemy(enemy2, counts);
        UpgradeEnemy(enemy3, counts);
    }

    private bool CheckPreparedProgress()
    {
        switch (pick)
        {
            case Pick.None:
                return false;
            case Pick.level3:
                PickLevelProgress(constructor.level3);
                return true;
            case Pick.level2:
                PickLevelProgress(constructor.level2);
                return true;
            case Pick.level1:
                PickLevelProgress(constructor.level1);
                return true;
            default:
                Debug.Log("Error prepared progress");
                return false;
        }
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
