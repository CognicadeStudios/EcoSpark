using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.Mathematics;

public class GridController : MonoBehaviour
{
    public LightingManager lightingManager;
    public Material previewMat;
    public int gridWidth, gridHeight;
    public float gridScale;
    public Vector3 gridOffset;
    public BuildingController [,] buildingsGrid;
    public GameObject buildingPrefab;
    public ProdecuralGenerator generator;
    public static GridController Instance;
    public Texture2D normalCursor;
    public void Awake()
    {
        Instance = this;
        buildingsGrid = new BuildingController[gridWidth, gridHeight];
        Cursor.SetCursor(normalCursor, new Vector2(225, 0), CursorMode.Auto);
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                buildingsGrid[x, y] = Instantiate(buildingPrefab, GetWorldPosition(new Vector2Int(x, y)), buildingPrefab.transform.rotation, transform).GetComponent<BuildingController>();
                buildingsGrid[x, y].gridPosition = new Vector2Int(x, y);
                buildingsGrid[x, y].GetComponent<BoxCollider>().size = new Vector3(gridScale, gridScale, 1);
                buildingsGrid[x, y].lightingManager = lightingManager;
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

    public GameObject SetBuilding(int x, int y, BuildingType buildingType)
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

    public Vector2 GetGridPositionNoRound(Vector3 worldPosition)
    {
        return new Vector2((worldPosition.x - gridOffset.x) / gridScale, (worldPosition.z - gridOffset.y) / gridScale);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * gridScale, 0, gridPosition.y * gridScale) + gridOffset;
    }
    public bool isBuilding = false;
    public Vector2Int buildingPreviewPosition;
    public Vector2 mousePosition;
    BuildingType currentBuildingType;

    public void EnableBuildingMode(int buildingType)
    {
        //check if we can buy this type of building first lol

        int cost = BuildingController.GetCostToBuild((BuildingType)buildingType) ;
        if(cost <= GameManager.Instance.Money)
        {
            GameManager.Instance.Money -= cost;
        }
        else 
        {
            Debug.Log("Not Enough Money to Build Building: " + buildingType);
            return;
        }

        currentBuildingType = (BuildingType)buildingType;
        Debug.Log("Starting Building: " + buildingType);
        isBuilding = true;
    }
    
    public void Update()
    {
        generator.GenerateNextTile();

        if(isBuilding)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isBuilding = false;
                Debug.Log("Ending Building: " + currentBuildingType);
                SetBuilding(buildingPreviewPosition.x, buildingPreviewPosition.y, BuildingType.Empty);
                buildingPreviewPosition = new Vector2Int(-1, -1);
            }

            RaycastHit hit;
            if(!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                return;
            }
            // Draw a ray from the camera to the world position
            Debug.DrawRay(Camera.main.transform.position, hit.point - Camera.main.transform.position, Color.red);

            Vector3 worldPosition = hit.point;
            Vector2Int gridPosition = GetGridPosition(worldPosition);
            mousePosition = GetGridPositionNoRound(worldPosition);
            
            if (!(gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < gridWidth && gridPosition.y < gridHeight))
            {
                return;
            }

            if(Input.GetMouseButtonDown(0))
            {
                //we gotta implement a check to see if we can place it here
                if(buildingsGrid[gridPosition.x, gridPosition.y].buildingType != currentBuildingType)
                {
                    Debug.Log("Cannot Place Building: " + buildingsGrid[gridPosition.x, gridPosition.y].buildingType );
                    return;
                }

                isBuilding = false;
                Debug.Log("Completing Building: " + currentBuildingType);
                buildingPreviewPosition = new Vector2Int(-1, -1);
                SetBuilding(gridPosition.x, gridPosition.y, currentBuildingType);
                buildingsGrid[gridPosition.x, gridPosition.y].isBuildingMode = false;
                BuildingInfo.NumberBuilt[currentBuildingType]++;
            }
            
            if(buildingsGrid[gridPosition.x, gridPosition.y].buildingType == BuildingType.Empty && buildingPreviewPosition != gridPosition)
            {
                SetBuilding(buildingPreviewPosition.x, buildingPreviewPosition.y, BuildingType.Empty);
                buildingPreviewPosition = gridPosition;
                GameObject g = SetBuilding(gridPosition.x, gridPosition.y, currentBuildingType);
            }
        }
    }
}