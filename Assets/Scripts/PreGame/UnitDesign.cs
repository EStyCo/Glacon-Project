using Game;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class UnitDesign : MonoBehaviour
{
    enum Property
    {
        Player,
        Enemy,
        Neutral
    }
    public int skinUnits;
    private Property property;

    private SpriteResolver skinUnitsResolver;
    public GameObject unitPrefab;
    void Start()
    {
        if (unitPrefab != null)
        {
            skinUnitsResolver = unitPrefab.GetComponent<SpriteResolver>();
        }
        CheckProperty();
        CheckIndexSkin();
        ChangeSkinUnits(skinUnits);
    }

    private void CheckProperty()
    {
        if (gameObject.CompareTag("PlayerPlanet"))
        {
            property = Property.Player;
        }
        else if (gameObject.CompareTag("Enemy1") || gameObject.CompareTag("Enemy2") || gameObject.CompareTag("Enemy3"))
        {
            property = Property.Enemy;
        }
        else
        {
            property = Property.Neutral;
        }
    }
    private void CheckIndexSkin()
    {
        skinUnits = GameManager.Instance.skinUnits;
    }

    public void ChangeSkinUnits(int indexSkin)
    {
        if (property == Property.Player)
        {
            skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + indexSkin.ToString());
        }
        else if (property == Property.Enemy)
        {
            skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + Random.Range(1, 7).ToString());
        }
    }
}
