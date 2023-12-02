using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Classic = 0,
        SpeedTime = 1,
        Stealth = 3,
        Tutorial = 4
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum State
    {
        Disable = 0,
        SandBox = 1,
        Campaign = 2
    }

    [Header("Game Settings")]
    public Difficulty currentDifficulty;
    public GameMode currentGameMode;

    public int planetCount;
    public int countEnemyPlanets;
    public bool blackHoleIsOn;
    public bool portalIsOn;

    [Header("Game State")]
    public State currentState;


    public void ChangeDifficulty(int index)
    {
        currentDifficulty = (Difficulty)index;
    }

    public void ChangeGameMode(int index)
    {
        currentGameMode = (GameMode)index;
    }
}
