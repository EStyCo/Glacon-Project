using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitializeGM : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;
    [Inject] ProgressPlayer player;
    [Inject] ProgressEnemy1 enemy1;

    [Header("GameMode")]
    [SerializeField] private GameObject classicGM;
    [SerializeField] private GameObject speedGM;
    [SerializeField] private GameObject stealthGM;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialGM;
    [SerializeField] private GameObject tutorialCanvas;

    [Header("Other")]
    [SerializeField] private GameObject damper;

    private string gameMode;

    void Start()
    {
        ClearComponents();
        GetGM();
        ChangeGM();
    }
    private void ClearComponents()
    {
        tutorialCanvas.SetActive(false);
        classicGM.SetActive(false);
        speedGM.SetActive(false);
        tutorialGM.SetActive(false);
        damper.SetActive(true);
    }
    private void GetGM()
    {
        gameMode = gameModeManager.currentGameMode.ToString();
    }

    private void ChangeGM()
    {
        switch (gameMode)
        {
            case "Classic":
                SelectClassicMode();
                break;

            case "SpeedTime":
                SelectSpeedTimeMode();
                break;

            case "Tutorial":
                SelectTutorialMode();
                break;

            default:
                SelectClassicMode();
                break;      
        }
    }

    private void SelectClassicMode()
    {
        classicGM.SetActive(true);
    }

    private void SelectSpeedTimeMode()
    {
        speedGM.SetActive(true);
        damper.SetActive(false);
    }

    private void SelectTutorialMode()
    {
        player.DisableProgress();
        enemy1.ResetProgress();

        tutorialCanvas.SetActive(true);
        tutorialGM.SetActive(true);
    }
}
