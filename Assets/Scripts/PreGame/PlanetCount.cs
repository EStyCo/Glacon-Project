using TMPro;
using UnityEngine;
using Zenject;

public class PlanetCount : MonoBehaviour
{
    [Inject] GameManager gameManager;
    public TextMeshProUGUI textPlanets;
    public UnityEngine.UI.Slider sliderPlanets;
    public UnityEngine.UI.Toggle randomPlanets;

    public int count;
    private bool isRandom = false;

    private void Start()
    {
        sliderPlanets.value = gameManager.planets;
        UpdatePlanetsValueText(gameManager.planets);
        ToggleRandomPlanets();
    }

    public void OnSliderPlanentsValueChanged()
    {
        UpdatePlanetsValueText(sliderPlanets.value);
    }

    public void UpdatePlanetsValueText(float value)
    {
        if (isRandom) textPlanets.text = "�������";
        else textPlanets.text = "�������: " + value.ToString();

        count = (int)value;
        gameManager.ChangePlanets(count);
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
