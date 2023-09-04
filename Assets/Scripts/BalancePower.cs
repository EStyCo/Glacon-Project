using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalancePower : MonoBehaviour
{
    public static BalancePower Instance { get; private set; }

    public Slider ratioSlider;

    public int enemyPower = 50;
    public int playerPower = 50;
    void Start()
    {
        InvokeRepeating("UpdateSliderValue", 1f, 0.5f);
    }
    private void Update()
    {

    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateSliderValue()
    {
        float unitRatio = (float)playerPower / (playerPower + enemyPower);
        ratioSlider.value = unitRatio;
    }
    public void ChangeEnemyPower(bool creat)
    {
        if (creat) enemyPower++;
        if (!creat) enemyPower--;
    }
    public void ChangePlayerPower(bool creat) 
    {
        if (creat) playerPower++;
        if (!creat) playerPower--;
    }
    
}
