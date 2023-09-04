using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject unitPrefab;
    private SpriteRenderer planetRenderer;
    public TextMeshProUGUI unitCountText;

    private Color originalColor;

    private const int startingUnitCount = 50;
    private const float spawnDistance = 0.55f;
    private const float unitSpacing = 0.25f;

    private bool isIncreaseCoroutineRunning = false;

    private int maxUnitCurrent = 200;
    public int currentUnitCount;

    private void Update()
    {
        unitCountText.text = currentUnitCount.ToString();
    }

    private void Start()
    {
        originalColor = GameManager.Instance.colorUnits;
        planetRenderer = GetComponent<SpriteRenderer>();
        StartUnitCount();
        CheckTagPlanet();
    }
    private void StartUnitCount()
    {
        if (gameObject.tag == "PlayerPlanet" || gameObject.tag == "EnemyPlanet")
        { 
            currentUnitCount = 50;
            StartCoroutine(IncreaseUnitsOverTime());
        }
        else currentUnitCount = Random.Range(15, 51);
    }
    public void SendUnitsToPlanet(Planet targetPlanet)
    {
        int unitsToSend = currentUnitCount / 2;

        if (unitsToSend > 0)
        {
            StartCoroutine(SpawnUnitsWithDelay(targetPlanet, unitsToSend));
        }
    }

    private System.Collections.IEnumerator SpawnUnitsWithDelay(Planet targetPlanet, int unitsToSend)
    {
        for (int i = 0; i < unitsToSend; i++)
        {
            if (currentUnitCount > 1)
            { 
                SendUnits(targetPlanet);
                yield return new WaitForSeconds(0.08f); 
            }
        }
    }

    private void SendUnits(Planet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position).normalized;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);
        Quaternion spawnRotation = CalculateSpawnRotation(directionToTarget);

        SpawnUnitAtPosition(spawnPosition, targetPlanet);

        currentUnitCount--;
    }

    private Vector3 CalculateSpawnPosition(Vector3 directionToTarget)
    {
        return transform.position + directionToTarget * spawnDistance;
    }

    private Quaternion CalculateSpawnRotation(Vector3 directionToTarget)
    {
        return Quaternion.LookRotation(Vector3.forward, directionToTarget);
    }

    private void SpawnUnitAtPosition(Vector3 spawnPosition, Planet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(unitPrefab, spawnPosition, spawnRotation);
        UnitMovement unitMovement = unitInstance.GetComponent<UnitMovement>();

        if (unitMovement != null)
        {
            if (gameObject.tag == "PlayerPlanet")
            {
                unitInstance.tag = "PlayerUnit";
                unitInstance.GetComponent<SpriteRenderer>().color = originalColor; 
            }
            else if (gameObject.tag == "EnemyPlanet")
            {
                unitInstance.tag = "EnemyUnit";
                unitInstance.GetComponent<SpriteRenderer>().color = new Color(217f / 255f, 77f / 255f, 77f / 255f);
            }
            unitMovement.SetTarget(targetPlanet.transform);
            unitMovement.SetTargetPlanet(targetPlanet);
        }
    }
    public void IncreaseUnits()
    {
        if (currentUnitCount < maxUnitCurrent)
        {
            currentUnitCount++;
        } 
    }
    public void DecreaseUnits()
    {
        if (currentUnitCount > 0) currentUnitCount--;
        if (currentUnitCount < 1 && gameObject.tag != "PlayerPlanet")
        {
            gameObject.tag = "PlayerPlanet";
            CheckTagPlanet();
            CheckMakeUnits();
        }
    }
    public void IncreaseUnitsFromEnemy()
    {
        if (currentUnitCount < maxUnitCurrent) currentUnitCount++;
    }
    public void DecreaseUnitsFromEnemy()
    {
        if (currentUnitCount > 0) currentUnitCount--;
        if (currentUnitCount < 1 && gameObject.tag != "EnemyPlanet")
        { 
            gameObject.tag = "EnemyPlanet";
            CheckTagPlanet();
            CheckMakeUnits();
        }
    }
    private void CheckTagPlanet()
    {
        if (gameObject.tag == "PlayerPlanet")
        {
            planetRenderer.color = GameManager.Instance.colorUnits;
            originalColor = planetRenderer.color;
        }
        if (gameObject.tag == "EnemyPlanet")
        {
            planetRenderer.color = new Color(217f / 255f, 77f / 255f, 77f / 255f);
            originalColor = planetRenderer.color;
        }
    }
    private void CheckMakeUnits()
    {
        if (!isIncreaseCoroutineRunning)
        { 
            StartCoroutine(IncreaseUnitsOverTime());
            isIncreaseCoroutineRunning = true;
        }
    }
    private System.Collections.IEnumerator IncreaseUnitsOverTime()
    {
        while (true)
        {
            IncreaseUnits();

            if (gameObject.CompareTag("PlayerPlanet")) BalancePower.Instance.ChangePlayerPower(true);
            if (gameObject.CompareTag("EnemyPlanet")) BalancePower.Instance.ChangeEnemyPower(true);

            yield return new WaitForSeconds(1f); 
        }
    }
    public void SelectPlanet()

    {
        planetRenderer.color = Color.Lerp(planetRenderer.color, Color.black, 0.4f); 
    }

    public void DeselectPlanet()
    {
        planetRenderer.color = originalColor;
    }

}
