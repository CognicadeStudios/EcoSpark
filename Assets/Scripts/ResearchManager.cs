using CodeMonkey;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public List<Upgrades> researchedUpgrades;
    public List<Upgrades> /*!TODO*/ unlockableUpgrades;
    public Dictionary<Upgrades, List<Upgrades>> UnlockRequirements;

    public event EventHandler<OnUpgradeResearchedArgs> OnUpgradeResearched;
    public class OnUpgradeResearchedArgs : EventArgs
    {
        public Upgrades upgrade;
    }

    public UI_SkillTree skillTree;

    void Start()
    {
        researchedUpgrades = new List<Upgrades>();

        UnlockRequirements = new Dictionary<Upgrades, List<Upgrades>> {
            {Upgrades.SolarLevel2, new List<Upgrades>{Upgrades.SolarLevel1} }
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
        researchedUpgrades.Add(upgrade);
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
        if (!UnlockRequirements.ContainsKey(upgrade))
        {
            return true;
        }
        bool unlockable = true;
        foreach (Upgrades req in UnlockRequirements[upgrade])
        {
            unlockable = unlockable && researchedUpgrades.Contains(req);
            if (!unlockable)
            {
                return false;
            }
        }
        return true;
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
    ResearchLab,
    SolarLevel1,
    SolarLevel2,
    WindLevel1,
    WindLevel2,
    HydroLevel1,
    HydroLevel2,
    NuclearLevel1,
    NuclearLevel2,
};

