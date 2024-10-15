using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    //Quest Management
    private float probabilityOfTask;
    public List<QuestGoal> AvailableTasks;
    public List<QuestGoal> CurrentTasks;
    public const float baseProbability = 0.2f;
    public const float timeThreshold = 4.0f;
    private LightingManager lightManager;

    //Misc
    public QuestUI questUI;
    public static QuestSystem instance;
    
    //TODO: Uncomment Animator Commands
    public Animator anim;
    private Queue<string> sentences;
    private QuestSystem taskManager;

    public void Awake()
    {    
        instance = this;
    }

    public void Start()
    {
        CurrentTasks = new List<QuestGoal>();
        AvailableTasks = new List<QuestGoal>();
        
        probabilityOfTask = baseProbability;
        lightManager = LightingManager.instance;

        sentences = new Queue<string>();
        taskManager = FindObjectOfType<QuestSystem>();
        PopulateTaskList();

        StartRandomDialogue();
    }

     private void PopulateTaskList()
    {
        AvailableTasks = new List<QuestGoal>()
        {
            new QuestGoal(
                (int)BuildingType.SOLAR_PANEL,
                "Build", 
                new Cost(0, 10, 0, 10, 0),
                2,
                "Our renewable energy output is lagging behind! "+
                "Build 2 solar panels to help meet our sustainability goals.",
                "Build 2 Level 1 Solar Panels",
                "Glen Youngkin",
                null
            )
        };
    }


    // Update is called once per frame
    public void Update()
    {
        float timeElapsed = lightManager.TimeElapsed;
        if(timeElapsed > timeThreshold)
        {
            probabilityOfTask += timeElapsed / (timeThreshold * 100.0f);
            probabilityOfTask = Mathf.Min(probabilityOfTask, 1.0f);
        }

        for(int i = CurrentTasks.Count - 1; i >= 0; i--)
        {
            QuestGoal ts = CurrentTasks[i];
            ts.Check();
            if (ts.Completed)
            {
                CurrentTasks.RemoveAt(i);
            }
        }
        
        /*float prob = Random.Range(0.0f, 1.0f);
        if (prob < probabilityOfTask)
        {
            AssignTask(Random.Range(0, AvailableTasks.Count));
        }*/
    }

    public void AssignTask(int key)
    {   //Assign Task to CurentTask based on the key
        CurrentTasks.Add(AvailableTasks[key]);
        AvailableTasks.RemoveAt(key);
        probabilityOfTask = baseProbability;
    }

    public void StartRandomDialogue()
    {
        //TODO: Randomly assign the key
        int dialogueKey = 0;
        AssignTask(dialogueKey);
        questUI.AddNewQuest();
    }
}
