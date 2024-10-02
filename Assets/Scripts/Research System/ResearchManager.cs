using CodeMonkey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    
    public class OnUpgradeResearchedArgs : EventArgs { public Upgrades upgrade; }
    public event EventHandler<OnUpgradeResearchedArgs> OnUpgradeResearched;

    public UI_SkillTree skillTree;

    void Start()
    {
        UpgradeInfo.researchedUpgrades = new List<Upgrades>();

        OnUpgradeResearched += ResearchManager_OnUpgradeResearched;
        skillTree.Initialize();
    }

    
    public void TryUnlockUpgrade(Upgrades upgrade)
    {
        if (UpgradeInfo.researchedUpgrades.Contains(upgrade))
        {
            Debug.Log("Upgrade already reserached");
            return;
        }
        if (!IsUpgradeResearchable(upgrade))
        {
            Debug.Log("Upgrade not unlocked yet");
            return;
        }
        int cost = CostToResearch(upgrade);
        if (cost > GameManager.Instance.ResearchPoints)
        {
            Debug.Log("Cannot Afford Upgrade");
            return;
        }
        GameManager.Instance.ResearchPoints -= cost;
        UnlockUpgrade(upgrade);
    }
    private void UnlockUpgrade(Upgrades upgrade)
    {
        UpgradeInfo.researchedUpgrades.Add(upgrade);
        OnUpgradeResearched?.Invoke(this, new OnUpgradeResearchedArgs { upgrade = upgrade });
        Debug.Log("Upgrade Researched");
    }

    private void ResearchManager_OnUpgradeResearched(object sender, OnUpgradeResearchedArgs e)
    {
        switch (e.upgrade)
        {
            case Upgrades.Solar1:
                Debug.Log("Solar Level 1 Unlocked");
                break;
            case Upgrades.Geothermal1:
                Debug.Log("Solar Level 2 Unlocked");
                break;
        }
    }

    public bool IsUpgradeResearched(Upgrades upgrade)
    {
        return UpgradeInfo.researchedUpgrades.Contains(upgrade);
    }
    public bool IsUpgradeResearchable(Upgrades upgrade)
    {
        if (!UpgradeInfo.UnlockRequirements.ContainsKey(upgrade))
        {
            return true;
        }
        bool unlockable = true;
        foreach (Upgrades req in UpgradeInfo.UnlockRequirements[upgrade])
        {
            unlockable = unlockable && UpgradeInfo.researchedUpgrades.Contains(req);
            if (!unlockable)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsBuildingUnlocked(BuildingController.BuildingType buildingType)
    {
        switch (buildingType)
        {

            case BuildingController.BuildingType.SOLAR_PANEL:
                return IsUpgradeResearched(Upgrades.Solar1);
            default:
                return true;
        }
    }

    
    private int CostToResearch(Upgrades upgrade)
    {
        return UpgradeInfo.ResearchCosts[upgrade];
    }

    
}
public enum Upgrades
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
};


public class UpgradeInfo
{
    public static List<Upgrades> researchedUpgrades;

    public static readonly Dictionary<Upgrades, int> ResearchCosts = new()
    {
        {Upgrades.None,               0},
        {Upgrades.Research1,          1},
        {Upgrades.Research2,          2},
        {Upgrades.Research3,          4},
        {Upgrades.Battery1,           2},
        {Upgrades.Battery2,           4},
        {Upgrades.Battery3,           7},
        {Upgrades.HousingAndBusiness, 0},
        {Upgrades.Housing1,           2},
        {Upgrades.Housing2,           5},
        {Upgrades.Housing3,          10},
        {Upgrades.Business1,          2},
        {Upgrades.Business2,          4},
        {Upgrades.Business3,          9},
        {Upgrades.RenewableEnergy,    1},
        {Upgrades.Solar1,             3},
        {Upgrades.Geothermal1,        4},
        {Upgrades.Geothermal2,        6},
        {Upgrades.Hydro1,             1},
        {Upgrades.Hydro2,             3},
        {Upgrades.Hydro3,             6},
        {Upgrades.Wind1,              2},
        {Upgrades.Wind2,              3},
        {Upgrades.Wind3,              7},
        {Upgrades.Oil1,               1},
        {Upgrades.Oil2,               3},
        {Upgrades.Oil3,               5},
        {Upgrades.Nuclear1,           3},
        {Upgrades.Nuclear2,           5},
        {Upgrades.Nuclear3,           8},
    };

