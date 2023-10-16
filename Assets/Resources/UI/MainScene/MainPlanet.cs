using Game;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class MainPlanet : MonoBehaviour
{
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3
    }

    [SerializeField] private GameObject canvasParent;
    [SerializeField] private GameObject unitsParent;
    [SerializeField] private GameObject[] listUnits;

    private GameObject unitPrefab;
    private SpriteRenderer planetRenderer;
    private SpriteResolver spriteResolver;
    private CircleCollider2D circleCollider;

    private Color originalColor;
    public Size selectedSize = Size.medium;

    private const float spawnDistance = 0.25f;
    private const float unitSpacing = 0.25f;

    private void SetUnits()
    { 
        int randomIndex = Random.Range(0, listUnits.Length);

        unitPrefab = listUnits[randomIndex];
    }

    private void Start()
    {
        SetUnits();

        canvasParent = GameObject.FindGameObjectWithTag("CanvasParent");
        if (canvasParent == null) Debug.LogWarning("Не найден родитель канвас для планет!");
        unitsParent = GameObject.FindGameObjectWithTag("UnitsParent");
        if (unitsParent == null) Debug.LogWarning("Не найден родитель для юнитов!");
        originalColor = GameManager.Instance.colorPlanet;
        planetRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
        circleCollider = GetComponent<CircleCollider2D>();

        CheckSize((int)selectedSize);
    }

    public void SendUnitsToPlanet(MainPlanet targetPlanet)
    {
        int unitsToSend = Random.Range(5, 15);

        if (unitsToSend > 0)
        {
            StartCoroutine(SpawnUnitsWithDelay(targetPlanet, unitsToSend));
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

    private System.Collections.IEnumerator SpawnUnitsWithDelay(MainPlanet targetPlanet, int unitsToSend)
    {
        for (int i = 0; i < unitsToSend; i++)
        {
            SendUnits(targetPlanet);
            yield return new WaitForSeconds(0.08f);
        }
    }

    private void SendUnits(MainPlanet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - transform.position) * transform.localScale.normalized.y;
        Vector3 spawnPosition = CalculateSpawnPosition(directionToTarget);

        SpawnUnitAtPosition(spawnPosition, targetPlanet);
    }

    private Vector3 CalculateSpawnPosition(Vector3 directionToTarget)
    {
        return transform.position + directionToTarget.normalized * spawnDistance;
    }

    private void SpawnUnitAtPosition(Vector3 spawnPosition, MainPlanet targetPlanet)
    {
        Vector3 directionToTarget = (targetPlanet.transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        GameObject unitInstance = Instantiate(unitPrefab, spawnPosition, spawnRotation);
        unitInstance.GetComponent<MainUnit>().unitPrefab = unitPrefab;
        unitInstance.transform.SetParent(unitsParent.transform, false);
        MainUnit unitMovement = unitInstance.GetComponent<MainUnit>();

        if (unitMovement != null)
        {
            unitInstance.tag = gameObject.tag;
            unitMovement.tagUnit = unitInstance.tag.ToString();
            unitInstance.GetComponent<SpriteRenderer>().color = planetRenderer.color;

            unitMovement.target = targetPlanet.transform;
            unitMovement.SetTarget(targetPlanet);
        }
    }
}
