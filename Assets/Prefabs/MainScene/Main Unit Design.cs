using UnityEngine;
using UnityEngine.U2D.Animation;

public class MainUnitDesign : MonoBehaviour
{
    public int skinUnits;

    private SpriteResolver skinUnitsResolver;
    public GameObject unitPrefab;
    void Start()
    {
        if (unitPrefab != null)
        {
            skinUnitsResolver = unitPrefab.GetComponent<SpriteResolver>();
        }
        ChangeSkinUnits(skinUnits);
    }

    public void ChangeSkinUnits(int indexSkin)
    {

        skinUnitsResolver.SetCategoryAndLabel("Ships", "Ship" + Random.Range(1, 7).ToString());

    }
}
