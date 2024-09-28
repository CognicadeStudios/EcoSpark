using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public List<Upgrades> researchedUpgrades;
    public List<Upgrades> unlockableUpgrades;
    private Dictionary<Upgrades, List<Upgrades>> nextUnlocks;

    public ResearchManager()
    {
        researchedUpgrades = new List<Upgrades>();

        unlockableUpgrades = new List<Upgrades> { Upgrades.SolarLevel1 };


        nextUnlocks = new Dictionary<Upgrades, List<Upgrades>> {
            //for each upgrade, what other upgrades should it unlock?
            { Upgrades.SolarLevel1, new List<Upgrades> { Upgrades.SolarLevel2 } },
            { Upgrades.HydroLevel1, new List<Upgrades> { Upgrades.HydroLevel2 }},
            { Upgrades.NuclearLevel1, new List<Upgrades> { Upgrades.NuclearLevel2 }},
            { Upgrades.WindLevel1, new List<Upgrades> { Upgrades.WindLevel2 }},
        };
        
        
    }
    public void UnlockUpgrade(Upgrades upgrade)
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
        researchedUpgrades.Add(upgrade);
        Debug.Log("Upgrade Researched");
        
        foreach (Upgrades u in nextUnlocks[upgrade])
        {
            if (!unlockableUpgrades.Contains(u))
            {
                unlockableUpgrades.Add(u);
            }
        }
    }

    public bool IsUpgradeUnlocked(Upgrades upgrade)
    {
        return researchedUpgrades.Contains(upgrade);
    }

    public bool BuildingUnlocked(BuildingController.BuildingType buildingType)
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
                return IsUpgradeUnlocked(Upgrades.SolarLevel1);
            case BuildingController.BuildingType.WIND_TURBINE:
                return IsUpgradeUnlocked(Upgrades.WindLevel1);
            case BuildingController.BuildingType.WATER_TURBINE:
                return IsUpgradeUnlocked(Upgrades.HydroLevel1);
            case BuildingController.BuildingType.NUCLEAR_PLANT:
                return IsUpgradeUnlocked(Upgrades.NuclearLevel1);
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

