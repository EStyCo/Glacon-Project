using UnityEngine;
using Zenject;

public class ShipConstructor : MonoBehaviour
{
    [Header("Ships")]
    public float speedUnits;
    public float speedCruisers;

    [Header("Cruisers")]
    public int healthCruisers;
    [SerializeField] private int armorDreadnought;

    [Header("Planet")]
    public int minUnitsPlayer;
    public int maxUnitsPlayer;
    public int startUnitsEnemy;
    public int minNeutralUnits;
    public int maxNeutralUnits;

    [Header("Shield Planet")]
    public int restTimerShield;    
    public float speedShield;
    public float radiusShield;
    public int healthShield;

    [Header("Turret")]
    public float reloadSpeed;
    public float bulletSpeed;
    public float rangeFly;

    [Header("Ships Upgrade")]
    [SerializeField] private float bustSpeed1Level;
    [SerializeField] private float bustSpeed2Level;
    [SerializeField] private float bustDamage1Level;
    [SerializeField] private float bustDamage2Level;
    [SerializeField] private int bustArmorUnits;

    [Header("Draft")]
    public int firstTimerStart;
    public int firstTimerEnd;
    public int cycleTimerStart;
    public int cycleTimerEnd;
    public int units1LevelStart;
    public int units1LevelEnd;
    public int units2LevelStart;
    public int units2LevelEnd;
    public int chanceDraft1Level;
    public int chanceDraft2Level;
    public int chanceSpawnCruisers;

    [Header("Growth")]
    public int timerStart;
    public int timerEnd;
    public float bustGrowth1Level;
    public float bustGrowth2Level;

    [Header("Progress Enemy")]
    [Range(0, 3)] public int[] level1 = new int[4];
    [Range(0, 3)] public int[] level2 = new int[4];
    [Range(0, 3)] public int[] level3 = new int[4];


    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;



    public void ChangeUnit(GameObject unit)
    {
        string tagUnit = unit.tag;
        switch (tagUnit)
        {
            case "PlayerPlanet":
                ChangePlayerUnits(unit);
                break;
            case "Enemy1":
                ChangeEnemy1Unit(unit);
                break;
            case "Enemy2":
                ChangeEnemy2Unit(unit);
                break;
            case "Enemy3":
                ChangeEnemy3Unit(unit);
                break;
            default:
                break;
        }
    }

    public void ChangeCruiser(GameObject cruiser)
    {
        string tagCruiser = cruiser.tag;
        switch (tagCruiser)
        {
            case "PlayerPlanet":
                ChangePlayerCruisers(cruiser);
                break;
            case "Enemy1":
                ChangeEnemy1Cruisers(cruiser);
                break;
            case "Enemy2":
                ChangeEnemy2Cruisers(cruiser);
                break;
            case "Enemy3":
                ChangeEnemy3Cruisers(cruiser);
                break;
            default:
                break;
        }
    }

    // ÈÃÐÎÊ

    private void ChangePlayerUnits(GameObject unitPrefab)
    {
        unitPrefab.TryGetComponent(out Unit unit);

        switch (player.speedUnit)
        {
            case 1:
                unit.movementSpeed += bustSpeed1Level;
                break;
            case >= 2:
                unit.movementSpeed += bustSpeed2Level;
                break;
            default:
                break;
        }

        switch (player.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;

            case >= 2:
                unit.GetComponent<Unit>().armor = bustArmorUnits;
                break;

            default:
                break;
        }

        switch (player.damageUnit)
        {
            case 1:
                unit.damage = bustDamage1Level;
                break;
            case >= 2:
                unit.damage = bustDamage2Level;
                break;
            default:
                break;
        }
    }

