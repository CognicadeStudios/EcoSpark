using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
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
        if(GridController.instance.isBuilding && buildingType == BuildingType.Empty && !displayingRedPlane)
        {
            displayingRedPlane = true;
            redPlane = Instantiate(redPlanePrefab, transform.position + redPlanePrefab.transform.position, redPlanePrefab.transform.rotation, transform);
        }
        
        if(displayingRedPlane && !(GridController.instance.isBuilding && buildingType == BuildingType.Empty))
        {
            displayingRedPlane = false;
            Destroy(redPlane);
        }

        if(isBuildingMode) return;

        //Do building specific stuff...
        switch(buildingType)
        {
            case BuildingType.SOLAR_PANEL:
                float solarSpeed = 100.0f;
                if(LightingManager.instance.isNight) solarSpeed *= 0.2f;
                GameManager.Instance.CityEnergy += Time.deltaTime * solarSpeed;
                break;
            case BuildingType.HOUSE:
                float rand = Random.Range(0.0f, 0.5f) * Time.deltaTime * (GameManager.Instance.PublicApproval/100.0f);
                GameManager.Instance.Money += rand;
                break;
            case BuildingType.WIND_TURBINE:
                float windSpeed = 10.0f + Random.Range(-10.0f, 10.0f);
                GameManager.Instance.CityEnergy += Time.deltaTime * windSpeed;
                break;
            case BuildingType.WATER_TURBINE:
                float waterSpeed = 15.0f;
                GameManager.Instance.CityEnergy += Time.deltaTime * waterSpeed;
                break;
            case BuildingType.NUCLEAR_PLANT:
                float nuclearPlantSpeed = 15.0f;
                GameManager.Instance.CityEnergy += Time.deltaTime * nuclearPlantSpeed;
                break;
            //case BuildingType.OIL_DRILL:
            //    float oilSpeed = 15.0f;
            //    GameManager.Instance.CityEnergy += Time.deltaTime * oilSpeed;
            //    break;
            case BuildingType.COAL_MINE:
                float coalFactorySpeed = 15.0f;
                GameManager.Instance.CityEnergy += Time.deltaTime * coalFactorySpeed;
                break;
        }
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
        switch(type)
        {
            case BuildingType.Empty:
            case BuildingType.HOUSE:
            case BuildingType.SOLAR_PANEL:
            case BuildingType.NUCLEAR_PLANT:
            case BuildingType.COAL_MINE:
            case BuildingType.WATER_TURBINE:
            case BuildingType.WIND_TURBINE:
                break;
        }
    }

    public static int GetCostToBuild(BuildingType type)
    {
        //money?
        return type switch
        {
            BuildingType.HOUSE => 35,
            BuildingType.SOLAR_PANEL => 120, //ResearchManager.Instance.IsUpgradeResearched(Upgrade.Geothermal1) ? 10 : 20,
            BuildingType.WIND_TURBINE => 100,
            BuildingType.WATER_TURBINE => 175,
            BuildingType.NUCLEAR_PLANT => 200,
            //BuildingType.OIL_DRILL => 50,
            BuildingType.COAL_MINE => 65,
            _ => 0,
        };
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
    public static Dictionary<BuildingType, List<Cost>> productions = new()
    {

    };
    public static Dictionary<BuildingType, List<Cost>> consumptions = new()
    {

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