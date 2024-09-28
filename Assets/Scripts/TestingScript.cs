using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public ResearchManager researchManager;
    public BuildingController buildingController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(buildingController.GetCostToBuild(BuildingController.BuildingType.SOLAR_PANEL));
        }
    }
}
