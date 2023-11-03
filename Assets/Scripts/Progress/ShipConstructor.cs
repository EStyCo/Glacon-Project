using UnityEngine;
using Zenject;

public class ShipConstructor : MonoBehaviour
{    
    [Header("Settings X1")]
    [SerializeField] private float bustMoveSpeed;
    [SerializeField] private int bustArmorShip;
    [SerializeField] private float bustDamage;

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

    // »√–Œ 

    private void ChangePlayerUnits(GameObject unit)
    {
        switch (player.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += bustMoveSpeed;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            default:
                break;
        }

        switch (player.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            default:
                break;
        }

        switch (player.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = bustDamage;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = (bustDamage * 2);
                break;
            case 3:
                unit.GetComponent<Unit>().damage = (bustDamage * 2);
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
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 20);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 200);
                break;
            default:
                break;
        }

        switch (player.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // œ–Œ“»¬Õ»  1

    private void ChangeEnemy1Unit(GameObject unit)
    {
        switch (enemy1.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += bustMoveSpeed;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            default:
                break;
        }

        switch (enemy1.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            default:
                break;
        }

        switch (enemy1.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = bustDamage;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
                break;
            case 3:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
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
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 20);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 200);
                break;
            default:
                break;
        }

        switch (enemy1.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // œ–Œ“»¬Õ»  2

    private void ChangeEnemy2Unit(GameObject unit)
    {
        switch (enemy2.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += bustMoveSpeed;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            default:
                break;
        }

        switch (enemy2.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = bustArmorShip;
                break;
            default:
                break;
        }

        switch (enemy2.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = bustDamage;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
                break;
            case 3:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
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
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 20);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 200);
                break;
            default:
                break;
        }

        switch (enemy2.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }

    // œ–Œ“»¬Õ»  3

    private void ChangeEnemy3Unit(GameObject unit)
    {
        switch (enemy3.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += bustMoveSpeed;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += (bustMoveSpeed * 2);
                break;
            default:
                break;
        }

        switch (enemy3.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 0;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = 1;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = 1;
                break;
            default:
                break;
        }

        switch (enemy3.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = bustDamage;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
                break;
            case 3:
                unit.GetComponent<Unit>().damage = bustDamage * 2;
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
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 20);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                cruiser.GetComponent<Cruiser>().armor = (bustArmorShip * 200);
                break;
            default:
                break;
        }

        switch (enemy3.damageUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().damage = bustDamage;
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().damage = (bustDamage * 2);
                cruiser.GetComponent<Cruiser>().OnTurret();
                break;
            default:
                break;
        }
    }
}