    public void ChangePlayerCruisers(GameObject cruiser)
    {
        switch (player.speedUnit)
        {
            case 3:
                cruiser.GetComponent<Cruiser>().OnAirCraftSpawner();
                break;
            default:
                break;
        }

        switch (player.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().armor = 0;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().armor = (bustArmorUnits * healthCruisers);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (armorDreadnought);
                break;
            default:
                break;
        }

        switch (player.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage1Level;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // ÏÐÎÒÈÂÍÈÊ 1

    private void ChangeEnemy1Unit(GameObject unitPrefab)
    {
        unitPrefab.TryGetComponent(out Unit unit);

        switch (enemy1.speedUnit)
        {
            case 1:
                unit.movementSpeed += bustSpeed1Level;
                break;
            case >= 2:
                unit.movementSpeed += bustSpeed2Level;
                break;
            default:
                break;
        }

        switch (enemy1.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;

            case >= 2:
                unit.GetComponent<Unit>().armor = bustArmorUnits;
                break;

            default:
                break;
        }

        switch (enemy1.damageUnit)
        {
            case 1:
                unit.damage = bustDamage1Level;
                break;
            case >= 2:
                unit.damage = bustDamage2Level;
                break;
            default:
                break;
        }
    }

    public void ChangeEnemy1Cruisers(GameObject cruiser)
    {
        switch (enemy1.speedUnit)
        {
            case 3:
                cruiser.GetComponent<Cruiser>().OnAirCraftSpawner();
                break;
            default:
                break;
        }

        switch (enemy1.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().armor = 0;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().armor = (bustArmorUnits * healthCruisers);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (armorDreadnought);
                break;
            default:
                break;
        }

        switch (enemy1.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage1Level;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // ÏÐÎÒÈÂÍÈÊ 2

    private void ChangeEnemy2Unit(GameObject unitPrefab)
    {
        unitPrefab.TryGetComponent(out Unit unit);

        switch (enemy2.speedUnit)
        {
            case 1:
                unit.movementSpeed += bustSpeed1Level;
                break;
            case >= 2:
                unit.movementSpeed += bustSpeed2Level;
                break;
            default:
                break;
        }

        switch (enemy2.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;

            case >= 2:
                unit.GetComponent<Unit>().armor = bustArmorUnits;
                break;

            default:
                break;
        }

        switch (enemy2.damageUnit)
        {
            case 1:
                unit.damage = bustDamage1Level;
                break;
            case >= 2:
                unit.damage = bustDamage2Level;
                break;
            default:
                break;
        }
    }

    public void ChangeEnemy2Cruisers(GameObject cruiser)
    {
        switch (enemy2.speedUnit)
        {
            case 3:
                cruiser.GetComponent<Cruiser>().OnAirCraftSpawner();
                break;
            default:
                break;
        }

        switch (enemy2.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().armor = 0;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().armor = (bustArmorUnits * healthCruisers);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (armorDreadnought);
                break;
            default:
                break;
        }

        switch (enemy2.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage1Level;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // ÏÐÎÒÈÂÍÈÊ 3 

    private void ChangeEnemy3Unit(GameObject unitPrefab)
    {
        unitPrefab.TryGetComponent(out Unit unit);

        switch (enemy3.speedUnit)
        {
            case 1:
                unit.movementSpeed += bustSpeed1Level;
                break;
            case >= 2:
                unit.movementSpeed += bustSpeed2Level;
                break;
            default:
                break;
        }

        switch (enemy3.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;

            case >= 2:
                unit.GetComponent<Unit>().armor = bustArmorUnits;
                break;

            default:
                break;
        }

        switch (enemy3.damageUnit)
        {
            case 1:
                unit.damage = bustDamage1Level;
                break;
            case >= 2:
                unit.damage = bustDamage2Level;
                break;
            default:
                break;
        }
    }

    public void ChangeEnemy3Cruisers(GameObject cruiser)
    {
        switch (enemy3.speedUnit)
        {
            case 3:
                cruiser.GetComponent<Cruiser>().OnAirCraftSpawner();
                break;
            default:
                break;
        }

        switch (enemy3.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().armor = 0;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().armor = (bustArmorUnits * healthCruisers);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (armorDreadnought);
                break;
            default:
                break;
        }

        switch (enemy3.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage1Level;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = bustDamage2Level;
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }
}
