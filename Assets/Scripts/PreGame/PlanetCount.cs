using TMPro;
using UnityEngine;
using Zenject;

public class PlanetCount : MonoBehaviour
{
    [Inject] GameModeManager gmManager;
    public TextMeshProUGUI textPlanets;
    public UnityEngine.UI.Slider sliderPlanets;
    public UnityEngine.UI.Toggle randomPlanets;

    public int count;
    private bool isRandom = false;

    private void Start()
    {
        sliderPlanets.value = gmManager.planetCount;
        UpdatePlanetsValueText(gmManager.planetCount);
        ToggleRandomPlanets();
    }

    public void OnSliderPlanentsValueChanged()
    {
        UpdatePlanetsValueText(sliderPlanets.value);
    }

    public void UpdatePlanetsValueText(float value)
    {
        if (isRandom) textPlanets.text = "Случайно";
        else textPlanets.text = "Планеты: " + value.ToString();

        count = (int)value;
        gmManager.planetCount = count;
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
