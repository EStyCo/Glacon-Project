using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIProgress : MonoBehaviour
{
    [Inject] private ProgressPlayer player;
    [Inject] private GameManager gameManager;
    [Inject] private UIMain uiMain;

    [Header("Ships")]
    [SerializeField] private UIProgressButton[] ships = new UIProgressButton[9];

    [Header("Planets")]
    [SerializeField] private GameObject[] planetsArmor = new GameObject[3];
    [SerializeField] private GameObject[] shipsDraft = new GameObject[3];
    [SerializeField] private GameObject[] shipsGrowth = new GameObject[3];


    private void Start()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        foreach (var item in ships) item.LookPosition();
    }

    public bool SetNewValue(string param, int value)
    {
        if (!CheckStudyBranches(param))
            return false;

        int points = gameManager.points;
        if (points <= 0)
            return false;

        return CheckParameters(param, value);
    }

    private bool Check(ref int origValue, int value)
    {
        if (value - 1 == origValue)
        {
            if (!TakePoints(value)) return false;
            origValue = value;

            player.SaveData();
            uiMain.UpdateData();
            return true;
        }
        return false;
    }

    private bool TakePoints(int index)
    {
        int temp = gameManager.points - index;

        if (temp < 0) return false;

        gameManager.points = temp;
        return true;
    }



    private bool CheckParameters(string param, int value)
    {
        switch (param)
        {
            case "SpeedUnit":
                return Check(ref player.speedUnit, value);

            case "ArmorUnit":
                return Check(ref player.armorUnit, value);

            case "DamageUnit":
                return Check(ref player.damageUnit, value);

            default:
                return false;
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

    private bool CheckStudyBranches(string param)
    {
        Dictionary<string, int> branch = GetBranch(param);
        if (branch.Count >= 2 && !branch.ContainsKey(param)) return false;

        if (branch.Count == 2 && branch.ContainsKey(param) && branch[param] >= 2)
        {
            foreach (var skill in branch)
            {
                if (skill.Key == param && !branch.ContainsValue(3)) return true;
            }
            return false;
        }

        return true;
    }

    private Dictionary<string, int> GetBranch(string param)
    {
        Dictionary<string, int> branch = new Dictionary<string, int>();

        if (param == "SpeedUnit" || param == "ArmorUnit" || param == "DamageUnit")
        {
            if (player.speedUnit > 0) branch.Add("SpeedUnit", player.speedUnit);
            if (player.armorUnit > 0) branch.Add("ArmorUnit", player.armorUnit);
            if (player.damageUnit > 0) branch.Add("DamageUnit", player.damageUnit);
        }
        else
        {
            if (player.armorPlanet > 0) branch.Add("ArmorPlanet", player.armorPlanet);
            if (player.draftPlanet > 0) branch.Add("DraftPlanet", player.draftPlanet);
            if (player.growthPlanet > 0) branch.Add("DraftPlanet", player.growthPlanet);
        }

        return branch;
    }
}
