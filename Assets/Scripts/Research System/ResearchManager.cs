using CodeMonkey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public static ResearchManager Instance;
    private void Awake()
    {
        Instance = this;
        Debug.Log(Instance);
    }
    public class OnUpgradeResearchedArgs : EventArgs { public Upgrade upgrade; }
    public event EventHandler<OnUpgradeResearchedArgs> OnUpgradeResearched;

    public UI_SkillTree skillTree;

    void Start()
    {
        UpgradeInfo.researchedUpgrades = new List<Upgrade>();

        OnUpgradeResearched += ResearchManager_OnUpgradeResearched;
        skillTree.Initialize();
    }


    public string ErrorMessage;
    public bool TryUnlockUpgrade(Upgrade upgrade)
    {
        ErrorMessage = "";
        if (IsUpgradeResearched(upgrade))
        {
            ErrorMessage = "Upgrade already reserached";
            return false;
        }

        if (!IsUpgradeResearchable(upgrade))
        {
            return false;
        }

        int cost = UpgradeInfo.ResearchCosts[upgrade];

        if (cost > GameManager.Instance.ResearchPoints)
        {
            ErrorMessage = ("Cannot Afford Upgrade");
            return false;
        }
        GameManager.Instance.ResearchPoints -= cost;
        UnlockUpgrade(upgrade);
        return true;
    }
    private void UnlockUpgrade(Upgrade upgrade)
    {
        UpgradeInfo.researchedUpgrades.Add(upgrade);
        UpgradeInfo.TierAmounts[UpgradeInfo.UpgradeTiers[upgrade]]++;
        OnUpgradeResearched?.Invoke(this, new OnUpgradeResearchedArgs { upgrade = upgrade });
        Debug.Log("Upgrade Researched");
    }
    public bool IsUpgradeResearched(Upgrade upgrade)
    {
        return UpgradeInfo.researchedUpgrades.Contains(upgrade);
    }
    public bool IsUpgradeResearchable(Upgrade upgrade)
    {
        if (!UpgradeInfo.UnlockRequirements.ContainsKey(upgrade))
        {
            if (UpgradeInfo.UpgradeTiers[upgrade] > 0)
            {
                ErrorMessage = "Upgrade not in requirements: " + upgrade;
            }
            return true;
        }
        switch (UpgradeInfo.UpgradeTiers[upgrade])
        {
            case 0:
            case 1:
                break;
            case 2:
                if (UpgradeInfo.TierAmounts[1] < 5)
                {
                    ErrorMessage = "Not Enough Tier 1 Upgrades Unlocked (required: 5)";
                    return false;
                }
                break;
            case 3:
                if (UpgradeInfo.TierAmounts[2] < 6)
                {
                    ErrorMessage = "Not Enough Tier 2 Upgrades Unlocked (required: 6)";
                    return false;
                }
                break;
        }
        foreach (Upgrade req in UpgradeInfo.UnlockRequirements[upgrade])
        {
            if (!IsUpgradeResearched(req))
            {
                ErrorMessage = "Requirement Not Met: " + req;
                return false;
            }
        }
        return true;
    }

    private void ResearchManager_OnUpgradeResearched(object sender, OnUpgradeResearchedArgs e)
    {
        switch (e.upgrade)
        {
            case Upgrade.None:
                break;
            case Upgrade.Research1:
                break;
            case Upgrade.Research2:
                break;
            case Upgrade.Research3:
                break;
            case Upgrade.Battery1:
                break;
            case Upgrade.Battery2:
                break;
            case Upgrade.Battery3:
                break;
            case Upgrade.HousingAndBusiness:
                break;
            case Upgrade.Housing1:
                break;
            case Upgrade.Housing2:
                break;
            case Upgrade.Housing3:
                break;
            case Upgrade.Business1:
                break;
            case Upgrade.Business2:
                break;
            case Upgrade.Business3:
                break;
            case Upgrade.RenewableEnergy:
                break;
            case Upgrade.Solar1:
                break;
            case Upgrade.Geothermal1:
                break;
            case Upgrade.Geothermal2:
                break;
            case Upgrade.Hydro1:
                break;
            case Upgrade.Hydro2:
                break;
            case Upgrade.Hydro3:
                break;
            case Upgrade.Wind1:
                break;
            case Upgrade.Wind2:
                break;
            case Upgrade.Wind3:
                break;
            case Upgrade.Oil1:
                break;
            case Upgrade.Oil2:
                break;
            case Upgrade.Oil3:
                break;
            case Upgrade.Nuclear1:
                break;
            case Upgrade.Nuclear2:
                break;
            case Upgrade.Nuclear3:
                break;
            case Upgrade.NonrenewableEnergy:
                break;
            case Upgrade.StorageAndResearch:
                break;
        }
    }

    
    //TODO: Move To BuildingController
    public bool IsBuildingUnlocked(BuildingType buildingType)
    {
        switch (buildingType)
        {

            case BuildingType.SOLAR_PANEL:
                return IsUpgradeResearched(Upgrade.Solar1);
            default:
                return true;
        }
    }
    
}
public enum Upgrade
{
    None,
    Research1,
    Research2,
    Research3,
    Battery1,
    Battery2,
    Battery3,
    HousingAndBusiness,
    Housing1,
    Housing2,
    Housing3,
    Business1,
    Business2,
    Business3,
    RenewableEnergy,
    Solar1,
    Geothermal1,
    Geothermal2,
    Hydro1,
    Hydro2,
    Hydro3,
    Wind1,
    Wind2,
    Wind3,
    Oil1,
    Oil2,
    Oil3,
    Nuclear1,
    Nuclear2,
    Nuclear3,
    NonrenewableEnergy,
    StorageAndResearch,
};


