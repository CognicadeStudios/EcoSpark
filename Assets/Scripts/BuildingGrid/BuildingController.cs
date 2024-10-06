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
        RoadU,
        RoadD,
        RoadR,
        RoadL,
        RoadNE,
        RoadNW,
        RoadSE,
        RoadSW,
        HOUSE,
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
    public GameObject redPlane, redPlanePrefab;
    public bool displayingRedPlane;
    
    void Start() 
    {
        currentBuilding = null;
        displayingRedPlane = false;
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

        //Do building specific stuff...
        switch(buildingType)
        {
            case BuildingType.SOLAR_PANEL:
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
        return type switch
        {
            BuildingType.HOUSE => 10,
            BuildingType.SOLAR_PANEL => (researchManager.IsUpgradeResearched(Upgrade.Geothermal1)) ? 10 : 20,
            BuildingType.NUCLEAR_PLANT => 50,
            BuildingType.WIND_TURBINE => 30,
            _ => 0,
        };
    }
}
