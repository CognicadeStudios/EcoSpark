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
        currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position, Quaternion.identity);
    }
    public void BuildBuilding(BuildingType buildingType, Quaternion buildingRotation)
    {
        this.buildingType = buildingType;
        currentBuilding = Instantiate(buildingPrefabs[(int)buildingType], transform.position, buildingRotation);
    }
}
