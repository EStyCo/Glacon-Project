using UnityEngine;

public class ProgressPlayer : MonoBehaviour
{
    [Range(0, 3)] public int speedUnit = 0;
    [Range(0, 3)] public int armorUnit = 0;
    [Range(0, 3)] public int damageUnit = 0;

    [Range(0, 3)] public int armorPlanet = 0;
    [Range(0, 3)] public int draftPlanet = 0;
    [Range(0, 3)] public int growthPlanet = 0;

    private void Awake()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("SpeedUnit", speedUnit);
        PlayerPrefs.SetInt("ArmorUnit", armorUnit);
        PlayerPrefs.SetInt("DamageUnit", damageUnit);
    }
    public void ResetData()
    { 
        
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("SpeedUnit")) speedUnit = PlayerPrefs.GetInt("SpeedUnit");
        else speedUnit = 0;

        if (PlayerPrefs.HasKey("ArmorUnit")) armorUnit = PlayerPrefs.GetInt("ArmorUnit");
        else armorUnit = 0;

        if (PlayerPrefs.HasKey("DamageUnit")) damageUnit = PlayerPrefs.GetInt("DamageUnit");
        else damageUnit = 0;
    }

}
