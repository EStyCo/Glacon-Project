using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class TutorPlanet : MonoBehaviour
{
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3
    } // Размеры планет

    public GameObject unitPrefab;
    private SpriteRenderer planetRenderer;
    private SpriteResolver spriteResolver;
    private CircleCollider2D circleCollider;
    public TextMeshProUGUI unitCountText;

    private Color originalColor;
    public Size selectedSize = Size.medium;

    private const float spawnDistance = 0.55f;
    private const float unitSpacing = 0.25f;

    private bool canIncreaseUnits = false;
    private bool isTryDesant = false;

    private int maxUnitCurrent = 50;
    public int currentUnitCount;

    private void Update()
    {
        CheckTryDesant();
        unitCountText.text = currentUnitCount.ToString();
    }
    private void Start()
    {
        planetRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
        circleCollider = GetComponent<CircleCollider2D>();
        originalColor = planetRenderer.color;

        StartUnitCount();
        //CheckTagPlanet();
        CheckSize((int)selectedSize);
    }
    private void CheckTryDesant()
    {
        if (gameObject.CompareTag("EnemyPlanet") && currentUnitCount < 15 && !isTryDesant)
        { 
            isTryDesant = true;
            Tutorial.Instance.NextDialog();
        }
    }
    private void StartUnitCount()
    {
        if (gameObject.tag == "PlayerPlanet")
        {
            currentUnitCount = 15;
            StartCoroutine(IncreaseUnitsOverTime());
        }
        else currentUnitCount = 30;
    } // Генерация юнитов на планетах игрока и противника.
    public void SendUnitsToPlanet(TutorPlanet targetPlanet)
    {
        if (gameObject.CompareTag("PlayerPlanet") && Selector.Instance != null)
        {
            float percentToSend = (float)Selector.Instance.selectedPercent / 100f;
            int unitsToSend = Mathf.CeilToInt(currentUnitCount * percentToSend);

            if (unitsToSend > 0)
            {
                StartCoroutine(SpawnUnitsWithDelay(targetPlanet, unitsToSend));
            }
        }
        else
        {
            int unitsToSend = currentUnitCount / 2;

            if (unitsToSend > 0)
            {
                StartCoroutine(SpawnUnitsWithDelay(targetPlanet, unitsToSend));
            }
        }

    } // Отправка юнитов с планеты на планету.
    public void CheckSize(int value)
    {
        Size selectedSize = (Size)value;

        if (selectedSize == Size.small)
        {
            spriteResolver.SetCategoryAndLabel("Small", "Small" + Random.Range(1, 3).ToString());
            circleCollider.radius = 0.5f;
        }
        if (selectedSize == Size.medium)
        {
            spriteResolver.SetCategoryAndLabel("Medium", "Medium" + Random.Range(1, 3).ToString());
            circleCollider.radius = 0.6f;
        }
        if (selectedSize == Size.large)
        {
            spriteResolver.SetCategoryAndLabel("Large", "Large" + Random.Range(1, 3).ToString());
            circleCollider.radius = 0.7f;
        }
    } // Проверка планеты на размер для старта корутины генерации юнитов.
    private System.Collections.IEnumerator SpawnUnitsWithDelay(TutorPlanet targetPlanet, int unitsToSend)
    {
        for (int i = 0; i < unitsToSend - 1; i++)
        {
            if (currentUnitCount > 1)
            {
                SendUnits(targetPlanet);
                yield return new WaitForSeconds(0.08f);
            }
        }
    }

    private void SendUnits(TutorPlanet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position).normalized;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);

        SpawnUnitAtPosition(spawnPosition, targetPlanet);

        currentUnitCount--;
    }

    private Vector3 CalculateSpawnPosition(Vector3 directionToTarget)
    {
        return transform.position + directionToTarget * spawnDistance;
    }
    private void SpawnUnitAtPosition(Vector3 spawnPosition, TutorPlanet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(unitPrefab, spawnPosition, spawnRotation);
        TutorUnitMovement unitMovement = unitInstance.GetComponent<TutorUnitMovement>();

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
        if (currentUnitCount < maxUnitCurrent) currentUnitCount++;
    } // Увеличение юнитов
    public void DecreaseUnits()
    {
        if (currentUnitCount > 0) currentUnitCount--;
        if (currentUnitCount < 1 && gameObject.tag != "PlayerPlanet")
        {
            Tutorial.Instance.Invoke("NextDialog", 2);
            gameObject.tag = "PlayerPlanet";
            canIncreaseUnits = true;
            CheckTagPlanet();
            CheckMakeUnits();
        }
    } // Уменьшение юнитов и смена тэга планеты.
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
            canIncreaseUnits = true;
            CheckTagPlanet();
            CheckMakeUnits();
        }
    }
    private void CheckTagPlanet()
    {
        if (gameObject.tag == "PlayerPlanet")
        {
            planetRenderer.color = new Color(59f / 255f, 115f / 255f, 45f / 255f);
            originalColor = planetRenderer.color;
        }
/*        if (gameObject.tag == "EnemyPlanet")
        {
            planetRenderer.color = new Color(217f / 255f, 77f / 255f, 77f / 255f, 0f / 255f);
            originalColor = planetRenderer.color;
        }*/
    }
    public void CheckMakeUnits()
    {
        if (canIncreaseUnits)
        {
            StartCoroutine(IncreaseUnitsOverTime());
        }
    }
    private System.Collections.IEnumerator IncreaseUnitsOverTime()
    {
        float timerFromSize = 0.1f;

        if (selectedSize == Size.small) timerFromSize = 1.05f;
        else if (selectedSize == Size.medium) timerFromSize = 0.75f;
        else if (selectedSize == Size.large) timerFromSize = 0.55f;

        while (true)
        {
            IncreaseUnits();


            yield return new WaitForSeconds(timerFromSize);
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
