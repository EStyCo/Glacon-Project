using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Zenject;

public class UIProgressButton : MonoBehaviour
{
    [Inject] private UIProgress uiProgress;
    [Inject] private ProgressPlayer player;

    [SerializeField] private int value;
    [SerializeField] private string param;
    [SerializeField] private SpriteResolver substrateResolver;
    [SerializeField] private SpriteRenderer substrateRenderer;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color selectColor;

    [Header("Name")]
    [SerializeField] private string nameSkill;
    [SerializeField] private TextMeshProUGUI textName;

    [Header("Description")]
    [SerializeField] private string description;
    [SerializeField] private TextMeshProUGUI textDescription;

    public void Upgrade()
    {
        if (uiProgress.SetNewValue(param, value))
        {
            SelectButton(true);
            ChangeSprite(true);
        }
        else SelectButton(false);
    }

    public void SelectButton(bool isSelected)
    {
        if (isSelected)
        {
            substrateRenderer.color = selectColor;
            ShowDescription(true);
        }
        else
        {
            substrateRenderer.color = originalColor;
            ShowDescription(false);
        }
    }

    public void ChangeSprite(bool state)
    {
        SpriteResolver spriteResolver = gameObject.GetComponent<SpriteResolver>();

        if (state)
        {
            spriteResolver.SetCategoryAndLabel("Button", "Chosen");
            substrateResolver.SetCategoryAndLabel("Substrate", "Chosen");
        }
        else
        {
            spriteResolver.SetCategoryAndLabel("Button", "UnChosen");
            substrateResolver.SetCategoryAndLabel("Substrate", "UnChosen");
        }
    }

    public void LookPosition()
    {
        int originalValue = GetParam(param);

        if (value <= originalValue) ChangeSprite(true);
        else ChangeSprite(false);
    }

    private void ShowDescription(bool isShow)
    {
        if (isShow)
        {
            textName.text = nameSkill;
            textDescription.text = description;
        }
        else
        {
            textName.text = "";
            textDescription.text =  "";
        }

    }

    private int GetParam(string param)
    {
        switch (param)
        {
            case "SpeedUnit":
                return player.speedUnit;

            case "ArmorUnit":
                return player.armorUnit;

            case "DamageUnit":
                return player.damageUnit;

            case "ArmorPlanet":
                return player.armorPlanet;

            case "DraftPlanet":
                return player.draftPlanet;

            case "GrowthPlanet":
                return player.growthPlanet;

            default:
                return 0;
        }
    }
}
