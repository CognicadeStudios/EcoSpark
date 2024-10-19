using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public LightingManager lightingManager;
    public ResearchManager researchManager;
    public List<GameObject> buildingPrefabs;
    public BuildingType buildingType;
    public Vector2Int gridPosition;
    public GameObject currentBuilding;    
    public GameObject redPlane, redPlanePrefab;
    public bool displayingRedPlane;
    public bool isBuildingMode;
    
    void Start() 
    {
        currentBuilding = null;
        displayingRedPlane = false;
        isBuildingMode = true;
    }

    void Update()
    { 
        if(GridController.Instance.isBuilding && buildingType == BuildingType.Empty && !displayingRedPlane)
        {
            displayingRedPlane = true;
            redPlane = Instantiate(redPlanePrefab, transform.position + redPlanePrefab.transform.position, redPlanePrefab.transform.rotation, transform);
        }
        
        if(displayingRedPlane && !(GridController.Instance.isBuilding && buildingType == BuildingType.Empty))
        {
            displayingRedPlane = false;
            Destroy(redPlane);
        }

        if(isBuildingMode) return;
    }

    /// <summary>
    /// Builds the specified building type on this building controller.
    /// If there is already a building on this controller, it will be destroyed first.
    /// </summary>
    /// <param name="buildingType">Type of building to build.</param>
    /// <returns>The newly built building.</returns>
    public GameObject BuildBuilding(BuildingType buildingType)
    {
        if(this.currentBuilding != null)
        {
            DestroyBuilding();
        }

        this.buildingType = buildingType;
        OnBuild(buildingType);
        //I have no Idea how this works or how to implement logic in this so
        GameObject buildprefab = buildingPrefabs[(int)buildingType];
        return currentBuilding = Instantiate(buildprefab, transform.position + buildprefab.transform.position, buildprefab.transform.rotation, transform);
    }
    public void DestroyBuilding()
    {
        Destroy(currentBuilding);
        this.buildingType = BuildingType.Empty;
    }
    
    public void OnBuild(BuildingType type)
    {
        
    }

    public static int GetCostToBuild(BuildingType type)
    {
        int level = BuildingInfo.LevelOf[type];
        return BuildingInfo.BuildCosts[type][level > 0? level : 1];
    }

    public static void HourlyValueUpdates(object sender, LightingManager.HourChangedArgs e)
    {
        Cost total = new Cost();
        foreach (KeyValuePair<BuildingType, int> entry in BuildingInfo.NumberBuilt)
        {
            Debug.Log(entry);
            if (entry.Key < BuildingType.Crossroad)
            {
                total += entry.Value * BuildingInfo.Productions[entry.Key][BuildingInfo.LevelOf[entry.Key]];
            }
        }
        GameManager.Instance.Transaction(total);
        Debug.Log("net productions of buildings: " + total);
    }
    
    
}

