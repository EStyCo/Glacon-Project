using TMPro;
using UnityEngine;
using Zenject;

public class ShowProgress : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    [SerializeField] private TextMeshProUGUI speedUText;
    [SerializeField] private TextMeshProUGUI armorUText;
    [SerializeField] private TextMeshProUGUI damageUText;
    [SerializeField] private TextMeshProUGUI armorPText;
    [SerializeField] private TextMeshProUGUI growthPText;
    [SerializeField] private TextMeshProUGUI draftPText;

    public void ShowProgressCampaign()
    {
        speedUText.text = $"Speed - [{player.speedUnit}]lvl";
        armorUText.text = $"Armor - [{player.armorUnit}]lvl";
        damageUText.text = $"Damage - [{player.damageUnit}]lvl";

        armorPText.text = $"Armor - [{player.armorPlanet}]lvl";
        growthPText.text = $"Growth - [{player.growthPlanet}]lvl";
        draftPText.text = $"Draft - [{player.draftPlanet}]lvl";
    }
}
