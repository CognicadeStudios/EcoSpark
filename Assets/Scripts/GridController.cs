using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridController : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public float gridScale;
    public Vector3 gridOffset;
    public BuildingController [,] buildingsGrid;
    public GameObject buildingPrefab;
    public static GridController instance;
    public void Awake()
    {
        instance = this;
        buildingsGrid = new BuildingController[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                buildingsGrid[x, y] = Instantiate(buildingPrefab, GetWorldPosition(new Vector2Int(x, y)), buildingPrefab.transform.rotation).GetComponent<BuildingController>();
                buildingsGrid[x, y].gridPosition = new Vector2Int(x, y);
                buildingsGrid[x, y].GetComponent<BoxCollider>().size = new Vector3(gridScale, gridScale, 1);
            }
        }
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

    public void SetBuilding(int x, int y, BuildingController.BuildingType buildingType, Quaternion buildingRotation)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            buildingsGrid[x, y].BuildBuilding(buildingType, buildingRotation);
        }
        else
        {
            Debug.Log("SetGridObject: Grid position out of bounds: " + x + ", " + y);
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

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot a raycase form the mouse position and get the hit postion
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                //draw the raycase as debug.drawray
                Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hit.point - Camera.main.ScreenPointToRay(Input.mousePosition).origin, Color.red, 5);
                Vector3 worldPosition = hit.point;
                Vector2Int gridPosition = GetGridPosition(worldPosition);
                SetBuilding(gridPosition.x, gridPosition.y, BuildingController.BuildingType.TOWN_HALL, Quaternion.identity);
            }
        }
    }
}