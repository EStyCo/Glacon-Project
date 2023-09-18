using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class GameModeManager : MonoBehaviour
{
    public GameObject spawnerPlanets;
    public enum GameMode
    {
        Classic = 0,
        SpeedTime = 1
    }
    public static GameModeManager Instance { get; private set; }

    public int countEnemyPlanets = 1; 

    public GameMode currentGameMode = GameMode.Classic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ChangeMode();
    }
    public void ChangeMode()
    {

        Classic classic = spawnerPlanets.GetComponent<Classic>();
        SpeedTime speedTime = spawnerPlanets.GetComponent<SpeedTime>();


        classic.enabled = false;
        speedTime.enabled = false;

        if (currentGameMode == GameMode.Classic) classic.enabled = true;
        else if (currentGameMode == GameMode.SpeedTime) speedTime.enabled = true;
    }
}
