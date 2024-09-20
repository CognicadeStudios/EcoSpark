using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey.Utils;

public class GridController : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public float gridScale;
    public Vector3 gridOffset;

    private GridObject [,] data;

    public void Awake()
    {
        data = new GridObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                data[x, y] = new GridObject(0);
            }
        }
    }

    public GridObject getGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            return data[x, y];
        }
        else
        {
            Debug.Log("GetGridObject: Grid position out of bounds: " + x + ", " + y);
            return null;
        }
    }

    public void setGridObject(int x, int y, GridObject gridObject)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
        {
            data[x, y] = gridObject;
        }
        else
        {
            Debug.Log("SetGridObject: Grid position out of bounds: " + x + ", " + y);
        }
    }

    public Vector2Int getGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt((worldPosition.x - gridOffset.x) / gridScale), Mathf.FloorToInt((worldPosition.y - gridOffset.y) / gridScale));
    }

    public Vector3 getWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * gridScale, gridPosition.y * gridScale, 0) + gridOffset;
    }
}