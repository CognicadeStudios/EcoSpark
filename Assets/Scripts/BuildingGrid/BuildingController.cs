using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public ResearchManager researchManager;
    public enum BuildingType : int {
        Empty,
        Crossroad,
        StraightX,
        StraightY,
        RoadNE,
        RoadNW,
        RoadSE,
        RoadSW,
        HOUSE,
        BUSINESS,
        SOLAR_PANEL,
        WIND_TURBINE,
        WATER_TURBINE,
        NUCLEAR_PLANT,
        COAL_MINE,
        OIL_DRILL, 
        ENERGY_STORAGE,
        RESEARCH_LAB,
        
    };

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

    public GameObject BuildBuilding(BuildingType buildingType)
    {
        if(this.currentBuilding != null)
        {
            DestroyBuilding();
        }

        this.buildingType = buildingType;
        OnBuild(buildingType);
        //I have no Idea how this works or how to implement logic in this so
        return currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position + buildingPrefabs[(int)buildingType].transform.position, buildingPrefabs[(int)buildingType].transform.rotation, transform);
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
    public static Dictionary<BuildingController.BuildingType, List<int>> BuildCosts = new()
    {

    };
}

/*
todo:
1. add mechanics for other buildings lol
*/