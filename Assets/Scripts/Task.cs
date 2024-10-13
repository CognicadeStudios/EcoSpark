using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public int ID { get; set; }

    protected GridController gridController;
    protected ResearchManager researchManager;

    public virtual void Init()
    {
        // default init
        gridController = FindObjectOfType<GridController>();
        researchManager = FindObjectOfType<ResearchManager>();

    }

    public void Complete()
    {
        Completed = true; 
    }


}
