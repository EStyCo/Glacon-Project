using Game;
using UnityEngine;
using UnityEngine.U2D.Animation;

public static class ShipDesign
{
    /// <summary>
    /// Меняет планете Игрока скин юнитов и крейсеров исходя из выбранного скина.
    /// </summary>
    /// <param name="planetPrefab"> Планета. </param>
    /// <param name="unitPrefab"> "Префаб Юнита" </param>
    /// <param name="cruiserPrefab"> "Префаб Крейсера" </param>
    public static void ChangePlayerSkin(GameObject planetPrefab, GameObject unitPrefab, GameObject cruiserPrefab)
    {
        int skinUnits = GameManager.Instance.skinUnits;
        GameObject instance = planetPrefab;

        GameObject tempUnitPrefab = instance.GetComponent<Planet>().unitPrefab = unitPrefab;
        SpriteResolver skinUnitsResolver = tempUnitPrefab?.GetComponent<SpriteResolver>();
        skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + skinUnits.ToString());

        GameObject tempCruiserPrefab = instance.GetComponent<Planet>().cruiserPrefab = cruiserPrefab;
        SpriteResolver skinCruisersResolver = tempCruiserPrefab?.GetComponent<SpriteResolver>();
        skinCruisersResolver.SetCategoryAndLabel("Ships", "Ship" + skinUnits.ToString());
    }

    /// <summary>
    /// Меняет планете Противника скин юнитов и крейсеров рандомно.
    /// </summary>
    /// <param name="planetPrefab"> Планета </param>
    /// <param name="unitPrefab"> "Префаб Юнита" </param>
    /// <param name="cruiserPrefab"> "Префаб Крейсера" </param>
    public static void ChangeEnemySkin(GameObject planetPrefab, GameObject unitPrefab, GameObject cruiserPrefab)
    {
        GameObject instance = planetPrefab;

        GameObject tempUnitPrefab = instance.GetComponent<Planet>().unitPrefab = unitPrefab;
        SpriteResolver skinUnitsResolver = tempUnitPrefab?.GetComponent<SpriteResolver>();
        skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + Random.Range(1, 7).ToString());

        GameObject tempCruiserPrefab = instance.GetComponent<Planet>().cruiserPrefab = cruiserPrefab;
        SpriteResolver skinCruisersResolver = tempCruiserPrefab?.GetComponent<SpriteResolver>();
        skinCruisersResolver.SetCategoryAndLabel("Ships", "Ship" + Random.Range(1, 7).ToString());
    }
}
