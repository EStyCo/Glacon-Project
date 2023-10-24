using UnityEngine;
using Zenject;

public class UnitConstructor : MonoBehaviour
{
    public static UnitConstructor Instance { get; private set; }

    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                ChangeEnemy1Unit(unit);
                break;
            case "Enemy3":
                ChangeEnemy1Unit(unit);
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
                ChangePlayerCruisers(cruiser);
                break;
            case "Enemy2":
                ChangePlayerCruisers(cruiser);
                break;
            case "Enemy3":
                ChangePlayerCruisers(cruiser);
                break;
            default:
                break;
        }
    }

    private void ChangePlayerUnits(GameObject unit)
    {
        switch (player.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += 0.2f;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += 0.4f;
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += 0.4f;
                break;
            default:
                break;
        }

        switch (player.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 20;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = 50;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = 50;
                break;
            default:
                break;
        }

        switch (player.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = 20;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = 50;
                break;
            case 3:
                unit.GetComponent<Unit>().damage = 50;
                break;
            default:
                break;
        }
    }

    public void ChangePlayerCruisers(GameObject cruiser)
    {
        switch (player.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            default:
                break;
        }
    }

    public void ChangeEnemy1Cruisers(GameObject cruiser)
    {
        switch (enemy1.armorUnit)
        {
            case 1:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            case 2:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            case 3:
                cruiser.GetComponent<Cruiser>().ShowShield(true);
                break;
            default:
                break;
        }
    }

    private void ChangeEnemy1Unit(GameObject unit)
    {
        switch (enemy1.speedUnit)
        {
            case 1:
                unit.GetComponent<Unit>().movementSpeed += 0.2f;
                break;
            case 2:
                unit.GetComponent<Unit>().movementSpeed += 0.4f;
                break;
            case 3:
                unit.GetComponent<Unit>().movementSpeed += 0.4f;
                break;
            default:
                break;
        }

        switch (enemy1.armorUnit)
        {
            case 1:
                unit.GetComponent<Unit>().armor = 20;
                break;
            case 2:
                unit.GetComponent<Unit>().armor = 50;
                break;
            case 3:
                unit.GetComponent<Unit>().armor = 50;
                break;
            default:
                break;
        }

        switch (enemy1.damageUnit)
        {
            case 1:
                unit.GetComponent<Unit>().damage = 20;
                break;
            case 2:
                unit.GetComponent<Unit>().damage = 50;
                break;
            case 3:
                unit.GetComponent<Unit>().damage = 50;
                break;
            default:
                break;
        }
    }
}
