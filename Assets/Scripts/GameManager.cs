using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class GameManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public static GameManager Instance { get; private set; }

    public bool isPaused = false;
    public int planetCount;
    public int skinUnits = 3;
    public Color colorUnits;
    public LayerMask planetLayer;

    private List<Planet> selectedPlanets = new List<Planet>();
    private Planet targetPlanet;
    public SpriteResolver skinUnitsPrefab;
    public SpriteRenderer unitPrefab;
    public GameObject enemyPrefab;

    private void Start()
    {
        Color startingColor = new Color(1f, 1f, 1f, 1f);
        ChangeSkinUnits();
        ChangeColornUnits(startingColor);
    }
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
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, planetLayer);

            if (hit.collider != null)
            {
                Planet planet = hit.collider.GetComponent<Planet>();
                if (planet != null)
                {
                    if (planet.tag != "NeutralPlanet" && planet.tag != "EnemyPlanet" && !selectedPlanets.Contains(planet))
                    {
                        TogleListPlanet(planet);
                        planet = null;
                    }
                    else if (planet.tag != "NeutralPlanet" && planet.tag != "EnemyPlanet" && selectedPlanets.Contains(planet))
                    {
                        TogleListPlanet(planet);
                        planet = null;
                    }
                    else if (planet.tag == "PlayerPlanet" && selectedPlanets != null && targetPlanet == null)
                    {
                        targetPlanet = planet;
                        SendUnits();
                        targetPlanet = null;
                    }
                    /*else if (planet.tag != "NeutralPlanet" && planet.tag != "EnemyPlanet" && isRightButton && selectedPlanets.Contains(planet))
                    {
                        TogleListPlanet(planet);
                        planet = null;
                    }*/
                    else if ((planet.tag == "NeutralPlanet" || planet.tag == "EnemyPlanet") && selectedPlanets != null && targetPlanet == null)
                    {
                        targetPlanet = planet;
                        SendUnits();
                        targetPlanet = null;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, planetLayer);

            if (hit.collider != null)
            {
                Planet planet = hit.collider.GetComponent<Planet>();
                if (planet != null)
                {
                    if (planet.tag != "NeutralPlanet" && planet.tag != "EnemyPlanet" && planet.tag == "PlayerPlanet")
                    {
                        targetPlanet = planet;
                        SendUnits();
                        targetPlanet = null;
                    }
                }
            }
        }
    }
    private void TogleListPlanet(Planet planet)
    {
        if (selectedPlanets.Contains(planet))
        {
            selectedPlanets.Remove(planet);
            planet.DeselectPlanet();
        }
        else
        {
            selectedPlanets.Add(planet);
            planet.SelectPlanet();
        }
    }

    private void SendUnits()
    {
        if (selectedPlanets.Count > 0)
        {
            foreach (Planet planet in selectedPlanets)
            {
                planet.SendUnitsToPlanet(targetPlanet);
            }
        }
        ClearSelectedPlanets();
    }
    private void ClearSelectedPlanets()
    {
        foreach (Planet planet in selectedPlanets)
        {
            planet.DeselectPlanet();
        }
        selectedPlanets.Clear();
    }
    public void ChangeSkinUnits()
    {
        if (skinUnits == 1)
        {
            skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship1");
        }
        if (skinUnits == 2)
        {
            skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship2");
        }
        if (skinUnits == 3)
        {
            skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship3");
        }
        if (skinUnits == 4)
        {
            skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship4");
        }
        if (skinUnits == 5)
        {
            skinUnitsPrefab.SetCategoryAndLabel("Ships", "Ship5");
        }
    }
    public void ChangeColornUnits(Color color)
    {
        unitPrefab.color = color;
        colorUnits = color;
    }
    public void ChangeRank(int value)
    {
        Difficulty selectedDifficulty = (Difficulty)value;

        AIEasyController easy = enemyPrefab.GetComponent<AIEasyController>();
        AIMediumController medium = enemyPrefab.GetComponent<AIMediumController>();
        AIHardController hard = enemyPrefab.GetComponent<AIHardController>();

        easy.enabled = false;
        medium.enabled = false;
        hard.enabled = false;

        if (selectedDifficulty == Difficulty.Easy) easy.enabled = true;
        else if (selectedDifficulty == Difficulty.Medium) medium.enabled = true;
        else if (selectedDifficulty == Difficulty.Hard) hard.enabled = true;
    }

}
