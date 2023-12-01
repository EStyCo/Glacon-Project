using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorSelectManager : MonoBehaviour
{
    public static TutorSelectManager Instance { get; private set; }

    private TutorPlanet targetPlanet;
    public LayerMask planetLayer;

    public bool canSendUnits = false;
    public bool isPaused = true;
    public bool isSelecting = false;
    private bool isDrawing = false;

    private float delayDraw = 0.125f;

    private UnityEngine.Vector2 selectionStartPoint;

    private List<TutorPlanet> selectedPlanets = new List<TutorPlanet>();


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused && !isDrawing && !EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(DelayDrawing());
            selectionStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } // Начало рисование прямоугольника с задержкой.

        if (Input.GetMouseButtonUp(0) && !isPaused)
        {
            isDrawing = false;
            isSelecting = false;

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        } // Отпускание лкм, конец рисования прямоугольника.

        if (isSelecting)
        {
            UnityEngine.Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawSelectionRectangle(selectionStartPoint, currentMousePos);

            foreach (TutorPlanet planet in GetAllPlanets())
            {
                if (IsInsideSelectionRect(planet.transform.position, selectionStartPoint, currentMousePos))
                {
                    if (!selectedPlanets.Contains(planet)) TogleListPlanet(planet);
                }
                else if (selectedPlanets.Contains(planet)) TogleListPlanet(planet);
            } //Проверка планеты, попадает ли она в прямоугольник.
        } // Рисование прямоугольника.

        if (Input.GetMouseButtonDown(0) && !isPaused && !isSelecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, planetLayer);

            if (hit.collider != null)
            {
                TutorPlanet planet = hit.collider.GetComponent<TutorPlanet>();
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
                }
            }
            else ClearSelectionListPlanet();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, planetLayer);

            if (hit.collider != null)
            {
                TutorPlanet planet = hit.collider.GetComponent<TutorPlanet>();
                if (planet != null)
                {
                    if (planet.tag == "PlayerPlanet" && selectedPlanets != null && targetPlanet == null)
                    {
                        targetPlanet = planet;
                        SendUnits();
                        targetPlanet = null;
                    }
                    else if ((planet.tag == "NeutralPlanet" || planet.tag == "EnemyPlanet") && selectedPlanets != null && targetPlanet == null)
                    {
                        targetPlanet = planet;
                        SendUnits();
                        targetPlanet = null;
                    }
                }
            }
        } // ПКМ, отправка юнитов.
    }
    private IEnumerator DelayDrawing()
    {
        isDrawing = true;
        yield return new WaitForSeconds(delayDraw);
        if (isDrawing) isSelecting = true;
    } // Задержка перед рисованием.

    private void DrawSelectionRectangle(UnityEngine.Vector2 startPoint, UnityEngine.Vector2 endPoint)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 5;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, new UnityEngine.Vector3(startPoint.x, endPoint.y, 0));
        lineRenderer.SetPosition(2, endPoint);
        lineRenderer.SetPosition(3, new UnityEngine.Vector3(endPoint.x, startPoint.y, 0));
        lineRenderer.SetPosition(4, startPoint);
    } // Метод рисования прямоугольника.
    private bool IsInsideSelectionRect(UnityEngine.Vector2 point, UnityEngine.Vector2 rectStart, UnityEngine.Vector2 rectEnd)
    {
        float minX = Mathf.Min(rectStart.x, rectEnd.x);
        float maxX = Mathf.Max(rectStart.x, rectEnd.x);
        float minY = Mathf.Min(rectStart.y, rectEnd.y);
        float maxY = Mathf.Max(rectStart.y, rectEnd.y);

        return (point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY);
    } // Метод проверки нахождения планет в прямоугольнике.
    private TutorPlanet[] GetAllPlanets()
    {
        return GameObject.FindGameObjectsWithTag("PlayerPlanet")
                         .Select(go => go.GetComponent<TutorPlanet>())
                         .Where(planet => planet != null)
                         .ToArray();
    }
    private void TogleListPlanet(TutorPlanet planet)
    {
        if (selectedPlanets.Contains(planet))
        {
            selectedPlanets.Remove(planet);
            planet.DeselectPlanet();
        }
        else
        {
            if (Tutorial2.Instance.index == 1)
            { 
                Tutorial2.Instance.NextDialog();
                Tutorial2.Instance.RisePlanet();
            }
            selectedPlanets.Add(planet);
            planet.SelectPlanet();
        }
    } // Метод выделения планет и добавления в Лист.  

    private void SendUnits()
    {
        if (selectedPlanets.Count > 0 && canSendUnits)
            foreach (TutorPlanet planet in selectedPlanets)
                if (planet != targetPlanet)
                    planet.SendUnitsToPlanet(targetPlanet);

        ClearSelectionListPlanet();
    } // Отправка юнитов.
    private void ClearSelectionListPlanet()
    {
        foreach (TutorPlanet planet in selectedPlanets)
            planet.DeselectPlanet();

        selectedPlanets.Clear();
    } // Очистка списка выделенных планет.
}