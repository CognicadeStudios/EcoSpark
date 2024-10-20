using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    //Quest Management
    private float probabilityOfTask = 0f;
    public List<QuestGoal> AvailableTasks;
    public List<QuestGoal> CurrentTasks;
    public const float baseProbability = 0.0001f;
    public const float timeThreshold = 4.0f;
    private LightingManager lightManager;

    //Misc
    public QuestUI questUI;
    public static QuestSystem instance;
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

        taskManager = FindObjectOfType<QuestSystem>();
        PopulateTaskList();

        
        StartRandomDialogue();
        StartRandomDialogue();
    }

     private void PopulateTaskList()
    {
        AvailableTasks = new List<QuestGoal>()
        {
            new QuestGoal(
                (int)BuildingType.SOLAR_PANEL,
                Objective.Build, 
                new Cost(0, 5, 3, 100, 0),
                2,
                "Our renewable energy output is lagging behind! "+
                "Build 2 solar panels to help meet our sustainability goals.",
                "Build 2 Level 1 Solar Panels",
                "Governor",
                null
            ),
            new QuestGoal(
                (int)Upgrade.Solar1,
                Objective.Upgrade, 
                new Cost(0, 3, 5, 150, 0),
                1,
                "'Our energy bills are out of control. " +
                "If you could upgrade a few of our buildings to be more energy efficient, we'd all save a fortune.",
                "Upgrade Solar Panels to Level 2",
                "Local Business Owner",
                null
            ),
            new QuestGoal(
                (int)BuildingType.WIND_TURBINE,
                Objective.Build, 
                new Cost(0, 5, 3, 100, 0),
                2,
                "'We,ve had a power shortage in our area. We need help!"+
                "Could you build a wind farm and supply us with energy?",
                "Build 2 Level 1 Wind Turbines",
                "Farmers' Council",
                null
            ),

            new QuestGoal(
                (int)Upgrade.Wind1,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 150, 0),
                1,
                "The recent coal shortage has severely impacted our power output." +
                "We need to compensate by boosting solar energy production.",
                "Upgrade Wind Turbines to Level 2",
                "Energy Advisor",
                null
            ),

            new QuestGoal(
                (int)BuildingType.COAL_MINE,
                Objective.Destroy,
                new Cost(0, 5, 5, 300, 0),
                1,
                "Environmental activists demand: We can't let coal poison our air any longer! Tear it down!",
                "Dismantle the coal plant to eliminate pollution",
                "Pollution Activists",
                null
            ),

            new QuestGoal(
                (int)Upgrade.Wind2,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 200, 0),
                1,
                "The city has had a surge in electic consumtion." +
                "We need to compensate by boosting wind energy production.",
                "Upgrade Wind Turbines to Level 3",
                "People's Council",
                null
            ),
            
            new QuestGoal(
                (int)BuildingType.WATER_TURBINE,
                Objective.Build, 
                new Cost(0, 5, 5, 300, 0),
                2,
                "Geologists have discovered that underground rivers offer untapped energy potential. " +
                "Construct a hydroelectric plant to power thousands sustainably ",
                "Build 2 Hydroelectric Plants",
                "Geologists",
                null
            ),

            new QuestGoal(
                (int)Upgrade.Water1,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 300, 0),
                1,
                "The chief engineer demands immediate upgrades." +
                "We need more power to support the city's growth!",
                "Upgrade the hydroelectric plant to Level 2",
                "City Engineer",
                null
            ),

            new QuestGoal(
                (int)Upgrade.Wind3,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 1000, 0),
                1,
                "Recently, renewable energy has hit a high in the markets" +
                "To win over investors, create modern wind infrastructure",
                "Upgrade Wind Turbines to Level 3",
                "Investors",
                null
            ),

            new QuestGoal(
                (int)BuildingType.GEOTHERMAL_PLANT,
                Objective.Build, 
                new Cost(0, 5, 5, 300, 0),
                1,
                "The city's energy advisor urges action!" +
                "Tap into geothermal sources before our fossil fuel reserves dwindle!",
                "Build a geothermal plant to harness Earth's heat",
                "Geologists",
                null
            ),

            new QuestGoal(
                (int)BuildingType.COAL_MINE,
                Objective.Destroy,
                new Cost(0, 5, 5, 300, 0),
                1,
                "Environmental activists demand: We can't let coal poison our air any longer! Tear it down!",
                "Dismantle the coal plant to eliminate pollution",
                "Pollution Activists",
                null
            ),
            
            new QuestGoal(
                (int)Upgrade.Geothermal1,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 300, 0),
                1,
                "Lead scientist warns: The grid's struggling. We need stronger geothermal output to stabilize power supply!",
                "Upgrade the geothermal plant to boost energy output",
                "Lead Scientist",
                null
            ),

            new QuestGoal(
                (int)BuildingType.NUCLEAR_PLANT,
                Objective.Build, 
                new Cost(0, 5, 5, 300, 0),
                1,
                "Our energy demands are soaring. Nuclear power is our path to a cleaner future!",
                "Build a nuclear plant to generatelow-carbon energy",
                "Energy Commissioner",
                null
            ),

            new QuestGoal(
                (int)Upgrade.Nuclear1,
                Objective.Upgrade, 
                new Cost(0, 5, 5, 300, 0),
                1,
                "The chief safety officer emphasizes: 'We must modernize systems to ensure optimal performance and safety standards!",
                "Upgrade the nuclear plant to enhance safety features and increase efficiency",
                "Chief Safety Officer",
                null
            ),

            new QuestGoal(
                (int)BuildingType.COAL_MINE,
                Objective.Destroy,
                new Cost(0, 5, 5, 300, 0),
                1,
                "Environmental activists demand: We can't let coal poison our air any longer! Tear it down!",
                "Dismantle the coal plant to eliminate pollution",
                "Pollution Activists",
                null
            )
        };
    }


    // Update is called once per frame
    public void Update()
    {
        for(int i = CurrentTasks.Count - 1; i >= 0; i--)
        {
            QuestGoal ts = CurrentTasks[i];
            ts.Evaluate();
            if (ts.Completed)
            {
                //Debug.Log(ts.name + " has been completed");
                questUI.EnableCollectButton(i);
            }
        }
        
        //This randomly assigns quests
        float prob = Random.Range(0.0f, 1.0f);
        if (prob < probabilityOfTask && AvailableTasks.Count > 0 && CurrentTasks.Count < 3)
        {
            StartRandomDialogue();
            SoundManager.instance.PlaySound("Click");
        }
    }


    /// <summary>
    /// Assign a task to the current tasks by key
    /// </summary>
    /// <param name="key">The index of the task in the AvailableTasks list</param>
    public void AssignTask(int key)
    {   //Assign Task to CurentTask based on the key
        CurrentTasks.Add(AvailableTasks[key]);
        AvailableTasks.RemoveAt(key);
        probabilityOfTask = baseProbability;
    }

    public void StartRandomDialogue()
    {
        Debug.Log("Starting next quest: " + AvailableTasks[0].mission);
        //TODO: Randomly assign the key
        int dialogueKey = 0;
        AssignTask(dialogueKey);
        questUI.AddNewQuest();
    }
}
