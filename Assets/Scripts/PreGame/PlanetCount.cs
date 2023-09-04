using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetCount : MonoBehaviour
{
    public TextMeshProUGUI textPlanets;
    public UnityEngine.UI.Slider sliderPlanets;
    public UnityEngine.UI.Toggle randomPlanets;

    public int initialPlanetsValue = 15;
    public int count = 15;

    private void Start()
    {
        sliderPlanets.value = initialPlanetsValue;
        UpdatePlanetsValueText(initialPlanetsValue);
        ToggleRandomPlanets();

    }
    public void OnSliderPlanentsValueChanged()
    {
        UpdatePlanetsValueText(sliderPlanets.value);
    }
    public void UpdatePlanetsValueText(float value)
    {
        // ���������� ������ � ������� ��������� ��������
        textPlanets.text = "�������: " + value.ToString(); // ������ ����� ������������� ������ �� ������ �������
        count = (int)value;
        GameManager.Instance.planetCount = count;
    }
    public void ToggleRandomPlanets()
    {
        bool isRandom = randomPlanets.isOn;

        if (isRandom)
        {
            // ������������� ��������� �������� � Slider
            float randomValue = Random.Range(10, 26); // �� 10 �� 25
            sliderPlanets.value = randomValue;

            // ����� ��������� ����� � �������� �������� � GameManager
            UpdatePlanetsValueText(randomValue);
        }

        // ������������� Slider ��� ���������� (��� ��������) � ����������� �� �������� Toggle
        sliderPlanets.interactable = !isRandom;
    }
}
