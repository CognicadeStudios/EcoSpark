using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResearchPointHolder : MonoBehaviour
{
    public int rps;
    public int ResearchPoints
    {
        get
        {
            return rps;
        }
        set
        {
            rps = value;
            Debug.Log("Research Points: " + rps);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
