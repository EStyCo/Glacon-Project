using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public bool blackHoleIsOn = false;
    public enum GameMode
    {
        Classic = 0,
        SpeedTime = 1,
        Void = 3
    }

    public int countEnemyPlanets = 1; 

    public GameMode currentGameMode = GameMode.Classic;

    private void Start()
    {
        
    }
}
