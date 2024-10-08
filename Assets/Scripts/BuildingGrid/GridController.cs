using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.Mathematics;

public class GridController : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public float gridScale;
    public Vector3 gridOffset;
    public BuildingController [,] buildingsGrid;
    public GameObject buildingPrefab;
    public ProdecuralGenerator generator;
    public static GridController instance;
    public void Awake()
    {
        instance = this;
        buildingsGrid = new BuildingController[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                buildingsGrid[x, y] = Instantiate(buildingPrefab, GetWorldPosition(new Vector2Int(x, y)), buildingPrefab.transform.rotation, transform).GetComponent<BuildingController>();
                buildingsGrid[x, y].gridPosition = new Vector2Int(x, y);
                buildingsGrid[x, y].GetComponent<BoxCollider>().size = new Vector3(gridScale, gridScale, 1);
            }
        }

        generator = GetComponent<ProdecuralGenerator>();
        generator.Initialize(gridWidth);
    }

    public BuildingController GetBuilding(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            return buildingsGrid[x, y];
        }
        else
        {
            Debug.Log("GetGridObject: Grid position out of bounds: " + x + ", " + y);
            return null;
        }
    }

    public GameObject SetBuilding(int x, int y, BuildingController.BuildingType buildingType)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            buildingsGrid[x, y].DestroyBuilding();
            return buildingsGrid[x, y].BuildBuilding(buildingType);
        }
        else
        {
            Debug.Log("SetBuildingTest: Grid position out of bounds: " + x + ", " + y);
            return null;
        }
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt((worldPosition.x - gridOffset.x) / gridScale), Mathf.RoundToInt((worldPosition.z - gridOffset.y) / gridScale));
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * gridScale, 0, gridPosition.y * gridScale) + gridOffset;
    }
    public bool isBuilding = false;
    public Vector2Int buildingPreviewPosition;
    BuildingController.BuildingType currentBuildingType;

    public void EnableBuildingMode(int buildingType)
    {
        //check if we can buy this type of building first lol

        int cost = BuildingController.GetCostToBuild((BuildingController.BuildingType)buildingType) ;
        if(cost <= GameManager.Instance.Money)
        {
            GameManager.Instance.Money -= cost;
        }
        else 
        {
            Debug.Log("Not Enough Money to Build Building: " + buildingType);
            return;
        }

        currentBuildingType = (BuildingController.BuildingType)buildingType;
        Debug.Log("Starting Building: " + buildingType);
        isBuilding = true;
    }
    
    public void Update()
    {
        generator.GenerateNextTile();

        if(isBuilding)
        {
            RaycastHit hit;
            if(!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                return;
            }
            // Draw a ray from the camera to the world position
            Debug.DrawRay(Camera.main.transform.position, hit.point - Camera.main.transform.position, Color.red);

            Vector3 worldPosition = hit.point;
            Vector2Int gridPosition = GetGridPosition(worldPosition);
            
            if (!(gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridWidth && gridPosition.y < gridHeight))
            {
                return;
            }

            if(Input.GetMouseButtonDown(0))
            {
                isBuilding = false;
                Debug.Log("Ending Building: " + currentBuildingType);
                buildingPreviewPosition = new Vector2Int(-1, -1);
                SetBuilding(gridPosition.x, gridPosition.y, currentBuildingType);
                buildingsGrid[gridPosition.x, gridPosition.y].isBuildingMode = false;
            }
            
            if(buildingsGrid[gridPosition.x, gridPosition.y].buildingType == BuildingController.BuildingType.Empty && buildingPreviewPosition != gridPosition)
            {
                SetBuilding(buildingPreviewPosition.x, buildingPreviewPosition.y, BuildingController.BuildingType.Empty);
                buildingPreviewPosition = gridPosition;
                GameObject g = SetBuilding(gridPosition.x, gridPosition.y, currentBuildingType);
                for(int i = 0; i < g.transform.childCount; i++)
                {
                    g.transform.GetChild(i).GetComponent<MeshRenderer>().material.color = new Color(1.5f, 1.5f, 1.5f, 0.5f);
                }
            }
        }
    }
}