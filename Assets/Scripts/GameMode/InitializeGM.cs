using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitializeGM : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;

    [SerializeField] private GameObject classicGM;
    [SerializeField] private GameObject speedGM;
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
        classicGM.SetActive(false);
        speedGM.SetActive(false);
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
}
