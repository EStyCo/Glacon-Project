using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalancePower : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image playerFill;
    [SerializeField] private UnityEngine.UI.Image enemy1Fill;
    [SerializeField] private UnityEngine.UI.Image enemy2Fill;
    [SerializeField] private UnityEngine.UI.Image enemy3Fill;

    private int unitsCount = 0;

    public List<List<Planet>> planetLists;

    public List<Planet> playerPlanet = new List<Planet>();
    public List<Planet> enemy1Planet = new List<Planet>();
    public List<Planet> enemy2Planet = new List<Planet>();
    public List<Planet> enemy3Planet = new List<Planet>();

    private void Start()
    {
        planetLists = new List<List<Planet>> { playerPlanet, enemy1Planet, enemy2Planet, enemy3Planet };
        ClearFillArea();
        InvokeRepeating("CreateListPlanet", 0f, 0.25f);
        InvokeRepeating("UpdateFill", 0f, 0.25f);
    }

    private void SetColor()
    {
        if (playerPlanet.Count > 0)
        {
            Color newColor = playerPlanet[0].GetComponent<SpriteRenderer>().color;
            playerFill.color = newColor;
        }

        if (enemy1Planet.Count > 0)
        {
            Color newColor = enemy1Planet[0].GetComponent<SpriteRenderer>().color;
            enemy1Fill.color = newColor;
        }

        if (enemy2Planet.Count > 0)
        {
            Color newColor = enemy2Planet[0].GetComponent<SpriteRenderer>().color;
            enemy2Fill.color = newColor;
        }

        if (enemy3Planet.Count > 0)
        {
            Color newColor = enemy3Planet[0].GetComponent<SpriteRenderer>().color;
            enemy3Fill.color = newColor;
        }
    }


    private void ClearFillArea()
    {
        playerFill.fillAmount = 0f;
        enemy1Fill.fillAmount = 0f;
        enemy2Fill.fillAmount = 0f;
        StartCoroutine(SmoothTransition(1f, enemy3Fill, 1.4f));
    }

    private void CreateListPlanet()
    {
        RefreshListPlanet(playerPlanet, "PlayerPlanet");
        RefreshListPlanet(enemy1Planet, "Enemy1");
        RefreshListPlanet(enemy2Planet, "Enemy2");
        RefreshListPlanet(enemy3Planet, "Enemy3");
    }

    private void RefreshListPlanet(List<Planet> listPlanet, string tagPlanet)
    {
        listPlanet.Clear();

        GameObject[] playerPlanetObjects = GameObject.FindGameObjectsWithTag(tagPlanet);

        foreach (GameObject playerPlanetObject in playerPlanetObjects)
        {
            Planet planetComponent = playerPlanetObject.GetComponent<Planet>();
            if (planetComponent != null && !playerPlanet.Contains(planetComponent))
            {
                listPlanet.Add(planetComponent);
            }
        }

        SetColor();
    }

    private void SumUnits()
    {
        int tempUnitsCount = 0;
        for (int i = 0; i < planetLists.Count; i++)
        {
            foreach (Planet planet in planetLists[i])
            {
                tempUnitsCount += planet.currentUnitCount;
            }
        }
        int flyedUnits = FindUnits();
        unitsCount = tempUnitsCount + flyedUnits;
    }

    private int FindUnits()
    {
        int count = 0;

        Unit[] unitMovements = FindObjectsOfType<Unit>();
        foreach (Unit unitMovementComponent in unitMovements)
        {
            count++;
        }

        return count;
    }

    private int FindUnits(string tagUnit)
    {
        int count = 0;
        GameObject[] objectUnits = GameObject.FindGameObjectsWithTag(tagUnit);

        foreach (GameObject objectUnit in objectUnits)
        {
            Unit unitComponent = objectUnit.GetComponent<Unit>();

            if (unitComponent != null)
            {
                count++;
            }
        }
        return count;
    }

    private void UpdateFill()
    {
        SumUnits();
        int playerCount = 0;
        foreach (Planet planet in playerPlanet)
        {
            playerCount += planet.currentUnitCount;
        }
        playerCount += FindUnits("PlayerPlanet");

        int enemy1Count = playerCount;
        foreach (Planet planet in enemy1Planet)
        {
            enemy1Count += planet.currentUnitCount;
        }
        enemy1Count += FindUnits("Enemy1");

        int enemy2Count = enemy1Count;
        foreach (Planet planet in enemy2Planet)
        {
            enemy2Count += planet.currentUnitCount;
        }
        enemy2Count += FindUnits("Enemy2");

        if (planetLists.Count > 3 && (planetLists[3].Count == 0 || !planetLists.Contains(enemy3Planet)))
        {
            if (planetLists.Contains(enemy3Planet))
            {
                planetLists.Remove(enemy3Planet);
            }
            StartCoroutine(SmoothTransition(1f, enemy2Fill, 1.4f));
        }
        else
        {
            float tempEnemy2Count = (float)enemy2Count / (float)unitsCount;
            StartCoroutine(SmoothTransition(tempEnemy2Count, enemy2Fill, 1.4f));
        }

        if (planetLists.Count > 2 && (planetLists[2].Count == 0 || !planetLists.Contains(enemy2Planet)))
        {
            if (planetLists.Contains(enemy2Planet) || planetLists.Contains(enemy3Planet))
            {
                planetLists.Remove(enemy3Planet);
                planetLists.Remove(enemy2Planet);
            }
            StartCoroutine(SmoothTransition(1f, enemy1Fill, 1.4f));
        }
        else
        {
            float tempEnemy1Count = (float)enemy1Count / (float)unitsCount;
            StartCoroutine(SmoothTransition(tempEnemy1Count, enemy1Fill, 1.4f));
        }

        float tempPlayerCount = (float)playerCount / (float)unitsCount;
        StartCoroutine(SmoothTransition(tempPlayerCount, playerFill, 1.4f));
    }

    IEnumerator SmoothTransition(float newFillCount, Image imageFill, float duration)
    {
        float oldFillCount = imageFill.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            imageFill.fillAmount = Mathf.Lerp(oldFillCount, newFillCount, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageFill.fillAmount = newFillCount;
    }

}