public class UpgradeInfo
{
    public static List<Upgrade> researchedUpgrades;

    public static readonly Dictionary<Upgrade, int> ResearchCosts = new()
    {
        {Upgrade.None,               0},
        {Upgrade.Research1,          1},
        {Upgrade.Research2,          2},
        {Upgrade.Research3,          4},
        {Upgrade.Battery1,           2},
        {Upgrade.Battery2,           4},
        {Upgrade.Battery3,           7},
        {Upgrade.HousingAndBusiness, 0},
        {Upgrade.Housing1,           2},
        {Upgrade.Housing2,           5},
        {Upgrade.Housing3,          10},
        {Upgrade.Business1,          2},
        {Upgrade.Business2,          4},
        {Upgrade.Business3,          9},
        {Upgrade.RenewableEnergy,    0},
        {Upgrade.Solar1,             3},
        {Upgrade.Geothermal1,        4},
        {Upgrade.Geothermal2,        6},
        {Upgrade.Hydro1,             1},
        {Upgrade.Hydro2,             3},
        {Upgrade.Hydro3,             6},
        {Upgrade.Wind1,              2},
        {Upgrade.Wind2,              3},
        {Upgrade.Wind3,              7},
        {Upgrade.Oil1,               1},
        {Upgrade.Oil2,               3},
        {Upgrade.Oil3,               5},
        {Upgrade.Nuclear1,           3},
        {Upgrade.Nuclear2,           5},
        {Upgrade.Nuclear3,           8},
        {Upgrade.NonrenewableEnergy, 0},
        {Upgrade.StorageAndResearch, 0},
    };

    public static readonly Dictionary<Upgrade, List<Upgrade>> UnlockRequirements = new() {
        { Upgrade.Research1, new List<Upgrade>{ Upgrade.StorageAndResearch } },
        { Upgrade.Research2, new List<Upgrade>{ Upgrade.Research1 } },
        { Upgrade.Research3, new List<Upgrade>{ Upgrade.Research2 } },
        { Upgrade.Battery1, new List<Upgrade>{ Upgrade.StorageAndResearch } },
        { Upgrade.Battery2, new List<Upgrade>{ Upgrade.Battery1 } },
        { Upgrade.Battery3, new List<Upgrade>{ Upgrade.Battery2, Upgrade.Solar1 } },
        { Upgrade.Housing1, new List<Upgrade>{ Upgrade.HousingAndBusiness } },
        { Upgrade.Housing2, new List<Upgrade>{ Upgrade.Housing1, Upgrade.Business1, Upgrade.Research2 } },
        { Upgrade.Housing3, new List<Upgrade>{ Upgrade.Housing2, Upgrade.Battery2 } },
        { Upgrade.Business1, new List<Upgrade>{ Upgrade.HousingAndBusiness} },
        { Upgrade.Business2, new List<Upgrade>{ Upgrade.Business1 } },
        { Upgrade.Business3, new List<Upgrade>{ Upgrade.Business2, Upgrade.Housing2 } },
        { Upgrade.Solar1, new List<Upgrade>{ Upgrade.Wind1 } },
        { Upgrade.Geothermal1, new List<Upgrade>{ Upgrade.Hydro1 } },
        { Upgrade.Geothermal2, new List<Upgrade>{ Upgrade.Geothermal1, Upgrade.Solar1 } },
        { Upgrade.Hydro1, new List<Upgrade>{ Upgrade.RenewableEnergy } },
        { Upgrade.Hydro2, new List<Upgrade>{ Upgrade.Hydro1 } },
        { Upgrade.Hydro3, new List<Upgrade>{ Upgrade.Hydro2 } },
        { Upgrade.Wind1, new List<Upgrade>{ Upgrade.RenewableEnergy } },
        { Upgrade.Wind2, new List<Upgrade>{ Upgrade.Wind1 } },
        { Upgrade.Wind3, new List<Upgrade>{ Upgrade.Wind2, Upgrade.Solar1 } },
        { Upgrade.Oil1, new List<Upgrade>{ Upgrade.NonrenewableEnergy } },
        { Upgrade.Oil2, new List<Upgrade>{ Upgrade.Oil1 } },
        { Upgrade.Oil3, new List<Upgrade>{ Upgrade.Oil2 } },
        { Upgrade.Nuclear1, new List<Upgrade>{ Upgrade.Oil1 } },
        { Upgrade.Nuclear2, new List<Upgrade>{ Upgrade.Nuclear1 } },
        { Upgrade.Nuclear3, new List<Upgrade>{ Upgrade.Nuclear2 } },
    };

