using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public int planetCount = 15;

    public bool blackHoleIsOn = false;
    public bool portalIsOn = false;

    public enum GameMode
    {
        Classic = 0,
        SpeedTime = 1,
        Stealth = 3
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public int countEnemyPlanets = 1;

    public Difficulty currentDifficulty = Difficulty.Medium;
    public GameMode currentGameMode = GameMode.Classic;

    public void ChangeDifficulty(int index)
    {
        currentDifficulty = (Difficulty)index; 
    }

    public void ChangeGameMode(int index)
    {
        currentGameMode = (GameMode)index;
    }
}
