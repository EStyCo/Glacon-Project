using UnityEngine;
using Zenject;

public class ProgressPlanet : MonoBehaviour
{
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    [SerializeField] private float armorPlanet;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject shield;

    public void CheckProgress()
    {
        string tagPlanet = gameObject.tag;

        switch (tagPlanet)
        {
            case "PlayerPlanet":
                ChangePlayer();
                break;
            case "Enemy1":
                ChangeEnemy1();
                break;
            case "Enemy2":
                ChangeEnemy2();
                break;
            case "Enemy3":
                ChangeEnemy3();
                break;
            default:
                break;
        }

    }

    private void ChangePlayer()
    {
        turret.SetActive(false);

        switch (player.armorPlanet)
        {
            case 1:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 2:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 3:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                turret.SetActive(true);
                break;

            default:
                shield.SetActive(false);
                break;
        }

        switch (player.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth1Level;
                break;

            case >= 2:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth2Level;
                break;

            default:
                GetComponent<Planet>().growthLevel = 0f; 
                break;
        }
    }

    private void ChangeEnemy1()
    {
        turret.SetActive(false);

        switch (enemy1.armorPlanet)
        {
            case 1:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 2:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 3:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                turret.SetActive(true);
                break;

            default:
                shield.SetActive(false);
                break;
        }

        switch (enemy1.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth1Level;
                break;

            case >= 2:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth2Level;
                break;

            default:
                GetComponent<Planet>().growthLevel = 0f;
                break;
        }
    }

    private void ChangeEnemy2()
    {
        turret.SetActive(false);

        switch (enemy2.armorPlanet)
        {
            case 1:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 2:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 3:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                turret.SetActive(true);
                break;
            default:
                shield.SetActive(false);
                break;
        }

        switch (enemy2.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth1Level;
                break;

            case >= 2:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth2Level;
                break;

            default:
                GetComponent<Planet>().growthLevel = 0f;
                break;
        }
    }

    private void ChangeEnemy3()
    {
        turret.SetActive(false);

        switch (enemy3.armorPlanet)
        {
            case 1:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 2:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                break;

            case 3:
                shield.SetActive(true);
                shield.GetComponent<ShieldPlanet>().ResetShield();
                turret.SetActive(true);
                break;
            default:
                shield.SetActive(false);
                break;
        }

        switch (enemy3.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth1Level;
                break;

            case >= 2:
                GetComponent<Planet>().growthLevel = shipConstructor.bustGrowth2Level;
                break;

            default:
                GetComponent<Planet>().growthLevel = 0f;
                break;
        }
    }
}
