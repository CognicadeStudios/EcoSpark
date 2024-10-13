using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{

    private int probabilityOfTask;
    public List<QuestGoal> AvailableTasks;
    public List<QuestGoal> CurrentTask;
    private GridController gridController;
    private int timeElapsed;
    private int baseProbability = 20;
    private int timeThreshold = 10;
    private LightingManager lightManager;

    void Start()
    {
        CurrentTask = new List<QuestGoal>();
        AvailableTasks = new List<QuestGoal>();
        gridController = FindObjectOfType<GridController>();
        PopulateAvailableTasks();
        probabilityOfTask = 0;
        timeElapsed = 0;
        lightManager = FindObjectOfType<LightingManager>();


    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed = lightManager.timeTracker;
        if(timeElapsed > timeThreshold)
        {
            probabilityOfTask += (timeElapsed / timeThreshold) * 10;
            if(probabilityOfTask > 100)
            {
                probabilityOfTask = 100;
            }
        }
        foreach (QuestGoal ts in AvailableTasks)
        {
            ts.Evaluate();
            if (ts.Completed)
            {
                CurrentTask.Remove(ts);
            }
        }
    }
    public bool AssignTaskForDialogue(int key)
    {   //Assign Task to CurentTask based on the key
        int val = Random.Range(0, 100);
        if (val < probabilityOfTask)
        {
            CurrentTask.Add(AvailableTasks[key]);
            AvailableTasks.RemoveAt(key);
            probabilityOfTask = baseProbability;
            return true;
        }
        return false;
    }

    public void PopulateAvailableTasks()
    {
        AvailableTasks.Add(new QuestGoal(2, false, this.gridController.BuildingsBuilt[2], this.gridController.BuildingsBuilt[2] + 2, 5, 10, 20, "Build"));
        AvailableTasks.Add(new QuestGoal(8, false, 50, 20, 30, "Upgrade"));
    }

}
