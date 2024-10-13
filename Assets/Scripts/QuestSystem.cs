using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public float minInterval = 0.5f;
    public float maxInterval = 1.0f;

    private List<QuestGoal> AvailableTasks;
    private List<QuestGoal> CurrentTask;
    private GridController gridController;

    void Start()
    {
        CurrentTask = new List<QuestGoal>();
        AvailableTasks = new List<QuestGoal>();
        gridController = FindObjectOfType<GridController>();
        PopulateAvailableTasks();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (QuestGoal ts in AvailableTasks)
        {
            ts.Evaluate();
            if (ts.Completed)
            {
                CurrentTask.Remove(ts);
            }
        }
    }
    public void AssignTaskForDialogue(int key)
    {
        //Assign Task to CurentTask based on the key
        CurrentTask.Add(AvailableTasks[key]);
        AvailableTasks.RemoveAt(key);

    }

    public void PopulateAvailableTasks()
    {
        AvailableTasks.Add(new QuestGoal(2, false, this.gridController.BuildingsBuilt[2], this.gridController.BuildingsBuilt[2] + 2, 5, 10, 20, "Build"));
        AvailableTasks.Add(new QuestGoal(8, false, 50, 20, 30, "Upgrade"));
    }

}
