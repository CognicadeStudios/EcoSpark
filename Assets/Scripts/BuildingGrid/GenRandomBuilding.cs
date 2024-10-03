using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenRandomBuilding : MonoBehaviour
{
    void Start()
    {
        int numChildren = transform.childCount;
        int randomChild = Random.Range(0, numChildren);
        transform.GetChild(randomChild).gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
