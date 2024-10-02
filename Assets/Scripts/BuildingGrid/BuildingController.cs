using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public ResearchManager researchManager;
    public enum BuildingType : int {
        NONE,
        HOUSE,
        TOWN_HALL,
        SOLAR_PANEL,
        WIND_TURBINE,
        WATER_TURBINE,
        NUCLEAR_PLANT,
        OIL_DRILL, 
        COAL_FACTORY,
    };
    public List<GameObject> buildingPrefabs;
    public BuildingType buildingType;
    public Vector2Int gridPosition;
    public GameObject currentBuilding;
    void Start() 
    {
        currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position, Quaternion.identity, transform);
    }
    public GameObject BuildBuilding(BuildingType buildingType, Quaternion buildingRotation)
    {
        this.buildingType = buildingType;
        OnBuild(buildingType);
        //I have no Idea how this works or how to implement logic in this so
        return currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position, buildingRotation, transform);
    }
    public void DestroyBuilding()
    {
        Destroy(currentBuilding);
        this.buildingType = BuildingType.NONE;
    }
    
    public void OnBuild(BuildingType type)
    {
        switch(type)
        {
            case BuildingType.NONE:
                return;
            case BuildingType.HOUSE:
            case BuildingType.TOWN_HALL:
            case BuildingType.SOLAR_PANEL:
            case BuildingType.NUCLEAR_PLANT:
            case BuildingType.COAL_FACTORY:
            case BuildingType.WATER_TURBINE:
            case BuildingType.OIL_DRILL:
            case BuildingType.WIND_TURBINE:
                break;
        }
    }

    public int GetCostToBuild(BuildingType type)
    {
        //money?
        switch (type)
        {
            case BuildingType.NONE:
                return 0;
            case BuildingType.HOUSE:
                return 10;
            case BuildingType.TOWN_HALL:
                return 100;
            case BuildingType.SOLAR_PANEL:
                return (researchManager.IsUpgradeResearched(Upgrades.Geothermal1)) ? 10 : 20;
            case BuildingType.NUCLEAR_PLANT:
                return 50;
            case BuildingType.WIND_TURBINE:
                return 30;

        }
        return 0;
    }
}
