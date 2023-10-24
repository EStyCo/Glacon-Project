using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using Zenject;

public abstract class Spawner : MonoBehaviour
{
    [Inject] protected DiContainer diContainer;
    [Inject] protected GameModeManager gameModeManager;
    [Inject(Id = "Planet")] protected GameObject planetPrefab;
    [Inject(Id = "PlayerUnit")] protected GameObject playerUnitPrefab;
    [Inject(Id = "PlayerCruiser")] protected GameObject playerCruiserPrefab;

    protected List<Vector2> spawnPoints = new List<Vector2>();
    protected Color neutralColor = new Color(0.8207547f, 0.8207547f, 0.7162246f);
    protected Transform t;

    [SerializeField] protected Canvas canvasParent;
    [SerializeField] protected GameObject leftTopCanvas;
    [SerializeField] protected GameObject rightBottomCanvas;
    [SerializeField] protected GameObject leftBottomPM;
    [SerializeField] protected GameObject rightTopSM;

    private float minDistance = 1.1f;
    private float minDistanceToEnemy = 5f;

    private void Awake()
    {

    }
    void Start()
    {
        t = canvasParent.transform;
        GenerateObjects();
    }

    protected abstract void GenerateObjects();

    protected Vector2 GetRandomSpawnPoint(bool isEnemy)
    {
        bool isSucces = true;
        int index = 0;
        Vector2 defaultVector = new Vector2(0f, 0f);

        Vector2 randomPoint;
        Vector2 newVectorX = canvasParent.transform.InverseTransformPoint(rightBottomCanvas.transform.position) * canvasParent.transform.lossyScale.x;
        Vector2 newVectorY = canvasParent.transform.InverseTransformPoint(leftTopCanvas.transform.position) * canvasParent.transform.lossyScale.x;

        do
        {
            float randomX = UnityEngine.Random.Range(newVectorX.x, newVectorY.x);
            float randomY = UnityEngine.Random.Range(newVectorY.y, newVectorX.y);
            randomPoint = new Vector2(randomX, randomY);

            index++;
            if (index > 250)
            {
                isSucces = false;
                SceneManager.LoadScene(2);
                break;
            }
        }
        while (!IsValidSpawnPoint(randomPoint, isEnemy));

        if (isSucces)
            return randomPoint;
        else
            return defaultVector;
    }

    protected void SpawnNeutralPlanets()
    {
        int numberOfPlanets = gameModeManager.planetCount;

        for (int i = 0; i < numberOfPlanets; i++)
        {
            int randomSize = UnityEngine.Random.Range(1, 4);

            Vector2 neutralSpawnPoint = GetRandomSpawnPoint(false);
            GameObject neutralPlanet = diContainer.InstantiatePrefab(planetPrefab, neutralSpawnPoint, Quaternion.identity, t);

            Planet planetScript = neutralPlanet.GetComponent<Planet>();
            planetScript.selectedSize = (Planet.Size)randomSize;
            planetScript.framePlanet.GetComponent<SpriteResolver>().SetCategoryAndLabel("Frame", "Frame" + randomSize.ToString());

            neutralPlanet.GetComponent<SpriteRenderer>().color = neutralColor;
            neutralPlanet.tag = "NeutralPlanet";
            spawnPoints.Add(neutralSpawnPoint);
        }
    }

    protected bool IsValidSpawnPoint(Vector2 point, bool isEnemy)
    {
        if (leftBottomPM != null)
        {
            Vector2 PM = leftBottomPM.transform.position;

            if (point.x >= PM.x && point.x <= PM.x + 10f &&
                point.y >= PM.y && point.y <= PM.y + 10f)
            {
                return false; // Точка находится внутри запрещенной зоны, Меню Паузы
            }
        }

        if (rightTopSM != null)
        {
            Vector2 selector = rightTopSM.transform.position;

            if (point.x <= selector.x && point.x >= selector.x - 10f &&
                point.y <= selector.y && point.y >= selector.y - 10f)
            {
                return false; // Точка находится внутри запрещенной зоны, Селектора
            }
        }

        float distance;

        if (isEnemy)
            distance = minDistanceToEnemy;
        else
            distance = minDistance;

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            if (Vector2.Distance(point, spawnPoint) < distance)
            {
                return false;
            }
        }

        return true;
    }

}
