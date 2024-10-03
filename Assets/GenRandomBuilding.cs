using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenRandomBuilding : MonoBehaviour
{
    static int counter = 0;
    void Start()
    {
        int numChildren = transform.childCount;
        int randomChild = counter % numChildren;
        transform.GetChild(randomChild).gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0)) counter++;
    }
}
