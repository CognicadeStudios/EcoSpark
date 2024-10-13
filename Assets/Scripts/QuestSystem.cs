using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public float minInterval = 0.5f;
    public float maxInterval = 1.0f;

    private List<QuestGoal> AvailableTasks;
    private List<QuestGoal> CurrentTask; 

    void Start()
    {
        CurrentTask = new List<QuestGoal>();
        AvailableTasks = new List<QuestGoal>();
        PopulateAvailableTasks();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (QuestGoal ts in AvailableTasks)
        {
            if (ts.Completed)
            {
                CurrentTask.Remove(ts);
            }
        }
    }
    public void AssignTaskForDialogue(int key)
    {
        //Assign Task to CurentTask based on the key

    }

    public void PopulateAvailableTasks()
    {

    }

}