    public static List<int> TierAmounts = new() { 0, 0, 0, 0 };

    public static readonly Dictionary<Upgrade, int> UpgradeTiers = new()
    {
        { Upgrade.None, 0},
        { Upgrade.HousingAndBusiness, 0},
        { Upgrade.RenewableEnergy, 0},
        { Upgrade.NonrenewableEnergy, 0 },
        { Upgrade.StorageAndResearch, 0 },
        { Upgrade.Battery1, 1},
        { Upgrade.Business1, 1},
        { Upgrade.Housing1, 1},
        { Upgrade.Hydro1, 1},
        { Upgrade.Nuclear1, 1},
        { Upgrade.Oil1, 1},
        { Upgrade.Research1, 1},
        { Upgrade.Wind1, 1},
        { Upgrade.Battery2, 2},
        { Upgrade.Business2, 2},
        { Upgrade.Geothermal1, 2},
        { Upgrade.Housing2, 2},
        { Upgrade.Hydro2, 2},
        { Upgrade.Nuclear2, 2},
        { Upgrade.Oil2, 2},
        { Upgrade.Research2, 2},
        { Upgrade.Solar1, 2},
        { Upgrade.Wind2, 2},
        { Upgrade.Battery3, 3},
        { Upgrade.Business3, 3},
        { Upgrade.Geothermal2, 3},
        { Upgrade.Housing3, 3},
        { Upgrade.Hydro3, 3},
        { Upgrade.Nuclear3, 3},
        { Upgrade.Oil3, 3},
        { Upgrade.Research3, 3},
        { Upgrade.Wind3, 3},
    };

}


/*
case Upgrade.None:
    break;
case Upgrade.Research1:
    break;
case Upgrade.Research2:
    break;
case Upgrade.Research3:
    break;
case Upgrade.Battery1:
    break;
case Upgrade.Battery2:
    break;
case Upgrade.Battery3:
    break;
case Upgrade.HousingAndBusiness:
    break;
case Upgrade.Housing1:
    break;
case Upgrade.Housing2:
    break;
case Upgrade.Housing3:
    break;
case Upgrade.Business1:
    break;
case Upgrade.Business2:
    break;
case Upgrade.Business3:
    break;
case Upgrade.RenewableEnergy:
    break;
case Upgrade.Solar1:
    break;
case Upgrade.Geothermal1:
    break;
case Upgrade.Geothermal2:
    break;
case Upgrade.Hydro1:
    break;
case Upgrade.Hydro2:
    break;
case Upgrade.Hydro3:
    break;
case Upgrade.Wind1:
    break;
case Upgrade.Wind2:
    break;
case Upgrade.Wind3:
    break;
case Upgrade.Oil1:
    break;
case Upgrade.Oil2:
    break;
case Upgrade.Oil3:
    break;
case Upgrade.Nuclear1:
    break;
case Upgrade.Nuclear2:
    break;
case Upgrade.Nuclear3:
    break;
case Upgrade.NonrenewableEnergy:
    break;
case Upgrade.StorageAndResearch:
    break;
*/
