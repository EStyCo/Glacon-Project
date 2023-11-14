using UnityEngine;
using UnityEngine.U2D.Animation;

public static class ShipDesign
{
    /// <summary>
    /// ������ ������� ������ ���� ������ � ��������� ������ �� ���������� �����.
    /// </summary>
    /// <param name="planetPrefab"> �������. </param>
    /// <param name="unitPrefab"> "������ �����" </param>
    /// <param name="cruiserPrefab"> "������ ��������" </param>
    public static void ChangePlayerSkin(GameManager gameManager, GameObject planetPrefab, GameObject unitPrefab, GameObject cruiserPrefab)
    {
        int skinUnits = gameManager.skinUnits;
        GameObject instance = planetPrefab;

        GameObject tempUnitPrefab = instance.GetComponent<Planet>().unitPrefab = unitPrefab;
        SpriteResolver skinUnitsResolver = tempUnitPrefab?.GetComponent<SpriteResolver>();
        skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + skinUnits.ToString());

        GameObject tempCruiserPrefab = instance.GetComponent<Planet>().cruiserPrefab = cruiserPrefab;
        SpriteResolver skinCruisers = tempCruiserPrefab?.GetComponent<Cruiser>().skinCruiser;
        skinCruisers.SetCategoryAndLabel("Cruiser", "Cruiser" + skinUnits.ToString());

        SpriteResolver skinShieldCruisers = tempCruiserPrefab?.GetComponent<Cruiser>().skinShield;
        skinShieldCruisers.SetCategoryAndLabel("CruiserShield", "Shield" + skinUnits.ToString());
    }

    /// <summary>
    /// ������ ������� ���������� ���� ������ � ��������� ��������.
    /// </summary>
    /// <param name="planetPrefab"> ������� </param>
    /// <param name="unitPrefab"> "������ �����" </param>
    /// <param name="cruiserPrefab"> "������ ��������" </param>
    public static void ChangeEnemySkin(GameObject planetPrefab, GameObject unitPrefab, GameObject cruiserPrefab)
    {
        int randomIndex = Random.Range(1, 6);
        GameObject instance = planetPrefab;

        GameObject tempUnitPrefab = instance.GetComponent<Planet>().unitPrefab = unitPrefab;
        SpriteResolver skinUnitsResolver = tempUnitPrefab?.GetComponent<SpriteResolver>();
        skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + randomIndex.ToString());

        GameObject tempCruiserPrefab = instance.GetComponent<Planet>().cruiserPrefab = cruiserPrefab;
        SpriteResolver skinCruisers = tempCruiserPrefab?.GetComponent<Cruiser>().skinCruiser;
        skinCruisers.SetCategoryAndLabel("Cruiser", "Cruiser" + randomIndex.ToString());

        SpriteResolver skinShieldCruisers = tempCruiserPrefab?.GetComponent<Cruiser>().skinShield;
        skinShieldCruisers.SetCategoryAndLabel("CruiserShield", "Shield" + randomIndex.ToString());
    }
}
