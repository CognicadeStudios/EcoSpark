using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public enum BuildingType : int {
        TOWN_HALL = 1,
        SOLAR_PANEL = 2,
        WIND_TURBINE = 4,
        WATER_TURBINE = 8,
        NUCLEAR_PLANT = 16,
        OIL_DRILL = 32, 
        COAL_FACTORY = 64,
        RENEWABLE_ENERGY = SOLAR_PANEL | WIND_TURBINE | WATER_TURBINE,
        NON_RENEWABLE_ENERGY = NUCLEAR_PLANT | OIL_DRILL | COAL_FACTORY,
    };
    public BuildingType buildingType;
    public TextMesh textMesh;
    public Vector2Int gridPosition;
    void Start() 
    {
        textMesh.text = "0";
    }

    public void Test() 
    {
        Debug.Log(buildingType);
        textMesh.text = "1";
    }
}
