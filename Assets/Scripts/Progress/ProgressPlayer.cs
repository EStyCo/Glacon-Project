using UnityEngine;

public class ProgressPlayer : MonoBehaviour
{
    [Header("Campaign")]
    public int completedlevel;
    public int points;

    [Header("Perks")]
    [Range(0, 3)] public int speedUnit = 0;
    [Range(0, 3)] public int armorUnit = 0;
    [Range(0, 3)] public int damageUnit = 0;

    [Range(0, 3)] public int armorPlanet = 0;
    [Range(0, 3)] public int draftPlanet = 0;
    [Range(0, 3)] public int growthPlanet = 0;

    private void Awake()
    {
        //LoadData();
    }

    public void SaveDataSandBox()
    {
        PlayerPrefs.SetInt("SpeedUnit", speedUnit);
        PlayerPrefs.SetInt("ArmorUnit", armorUnit);
        PlayerPrefs.SetInt("DamageUnit", damageUnit);

        PlayerPrefs.SetInt("ArmorPlanet", armorPlanet);
        PlayerPrefs.SetInt("DraftPlanet", draftPlanet);
        PlayerPrefs.SetInt("GrowthPlanet", growthPlanet);
    }

    public void SaveDataCampaign()
    {
        PlayerPrefs.SetInt("CompletedLevel", completedlevel);
        PlayerPrefs.SetInt("Points", points);

        PlayerPrefs.SetInt("SpeedUnitCampaign", speedUnit);
        PlayerPrefs.SetInt("ArmorUnitCampaign", armorUnit);
        PlayerPrefs.SetInt("DamageUnitCampaign", damageUnit);

        PlayerPrefs.SetInt("ArmorPlanetCampaign", armorPlanet);
        PlayerPrefs.SetInt("DraftPlanetCampaign", draftPlanet);
        PlayerPrefs.SetInt("GrowthPlanetCampaign", growthPlanet);
    }

    public void ResetDataCampaign()
    {
        PlayerPrefs.DeleteKey("CompletedLevel");
        PlayerPrefs.DeleteKey("Points");

        PlayerPrefs.DeleteKey("SpeedUnitCampaign");
        PlayerPrefs.DeleteKey("ArmorUnitCampaign");
        PlayerPrefs.DeleteKey("DamageUnitCampaign");

        PlayerPrefs.DeleteKey("ArmorPlanetCampaign");
        PlayerPrefs.DeleteKey("DraftPlanetCampaign");
        PlayerPrefs.DeleteKey("GrowthPlanetCampaign");

        LoadDataCampaign();
    }

    public void ResetDataSandbox()
    {
        PlayerPrefs.DeleteKey("SpeedUnit");
        PlayerPrefs.DeleteKey("ArmorUnit");
        PlayerPrefs.DeleteKey("DamageUnit");

        PlayerPrefs.DeleteKey("ArmorPlanet");
        PlayerPrefs.DeleteKey("DraftPlanet");
        PlayerPrefs.DeleteKey("GrowthPlanet");

        LoadDataSandbox();
    }

    public void LoadDataSandbox()
    {
        if (PlayerPrefs.HasKey("SpeedUnit")) speedUnit = PlayerPrefs.GetInt("SpeedUnit");
        else speedUnit = 0;

        if (PlayerPrefs.HasKey("ArmorUnit")) armorUnit = PlayerPrefs.GetInt("ArmorUnit");
        else armorUnit = 0;

        if (PlayerPrefs.HasKey("DamageUnit")) damageUnit = PlayerPrefs.GetInt("DamageUnit");
        else damageUnit = 0;

        if (PlayerPrefs.HasKey("ArmorPlanet")) armorPlanet = PlayerPrefs.GetInt("ArmorPlanet");
        else armorPlanet = 0;

        if (PlayerPrefs.HasKey("DraftPlanet")) draftPlanet = PlayerPrefs.GetInt("DraftPlanet");
        else draftPlanet = 0;

        if (PlayerPrefs.HasKey("GrowthPlanet")) growthPlanet = PlayerPrefs.GetInt("GrowthPlanet");
        else growthPlanet = 0;
    }

    public void LoadDataCampaign()
    {
        if (PlayerPrefs.HasKey("CompletedLevel")) completedlevel = PlayerPrefs.GetInt("CompletedLevel");
        else completedlevel = 0;

        if (PlayerPrefs.HasKey("Points")) points = PlayerPrefs.GetInt("Points");
        else points = 0;

        if (PlayerPrefs.HasKey("SpeedUnitCampaign")) speedUnit = PlayerPrefs.GetInt("SpeedUnitCampaign");
        else speedUnit = 0;

        if (PlayerPrefs.HasKey("ArmorUnitCampaign")) armorUnit = PlayerPrefs.GetInt("ArmorUnitCampaign");
        else armorUnit = 0;

        if (PlayerPrefs.HasKey("DamageUnitCampaign")) damageUnit = PlayerPrefs.GetInt("DamageUnitCampaign");
        else damageUnit = 0;

        if (PlayerPrefs.HasKey("ArmorPlanetCampaign")) armorPlanet = PlayerPrefs.GetInt("ArmorPlanetCampaign");
        else armorPlanet = 0;

        if (PlayerPrefs.HasKey("DraftPlanetCampaign")) draftPlanet = PlayerPrefs.GetInt("DraftPlanetCampaign");
        else draftPlanet = 0;

        if (PlayerPrefs.HasKey("GrowthPlanetCampaign")) growthPlanet = PlayerPrefs.GetInt("GrowthPlanetCampaign");
        else growthPlanet = 0;
    }

    public void DisableProgress()
    {
        speedUnit = 0;
        armorUnit = 0;
        damageUnit = 0;
        armorPlanet = 0;
        draftPlanet = 0;
        growthPlanet = 0;
    }

    public void CompletedLevel()
    {
        points++;
        completedlevel++;

        SaveDataCampaign();
    }
}
