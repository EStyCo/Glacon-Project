using UnityEngine;
using Zenject;

public class UIProgress : MonoBehaviour
{
    [Inject] private ProgressPlayer player;
    [Inject] private GameManager gameManager;
    [Inject] private UIMain uiMain;

    [Header("Ships")]
    [SerializeField] private GameObject[] shipsSpeed = new GameObject[3];
    [SerializeField] private GameObject[] shipsDamage = new GameObject[3];
    [SerializeField] private GameObject[] shipsArmor = new GameObject[3];

    [Header("Planets")]
    [SerializeField] private GameObject[] planetsArmor = new GameObject[3];
    [SerializeField] private GameObject[] shipsDraft = new GameObject[3];
    [SerializeField] private GameObject[] shipsGrowth = new GameObject[3];


    public bool SetNewValue(string param, int value)
    {
        int points = gameManager.points;
        if (points <= 0)
            return false;

        CheckParameters(param, value);

        return true;
    }

    private void CheckParameters(string param, int value)
    {
        switch (param)
        {
            case "ShipSpeed":
                ChangeValue(value, ref player.speedUnit);
                break;
            default:
                break;
        }
    }

    private void ChangeValue(int value, ref int originalValue)
    {
        if (value - 1 == originalValue)
        { 
            originalValue = value;
            gameManager.points -= 1;

            uiMain.UpdateData();
        }
    }


}
