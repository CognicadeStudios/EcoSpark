using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenRandomBuilding : MonoBehaviour
{
    public bool isBuilding;
    void Start()
    {
        Transform toChild = transform;
        if(isBuilding)
        {
            Transform grandpa = transform.parent.parent;
            var position = grandpa.GetComponent<BuildingController>().gridPosition;
            var centerPosition = new Vector2(GridController.instance.gridWidth / 2, GridController.instance.gridHeight / 2);
            var dist = Vector2.Distance(centerPosition, position);

            if(dist > 7.0f) 
            {
                return;
            }
            if(dist > 4.0f && UnityEngine.Random.Range(0.0f, 1.0f) < 0.8)
            {
                toChild = transform.GetChild(1);
            }
            else 
            {
                toChild = transform.GetChild(0);
            }
        }

        int numChildren = toChild.childCount;
        int randomChild = Random.Range(0, numChildren);
        toChild.GetChild(randomChild).gameObject.SetActive(true);

        if(isBuilding)
        {
            BuildingController controller = transform.parent.parent.GetComponent<BuildingController>();
            controller.buildingType = BuildingController.BuildingType.HOUSE;
        }
    }

    void Update()
    {
    
    }
}