public class BuildingInfo
{
    public static Dictionary<BuildingType, List<int>> BuildCosts = new()
    {
        { BuildingType.HOUSE,            new(){0, 35 , 45 , 55 } },
        { BuildingType.BUSINESS,         new(){0, 50 , 50 , 60 } },
        { BuildingType.SOLAR_PANEL,      new(){0, 120, 0  , 0  } },
        { BuildingType.WIND_TURBINE ,    new(){0, 100, 95 , 85 } },
        { BuildingType.GEOTHERMAL_PLANT, new(){0, 150, 120, 0  } },
        { BuildingType.WATER_TURBINE,    new(){0, 120, 140, 130} },
        { BuildingType.NUCLEAR_PLANT,    new(){0, 200, 185, 160} },
        { BuildingType.COAL_MINE,        new(){0, 70 , 90 , 110} },
        { BuildingType.ENERGY_STORAGE,   new(){0, 50 , 60 , 70 } },
        { BuildingType.RESEARCH_LAB,     new(){0, 30 , 65 , 90 } },
    };
    public static Dictionary<BuildingType, List<Cost>> Productions = new()
    {
        {BuildingType.Empty, new(){
            new(0, 0, 0, 0, 0),
            new(0, 0, 0, 0, 0),
            new(0, 0, 0, 0, 0),
            new(0, 0, 0, 0, 0),
        }},
        {BuildingType.HOUSE, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.2f, 0.1f, 20, -0.5f),
            new(0, 0.3f, 0.2f, 50, -1),
            new(0, 0.4f, 0.3f, 100, -1.5f),
        }},
        {BuildingType.BUSINESS, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.1f, 0.1f, 100, -1),
            new(0, 0.2f, 0.2f, 250, -2),
            new(0, 0.3f, 0.3f, 500, -3),
        }},
        {BuildingType.SOLAR_PANEL, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.3f, 0.4f, 0, 10),
            new(0, 0, 0, 0, 0),
            new(0, 0, 0, 0, 0),
        }},
        {BuildingType.WIND_TURBINE, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.2f, 0.2f, 0, 3),
            new(0, 0.3f, 0.3f, 0, 7),
            new(0, 0.4f, 0.4f, 0, 12),
        }},
        {BuildingType.GEOTHERMAL_PLANT, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.2f, 0.3f, 0, 15),
            new(0, 0.3f, 0.4f, 0, 25),
            new(0, 0, 0, 0, 0),
        }},
        {BuildingType.WATER_TURBINE, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.2f, 0.3f, 0, 10),
            new(0, 0.3f, 0.4f, 0, 20),
            new(0, 0.4f, 0.5f, 0, 40),
        }},
        {BuildingType.NUCLEAR_PLANT, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, -0.1f, -0.3f, 0, 30),
            new(0, -0.2f, -0.4f, 0, 60),
            new(0, -0.3f, -0.5f, 0, 90),
        }},
        {BuildingType.COAL_MINE, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, -0.2f, -0.5f, 0, 2.5f),
            new(0, -0.3f, -0.6f, 0, 10),
            new(0, -0.4f, -0.7f, 0, 45),
        }},
        {BuildingType.ENERGY_STORAGE, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(0, 0.1f, 0.1f, 0, 20),
            new(0, 0.2f, 0.2f, 0, 40),
            new(0, 0.3f, 0.3f, 0, 80),
        }},
        {BuildingType.RESEARCH_LAB, new List<Cost>{
            new(0, 0, 0, 0, 0),
            new(2, 0.2f, 0.1f, 0, -5),
            new(4, 0.3f, 0.2f, 0, -10),
            new(6, 0.4f, 0.3f, 0, -15),
        }},

    };
    public static Dictionary<BuildingType, int> NumberBuilt = new(){
        {BuildingType.Empty, 0},
        {BuildingType.HOUSE, 0},
        {BuildingType.BUSINESS, 0},
        {BuildingType.SOLAR_PANEL, 0},
        {BuildingType.WIND_TURBINE, 0},
        {BuildingType.GEOTHERMAL_PLANT, 0},
        {BuildingType.WATER_TURBINE, 0},
        {BuildingType.NUCLEAR_PLANT, 0},
        {BuildingType.COAL_MINE, 0},
        {BuildingType.ENERGY_STORAGE, 0},
        {BuildingType.RESEARCH_LAB, 0},
        {BuildingType.Crossroad, 0},
        {BuildingType.StraightX, 0},
        {BuildingType.StraightY, 0},
        {BuildingType.RoadNE, 0},
        {BuildingType.RoadNW, 0},
        {BuildingType.RoadSE, 0},
        {BuildingType.RoadSW, 0},
    };
    public static Dictionary<BuildingType, int> LevelOf = new()
    {
        { BuildingType.Empty, 0},
        { BuildingType.HOUSE, 0},
        { BuildingType.BUSINESS, 0},
        { BuildingType.SOLAR_PANEL, 0},
        { BuildingType.WIND_TURBINE, 0},
        { BuildingType.GEOTHERMAL_PLANT, 0},
        { BuildingType.WATER_TURBINE, 0},
        { BuildingType.NUCLEAR_PLANT, 0},
        { BuildingType.COAL_MINE, 0},
        { BuildingType.ENERGY_STORAGE, 0},
        { BuildingType.RESEARCH_LAB, 0},
    };
}

public enum BuildingType : int
{
    Empty,
    HOUSE,
    BUSINESS,
    SOLAR_PANEL,
    WIND_TURBINE,
    GEOTHERMAL_PLANT,
    WATER_TURBINE,
    NUCLEAR_PLANT,
    COAL_MINE,
    ENERGY_STORAGE,
    RESEARCH_LAB,
    Crossroad,
    StraightX,
    StraightY,
    RoadNE,
    RoadNW,
    RoadSE,
    RoadSW,
};

/*
todo:
1. add mechanics for other buildings lol
*/