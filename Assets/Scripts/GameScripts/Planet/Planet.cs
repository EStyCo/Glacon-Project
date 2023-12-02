using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Zenject;

public class Planet : MonoBehaviour
{
    public enum Size
    {
        small = 1,
        medium = 2,
        large = 3
    }

    #region INJECTION

    [Inject] private GameModeManager gameModeManager;
    [Inject] private ShipConstructor shipConstructor;
    [Inject] private GameManager gameManager;
    [Inject] private Growth growth;
    [Inject] private Draft draft; 

    #endregion

    public float armor;

    [Header("Addictions")]
    public GameObject turret;
    public TextMeshProUGUI unitCountText;
    public SpriteRenderer framePlanet;
    public GameObject unitsParent;

    [Header("Units")]
    public float currentUnitCount;
    public float maxUnitCurrent = 60f;
    public float spawnRate = 1f;
    public float growthLevel = 0f;

    [HideInInspector] public Color color;
    [HideInInspector] public GameObject unitPrefab;
    [HideInInspector] public GameObject cruiserPrefab;
    [HideInInspector] public SpriteRenderer planetRenderer;
    [HideInInspector] public Size selectedSize = Size.medium;

    private SpriteResolver spriteResolver;
    private CircleCollider2D circleCollider;
    private MakeShip makeShip;
    private bool isCoroutineRunning = false;

    #region MONO

    private void Update()
    {
        unitCountText.text = currentUnitCount.ToString("0");
    }

    private void Start()
    {
        unitsParent = GameObject.FindGameObjectWithTag("UnitsParent");
        makeShip = gameObject.GetComponent<MakeShip>();
        planetRenderer = GetComponent<SpriteRenderer>();
        spriteResolver = GetComponent<SpriteResolver>();
        circleCollider = GetComponent<CircleCollider2D>();

        CheckSize((int)selectedSize);
        StartUnitCount();
        SetColor();
        growth.GetPlanet(gameObject);
        draft.GetPlanet(gameObject);
    }

    #endregion

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
            int unitsToSend = (int)currentUnitCount / 2;

            if (unitsToSend > 0)
            {
                StartCoroutine(makeShip.SpawnUnitsWithDelay(targetPlanet, unitsToSend));
            }
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

    public void CheckProgress()
    {
        GetComponent<ProgressPlanet>().CheckProgress();
    }

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

        ChangeSpawnRate();
    }

    public void IncreaseUnits()
    {
        if (currentUnitCount < maxUnitCurrent)
            currentUnitCount++;
    }

    public void DecreaseUnits()
    {
        if (currentUnitCount > 0)
            currentUnitCount--;
    }

    public void CheckMakeUnits()
    {
        CheckProgress();

        if (!isCoroutineRunning)
        {
            StartCoroutine(IncreaseUnitsOverTime());
            isCoroutineRunning = true;
        }
    }

    private void SetColor()
    {
        if (gameObject.CompareTag("PlayerPlanet") && gameModeManager.currentGameMode == GameModeManager.GameMode.Tutorial)
        {
            color = new Color(55 / 255f, 189 / 255f, 128 / 255f, 255 / 255f);
            planetRenderer.color = color;
        }
        else if (gameObject.CompareTag("PlayerPlanet"))
        {
            color = gameManager.colorPlayer;
            planetRenderer.color = color;
        }
    }

    private void StartUnitCount()
    {
        if (gameObject.tag != "NeutralPlanet")
        {
            currentUnitCount = shipConstructor.startUnitsEnemy;

            if (gameObject.tag == "PlayerPlanet")
                currentUnitCount = Random.Range(shipConstructor.minUnitsPlayer, shipConstructor.maxUnitsPlayer);

            CheckMakeUnits();
        }
        else currentUnitCount = Random.Range(shipConstructor.minNeutralUnits, shipConstructor.maxNeutralUnits);
    }

    private System.Collections.IEnumerator IncreaseUnitsOverTime()
    {
        while (true)
        {
            if (currentUnitCount < maxUnitCurrent)
                currentUnitCount += (spawnRate + growthLevel);
            else currentUnitCount = maxUnitCurrent;

            yield return new WaitForSeconds(1f);
        }
    }

    private void ChangeSpawnRate()
    {
        switch (selectedSize)
        {
            case Size.small:
                spawnRate = 0.65f;
                break;

            case Size.medium:
                spawnRate = 0.9f;
                break;

            case Size.large:
                spawnRate = 1.15f;
                break;

            default:
                break;
        }
    }
}
