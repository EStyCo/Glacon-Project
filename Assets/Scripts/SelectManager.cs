using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance { get; private set; }

    private Planet targetPlanet;
    public LayerMask planetLayer;

    public bool isPaused = false;
    public bool isSelecting = false;
    private bool isDrawing = false;

    private float delayDraw = 0.1f;

    private Vector2 selectionStartPoint;

    private List<Planet> selectedPlanets = new List<Planet>();


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused && !isDrawing)
        {
            StartCoroutine(DelayDrawing());
            selectionStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButtonUp(0) && !isPaused)
        {
            isDrawing = false;
            isSelecting = false;
            SelectPlanetsInRect();

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }

        if (isSelecting)
        {
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawSelectionRectangle(selectionStartPoint, currentMousePos);
        }

        if (Input.GetMouseButtonDown(0) && !isPaused && !isSelecting)
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
    private IEnumerator DelayDrawing()
    {
        isDrawing = true;
        yield return new WaitForSeconds(delayDraw); // Задержка в 0.3 секунды
        if (isDrawing) isSelecting = true;
    }
    private void SelectPlanetsInRect()
    {
        Planet[] playerPlanets = GameObject.FindGameObjectsWithTag("PlayerPlanet")
                                     .Select(go => go.GetComponent<Planet>())
                                     .Where(planet => planet != null)
                                     .OrderByDescending(planet => planet.currentUnitCount)
                                     .ToArray();

        float minX = Mathf.Min(selectionStartPoint.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        float maxX = Mathf.Max(selectionStartPoint.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        float minY = Mathf.Min(selectionStartPoint.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        float maxY = Mathf.Max(selectionStartPoint.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        foreach (Planet planet in playerPlanets)
        {
            if (planet.transform.position.x >= minX && planet.transform.position.x <= maxX &&
                planet.transform.position.y >= minY && planet.transform.position.y <= maxY)
            {
                if (!selectedPlanets.Contains(planet))
                {
                    selectedPlanets.Add(planet);
                    planet.SelectPlanet();
                }
            }
        }
    }

    private void DrawSelectionRectangle(Vector2 startPoint, Vector2 endPoint)
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 5;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, new Vector3(startPoint.x, endPoint.y, 0));
        lineRenderer.SetPosition(2, endPoint);
        lineRenderer.SetPosition(3, new Vector3(endPoint.x, startPoint.y, 0));
        lineRenderer.SetPosition(4, startPoint);
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
                if (planet != targetPlanet)
                { 
                    planet.SendUnitsToPlanet(targetPlanet);
                }
                    planet.DeselectPlanet();
            }
        }
        selectedPlanets.Clear();
    }
}