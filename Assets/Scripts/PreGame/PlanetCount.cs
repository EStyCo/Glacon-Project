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
        // Обновление текста с текущим значением ползунка
        textPlanets.text = "Планеты: " + value.ToString(); // Можете также форматировать строку по вашему желанию
        count = (int)value;
        GameManager.Instance.planetCount = count;
    }
    public void ToggleRandomPlanets()
    {
        bool isRandom = randomPlanets.isOn;

        if (isRandom)
        {
            // Устанавливаем случайное значение в Slider
            float randomValue = Random.Range(10, 26); // От 10 до 25
            sliderPlanets.value = randomValue;

            // Также обновляем текст и передаем значение в GameManager
            UpdatePlanetsValueText(randomValue);
        }

        // Устанавливаем Slider как неактивный (или активный) в зависимости от значения Toggle
        sliderPlanets.interactable = !isRandom;
    }
}
