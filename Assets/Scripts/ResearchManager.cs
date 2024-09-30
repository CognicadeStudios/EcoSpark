using CodeMonkey;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public List<Upgrades> researchedUpgrades;
    public List<Upgrades> unlockableUpgrades;
    public Dictionary<Upgrades, List<Upgrades>> nextUnlocks;

    public event EventHandler<OnUpgradeResearchedArgs> OnUpgradeResearched;
    public class OnUpgradeResearchedArgs : EventArgs
    {
        public Upgrades upgrade;
    }

    public UI_SkillTree skillTree;

    void Start()
    {
        researchedUpgrades = new List<Upgrades>();

        unlockableUpgrades = new List<Upgrades>();
        unlockableUpgrades.Add(Upgrades.SolarLevel1);


        nextUnlocks = new Dictionary<Upgrades, List<Upgrades>> {
            //for each upgrade, what other upgrades should it unlock?
            { Upgrades.SolarLevel1, new List<Upgrades> { Upgrades.SolarLevel2, Upgrades.HydroLevel1 } },
            { Upgrades.HydroLevel1, new List<Upgrades> { Upgrades.HydroLevel2 }},
            { Upgrades.NuclearLevel1, new List<Upgrades> { Upgrades.NuclearLevel2 }},
            { Upgrades.WindLevel1, new List<Upgrades> { Upgrades.WindLevel2 }},
        };

        OnUpgradeResearched += ResearchManager_OnUpgradeResearched;
        skillTree.Initialize();
    }

    
    public void TryUnlockUpgrade(Upgrades upgrade)
    {
        if (researchedUpgrades.Contains(upgrade))
        {
            Debug.Log("Upgrade already reserached");
            return;
        }
        if (!unlockableUpgrades.Contains(upgrade))
        {
            Debug.Log("Upgrade Not Unlocked Yet");
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
        
        researchedUpgrades.Add(upgrade);
        unlockableUpgrades.Remove(upgrade);
        
        if (nextUnlocks.ContainsKey(upgrade)) { 
            foreach (Upgrades u in nextUnlocks[upgrade])
            {
                if (!unlockableUpgrades.Contains(u))
                {
                    unlockableUpgrades.Add(u);
                }
            }
        }

        OnUpgradeResearched?.Invoke(this, new OnUpgradeResearchedArgs { upgrade = upgrade });
        Debug.Log("Upgrade Researched");
    }

    private void ResearchManager_OnUpgradeResearched(object sender, OnUpgradeResearchedArgs e)
    {
        switch (e.upgrade)
        {
            case Upgrades.SolarLevel1:
                Debug.Log("Solar Level 1 Unlocked");
                break;
            case Upgrades.SolarLevel2:
                Debug.Log("Solar Level 2 Unlocked");
                break;
        }
    }

    public bool IsUpgradeResearched(Upgrades upgrade)
    {
        return researchedUpgrades.Contains(upgrade);
    }
    public bool IsUpgradeResearchable(Upgrades upgrade)
    {
        return unlockableUpgrades.Contains(upgrade);
    }
    private int CostToResearch(Upgrades upgrade)
    {
        switch (upgrade)
        {
            case Upgrades.SolarLevel1:
                return 3;
            case Upgrades.SolarLevel2:
                return 5;
            case Upgrades.HydroLevel1:
                return 4;
        }
        Debug.Log("Unimplemented Cost");
        return 0;
    }

    public bool IsBuildingUnlocked(BuildingController.BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingController.BuildingType.NONE:
            case BuildingController.BuildingType.TOWN_HALL:
            case BuildingController.BuildingType.OIL_DRILL:
            case BuildingController.BuildingType.COAL_FACTORY:
            case BuildingController.BuildingType.HOUSE:
                return true;
            case BuildingController.BuildingType.SOLAR_PANEL:
                return IsUpgradeResearched(Upgrades.SolarLevel1);
            case BuildingController.BuildingType.WIND_TURBINE:
                return IsUpgradeResearched(Upgrades.WindLevel1);
            case BuildingController.BuildingType.WATER_TURBINE:
                return IsUpgradeResearched(Upgrades.HydroLevel1);
            case BuildingController.BuildingType.NUCLEAR_PLANT:
                return IsUpgradeResearched(Upgrades.NuclearLevel1);
        }
        return false;
    }
}

public enum Upgrades
{
    SolarLevel1,
    SolarLevel2,
    WindLevel1,
    WindLevel2,
    HydroLevel1,
    HydroLevel2,
    NuclearLevel1,
    NuclearLevel2,
};

