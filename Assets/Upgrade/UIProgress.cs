using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIProgress : MonoBehaviour
{
    [Inject] private ProgressPlayer player;

    [SerializeField] private LoadData loadData;
    [SerializeField] private CheckPoints checkPoints;

    [Header("Ships")]
    [SerializeField] private UIProgressButton[] ships = new UIProgressButton[0];

    [Header("Planets")]
    [SerializeField] private UIProgressButton[] planets = new UIProgressButton[0];


    private void Start()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        foreach (var item in ships) item.LookPosition();
        foreach (var item in planets) item.LookPosition();
    }

    public bool SetNewValue(string param, int value)
    {
        if (!CheckStudyBranches(param))
            return false;

        return CheckParameters(param, value);
    }

    private bool Check(ref int origValue, int value)
    {
        if (value - 1 == origValue)
        {
            if (loadData.isSandbox)
            {
                origValue = value;
                player.SaveDataSandBox();
            }
            else
            {
                return TakePoints(ref origValue, value);
            }

            return true;
        }
        return false;
    }

    private bool TakePoints(ref int origValue, int value)
    {
        if (player.points >= value)
        {
            player.points -= value;
            origValue = value;
            player.SaveDataCampaign();
            checkPoints.UpdateTextPoints();

            return true;
        }
        else return false;
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

            case "ArmorPlanet":
                return Check(ref player.armorPlanet, value);

            case "DraftPlanet":
                return Check(ref player.draftPlanet, value);

            case "GrowthPlanet":
                return Check(ref player.growthPlanet, value);

            default:
                return false;
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
            if (player.growthPlanet > 0) branch.Add("GrowthPlanet", player.growthPlanet);
        }

        return branch;
    }
}
