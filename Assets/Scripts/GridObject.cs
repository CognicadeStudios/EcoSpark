using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridObject
{
    int data;
    TextMeshPro textMeshPro;
    public GridObject(int data)
    {
        this.data = data;
    }

    public int getData()
    {
        return data;
    }

    public void setData(int data)
    {
        this.data = data;
    }
}
