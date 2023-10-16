using Game;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Timer : MonoBehaviour
{
    [Inject] GameModeManager gameModeManager;
    [SerializeField] private TextMeshProUGUI timerText;
    public float totalTime;
    private float currentTime;

    private void Start()
    {
        GetPlanet();
        currentTime = totalTime;
        UpdateTimerDisplay();
        StartCoroutine(StartTimer());
    }

    public void GetPlanet()
    {
        totalTime = gameModeManager.planetCount * 5;
    }

    private IEnumerator StartTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // Подождите 1 секунду
            currentTime -= 1;
            UpdateTimerDisplay();
        }

        //timerText.text = "00:00";
        SceneManager.LoadScene(3);
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