    public static readonly Dictionary<Upgrades, List<Upgrades>> UnlockRequirements = new() {
        { Upgrades.Research2, new List<Upgrades>{ Upgrades.Research1 } },
        { Upgrades.Research3, new List<Upgrades>{ Upgrades.Research2 } },
        { Upgrades.Battery2, new List<Upgrades>{ Upgrades.Battery1 } },
        { Upgrades.Battery3, new List<Upgrades>{ Upgrades.Battery3, Upgrades.Solar1 } },
        { Upgrades.Housing1, new List<Upgrades>{ Upgrades.HousingAndBusiness } },
        { Upgrades.Housing2, new List<Upgrades>{ Upgrades.Housing1, Upgrades.Business1 } },
        { Upgrades.Housing3, new List<Upgrades>{ Upgrades.Housing2, Upgrades.Battery2 } },
        { Upgrades.Business2, new List<Upgrades>{ Upgrades.Business1 } },
        { Upgrades.Business3, new List<Upgrades>{ Upgrades.Business2, Upgrades.Housing2, Upgrades.Research2 } },
        { Upgrades.Solar1, new List<Upgrades>{ Upgrades.Wind1 } },
        { Upgrades.Geothermal1, new List<Upgrades>{ Upgrades.Hydro1 } },
        { Upgrades.Geothermal2, new List<Upgrades>{ Upgrades.Geothermal1, Upgrades.Solar1 } },
        { Upgrades.Hydro1, new List<Upgrades>{ Upgrades.RenewableEnergy } },
        { Upgrades.Hydro2, new List<Upgrades>{ Upgrades.Hydro1 } },
        { Upgrades.Hydro3, new List<Upgrades>{ Upgrades.Hydro2 } },
        { Upgrades.Wind1, new List<Upgrades>{ Upgrades.RenewableEnergy } },
        { Upgrades.Wind2, new List<Upgrades>{ Upgrades.Wind1 } },
        { Upgrades.Wind3, new List<Upgrades>{ Upgrades.Wind2, Upgrades.Solar1 } },
        { Upgrades.Oil2, new List<Upgrades>{ Upgrades.Oil1 } },
        { Upgrades.Oil3, new List<Upgrades>{ Upgrades.Oil2 } },
        { Upgrades.Nuclear1, new List<Upgrades>{ Upgrades.Battery1 } },
        { Upgrades.Nuclear2, new List<Upgrades>{ Upgrades.Nuclear1 } },
        { Upgrades.Nuclear3, new List<Upgrades>{ Upgrades.Nuclear2 } },
    };

    public static List<int> TierAmounts = new() { 0, 0, 0, 0 };

    public static readonly Dictionary<Upgrades, int> UpgradeTiers = new()
    {
        { Upgrades.None, 0},
        { Upgrades.Research1, 1},
        { Upgrades.Research2, 2},
        { Upgrades.Research3, 3},
        { Upgrades.Battery1, 1},
        { Upgrades.Battery2, 2},
        { Upgrades.Battery3, 3},
        { Upgrades.HousingAndBusiness, 0},
        { Upgrades.Housing1, 1},
        { Upgrades.Housing2, 2},
        { Upgrades.Housing3, 3},
        { Upgrades.Business1, 1},
        { Upgrades.Business2, 2},
        { Upgrades.Business3, 3},
        { Upgrades.RenewableEnergy, 0},
        { Upgrades.Solar1, 2},
        { Upgrades.Geothermal1, 2},
        { Upgrades.Geothermal2, 3},
        { Upgrades.Hydro1, 1},
        { Upgrades.Hydro2, 2},
        { Upgrades.Hydro3, 3},
        { Upgrades.Wind1, 1},
        { Upgrades.Wind2, 2},
        { Upgrades.Wind3, 3},
        { Upgrades.Oil1, 1},
        { Upgrades.Oil2, 2},
        { Upgrades.Oil3, 3},
        { Upgrades.Nuclear1, 1},
        { Upgrades.Nuclear2, 2},
        { Upgrades.Nuclear3, 3},
    };


}


/*
case Upgrades.None:
    break;
case Upgrades.Research1:
    break;
case Upgrades.Research2:
    break;
case Upgrades.Research3:
    break;
case Upgrades.Battery1:
    break;
case Upgrades.Battery2:
    break;
case Upgrades.Battery3:
    break;
case Upgrades.HousingAndBusiness:
    break;
case Upgrades.Housing1:
    break;
case Upgrades.Housing2:
    break;
case Upgrades.Housing3:
    break;
case Upgrades.Business1:
    break;
case Upgrades.Business2:
    break;
case Upgrades.Business3:
    break;
case Upgrades.RenewableEnergy:
    break;
case Upgrades.Solar1:
    break;
case Upgrades.Geothermal1:
    break;
case Upgrades.Geothermal2:
    break;
case Upgrades.Hydro1:
    break;
case Upgrades.Hydro2:
    break;
case Upgrades.Hydro3:
    break;
case Upgrades.Wind1:
    break;
case Upgrades.Wind2:
    break;
case Upgrades.Wind3:
    break;
case Upgrades.Oil1:
    break;
case Upgrades.Oil2:
    break;
case Upgrades.Oil3:
    break;
case Upgrades.Nuclear1:
    break;
case Upgrades.Nuclear2:
    break;
case Upgrades.Nuclear3:
    break;
*/
