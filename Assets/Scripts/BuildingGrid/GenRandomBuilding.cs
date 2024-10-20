using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenRandomBuilding : MonoBehaviour
{
    public bool isBuilding;
    public float distFromCenter, currentRadius;
    private static List<GenRandomBuilding> objects;
    void Awake()
    {
        objects ??= new List<GenRandomBuilding>();
    }

    void Start()
    {
        currentRadius = 0.0f;
        if(isBuilding)
        {
            Transform grandpa = transform.parent.parent;
            var position = grandpa.GetComponent<BuildingController>().gridPosition;
            var centerPosition = new Vector2(GridController.Instance.gridWidth / 2, GridController.Instance.gridHeight / 2);
            distFromCenter = Vector2.Distance(centerPosition, position);

            Invoke(nameof(InitialUpdate), 3.0f);
            objects.Add(this);
        }
        else 
        {
            int numChildren = transform.childCount;
            int randomChild = Random.Range(0, numChildren);
            transform.GetChild(randomChild).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Triggers the UpdateBuildingGeneration function for all currently active
    /// GenRandomBuilding objects with the given new radius.
    /// </summary>
    /// <param name="newRadius">The new radius to pass to
    /// UpdateBuildingGeneration.</param>
    public static void TriggerRadiusUpdate(float newRadius)
    {
        foreach(GenRandomBuilding obj in objects)
        {
            obj.UpdateBuildingGeneration(newRadius);
        }
    }

    void InitialUpdate()
    {
        UpdateBuildingGeneration(4.0f);
    }

    void UpdateBuildingGeneration(float newRadius)
    {
        if(!isBuilding)
        {
            Debug.Log("You cannot update a non-building");
            return;
        }
        
        if(distFromCenter < currentRadius || distFromCenter > newRadius) 
        {
            currentRadius = newRadius;
            return;
        }
        
        BuildingController controller = transform.parent.parent.GetComponent<BuildingController>();
        if(controller.buildingType != BuildingType.Empty) return;

        Transform buildingPrefab;
        float rand = Random.Range(0.0f, 1.0f);
        if(rand > 0.4f) buildingPrefab = transform.GetChild(0);
        else buildingPrefab = transform.GetChild(1);
        
        int numChildren = buildingPrefab.childCount;
        int randomChild = Random.Range(0, numChildren);
        buildingPrefab.GetChild(randomChild).gameObject.SetActive(true);

        controller.buildingType = BuildingType.HOUSE;

        currentRadius = newRadius;

        BuildingInfo.NumberBuilt[BuildingType.HOUSE]++;
    }

}
