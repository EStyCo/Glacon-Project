using Game;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Planet : MonoBehaviour
{
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3
    } // Размеры планет

    public GameObject unitPrefab;
    public GameObject cruiserPrefab;
    public SpriteRenderer planetRenderer;
    public TextMeshProUGUI unitCountText;
    public SpriteRenderer framePlanet;
    public GameObject unitsParent;
    private GameObject canvasParent;
    private SpriteResolver spriteResolver;
    private CircleCollider2D circleCollider;
    private MakeShip makeShip;

    public Color color;
    public Size selectedSize = Size.medium;

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
        unitsParent = GameObject.FindGameObjectWithTag("UnitsParent");
        tagPlanet = gameObject.tag.ToString();
        makeShip = gameObject.GetComponent<MakeShip>();
        planetRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
        circleCollider = GetComponent<CircleCollider2D>();

        StartUnitCount();
        CheckSize((int)selectedSize);
        SetColor();
    }

    private void SetColor()
    {
        if (gameObject.CompareTag("PlayerPlanet"))
        {
            color = GameManager.Instance.colorPlanet;
        }
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
    public void SendShipsToPlanet(Planet targetPlanet)
    {
        if (gameObject.CompareTag("PlayerPlanet") && Selector.Instance != null)
        {
            float percentToSend = (float)Selector.Instance.selectedPercent / 100f;
            int unitsToSend = Mathf.CeilToInt(currentUnitCount * percentToSend);

            if (unitsToSend > 0)
            {
                StartCoroutine(makeShip.SpawnUnitsWithDelay(targetPlanet, unitsToSend));
            }
        }
        else
        {
            int unitsToSend = currentUnitCount / 2;

            if (unitsToSend > 0)
            {
                StartCoroutine(makeShip.SpawnUnitsWithDelay(targetPlanet, unitsToSend));
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
