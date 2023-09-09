using TMPro;
using UnityEngine;
using Game;

public class PlanetCount : MonoBehaviour
{
    public TextMeshProUGUI textPlanets;
    public UnityEngine.UI.Slider sliderPlanets;
    public UnityEngine.UI.Toggle randomPlanets;

    public int initialPlanetsValue = 15;
    public int count = 15;
    private bool isRandom = false;

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
        if (isRandom) textPlanets.text = "Планеты";
        else textPlanets.text = "Планеты: " + value.ToString();

        count = (int)value;
        GameManager.Instance.planetCount = count;
    }
    public void ToggleRandomPlanets()
    {
        isRandom = randomPlanets.isOn;

        if (isRandom)
        {
            float randomValue = Random.Range(10, 26);

            UpdatePlanetsValueText(randomValue);
        }

        sliderPlanets.interactable = !isRandom;
    }
}
