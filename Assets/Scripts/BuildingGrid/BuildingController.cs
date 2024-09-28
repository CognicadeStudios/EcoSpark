using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public enum BuildingType : int {
        NONE,
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
        return currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position, buildingRotation, transform);
    }
    public void DestroyBuilding()
    {
        Destroy(currentBuilding);
        this.buildingType = BuildingType.NONE;
    }
}
