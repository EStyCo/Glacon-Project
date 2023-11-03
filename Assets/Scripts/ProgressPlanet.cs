using UnityEngine;
using Zenject;

public class ProgressPlanet : MonoBehaviour
{
    [Inject] private ProgressPlayer player;
    [Inject] private ProgressEnemy1 enemy1;
    [Inject] private ProgressEnemy2 enemy2;
    [Inject] private ProgressEnemy3 enemy3;

    [SerializeField] private float armorPlanet;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject growthEffect;

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
                GetComponent<Planet>().armor = armorPlanet;
                break;
            case 2:
                GetComponent<Planet>().armor = armorPlanet * 2;
                break;
            case 3:
                GetComponent<Planet>().armor = armorPlanet * 2;
                turret.SetActive(true);
                break;
            default:
                GetComponent<Planet>().armor = 0;
                break;
        }

        switch (player.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = 0.05f;
                break;
            case 2:
                GetComponent<Planet>().growthLevel = 0.1f;
                break;
            case 3:
                GetComponent<Planet>().growthLevel = 0.1f; 
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
                GetComponent<Planet>().armor = armorPlanet;
                break;
            case 2:
                GetComponent<Planet>().armor = armorPlanet * 2;
                break;
            case 3:
                GetComponent<Planet>().armor = armorPlanet * 2;
                turret.SetActive(true);
                break;
            default:
                GetComponent<Planet>().armor = 0;
                break;
        }

        switch (enemy1.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = 0.05f;
                break;
            case 2:
                GetComponent<Planet>().growthLevel = 0.1f;
                break;
            case 3:
                GetComponent<Planet>().growthLevel = 0.1f; //
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
                GetComponent<Planet>().armor = armorPlanet;
                break;
            case 2:
                GetComponent<Planet>().armor = armorPlanet * 2;
                break;
            case 3:
                GetComponent<Planet>().armor = armorPlanet * 2;
                turret.SetActive(true);
                break;
            default:
                GetComponent<Planet>().armor = 0;
                break;
        }

        switch (enemy2.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = 0.05f;
                break;
            case 2:
                GetComponent<Planet>().growthLevel = 0.1f;
                break;
            case 3:
                GetComponent<Planet>().growthLevel = 0.1f; //
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
                GetComponent<Planet>().armor = armorPlanet;
                break;
            case 2:
                GetComponent<Planet>().armor = armorPlanet * 2;
                break;
            case 3:
                GetComponent<Planet>().armor = armorPlanet * 2;
                turret.SetActive(true);
                break;
            default:
                GetComponent<Planet>().armor = 0;
                break;
        }

        switch (enemy3.growthPlanet)
        {
            case 1:
                GetComponent<Planet>().growthLevel = 0.1f;
                break;
            case 2:
                GetComponent<Planet>().growthLevel = 0.1f;
                break;
            case 3:
                GetComponent<Planet>().growthLevel = 0.1f; //
                break;
            default:
                GetComponent<Planet>().growthLevel = 0f;
                break;
        }
    }
}
