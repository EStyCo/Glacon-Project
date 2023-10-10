using Game;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.U2D.Animation;

public class Planet : MonoBehaviour
{
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3
    } // Размеры планет

    private GameObject canvasParent;
    private GameObject unitsParent;
    public GameObject unitPrefab;
    public SpriteRenderer planetRenderer;
    public SpriteRenderer framePlanet;
    private SpriteResolver spriteResolver;
    private CircleCollider2D circleCollider;
    public TextMeshProUGUI unitCountText;

    public Color originalColor;
    public Size selectedSize = Size.medium;

    private const float spawnDistance = 0.25f;
    private const float unitSpacing = 0.25f;

    private bool isIncreaseCoroutineRunning = false;
    private string tagPlanet;

    private int maxUnitCurrent = 75;
    public int currentUnitCount;
    public float timerFromSize = 1f;

    public int tempCoroutin = 0;

    private void Update()
    {
        unitCountText.text = currentUnitCount.ToString();
    }
    private void Start()
    {
        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        if (canvasParent == null) Debug.LogWarning("Не найден родитель канвас для планет!");
        unitsParent = GameObject.FindGameObjectWithTag("UnitsParent");
        if (unitsParent == null) Debug.LogWarning("Не найден родитель для юнитов!");
        tagPlanet = gameObject.tag.ToString();
        originalColor = GameManager.Instance.colorPlanet;
        planetRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
        circleCollider = GetComponent<CircleCollider2D>();

        StartUnitCount();
        CheckSize((int)selectedSize);
    }
    private void StartUnitCount()
    {
        if (gameObject.tag != "NeutralPlanet")
        {
            currentUnitCount = 65;
            if (gameObject.tag == "PlayerPlanet")
            {
                currentUnitCount = 40;
            }
            CheckMakeUnits();
        }
        else currentUnitCount = Random.Range(15, 41);
    } // Генерация юнитов на планетах игрока и противника.
    public void SendUnitsToPlanet(Planet targetPlanet)
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
    private System.Collections.IEnumerator SpawnUnitsWithDelay(Planet targetPlanet, int unitsToSend)
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

    private void SendUnits(Planet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position) * transform.localScale.normalized.y;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);

        SpawnUnitAtPosition(spawnPosition, targetPlanet);

        currentUnitCount--;
    }

    private Vector3 CalculateSpawnPosition(Vector3 directionToTarget)
    {
        return transform.position + directionToTarget.normalized * spawnDistance;
    }
    private void SpawnUnitAtPosition(Vector3 spawnPosition, Planet targetPlanet)
    {

        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(unitPrefab, spawnPosition, spawnRotation);
        unitInstance.GetComponent<UnitMovement>().unitPrefab = unitPrefab;
        unitInstance.transform.SetParent(unitsParent.transform, false);
        UnitMovement unitMovement = unitInstance.GetComponent<UnitMovement>();

        if (unitMovement != null)
        {
            unitInstance.tag = gameObject.tag;
            unitMovement.tagUnit = unitInstance.tag.ToString();
            unitInstance.GetComponent<SpriteRenderer>().color = planetRenderer.color;

            unitMovement.target = targetPlanet.transform;
            unitMovement.SetTarget(targetPlanet);
        }
    }
/*    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag != tagPlanet && collision.gameObject.IsDestroyed())
        {
            if (currentUnitCount >= 0) currentUnitCount--;

        }
        else if (collision.gameObject.tag == tagPlanet && collision.gameObject.IsDestroyed())
        {
            if (currentUnitCount <= maxUnitCurrent)
            { 
                currentUnitCount++;
            }
        }
    }*/
    public void IncreaseUnits()
    {
        if (currentUnitCount < maxUnitCurrent) 
            currentUnitCount++;
    } // Увеличение юнитов
    public void DecreaseUnits()
    {
        if (currentUnitCount > 0) 
            currentUnitCount--;
    } // Уменьшение юнитов и смена тэга планеты.
    public void CheckMakeUnits()
    {
        if (!isIncreaseCoroutineRunning)
        {

            tempCoroutin++; // считаем сколько раз запусталсь корутина.

            StartCoroutine(IncreaseUnitsOverTime());
            isIncreaseCoroutineRunning = true;
        }
    }
    private System.Collections.IEnumerator IncreaseUnitsOverTime()
    {

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
        framePlanet.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
    }

    public void DeselectPlanet()
    {
        framePlanet.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0f / 255f);
    }

}
