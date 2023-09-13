using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public static Speedometer Instance { get; private set; }

    public TextMeshProUGUI textSpedometer;
    public int countSpeedometer = 50;

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
    void Start()
    {
        textSpedometer.text = countSpeedometer.ToString() + "%";
    }

    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta > 0 && countSpeedometer < 100)
        {
            countSpeedometer += 5;
            textSpedometer.text = countSpeedometer.ToString() + "%";
        }
        else if (scrollDelta < 0 && countSpeedometer > 20)
        {
            countSpeedometer -= 5;
            textSpedometer.text = countSpeedometer.ToString() + "%";
        }
    }
}
