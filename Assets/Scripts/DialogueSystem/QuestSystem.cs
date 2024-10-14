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
    public GridController gridController;
    public float timeElapsed;
    public float baseProbability = 0.2f;
    public float timeThreshold = 4.0f;
    public LightingManager lightManager;

    //Dialogue Management 
    public TextMeshProUGUI nameText;
    public Image npcImage;
    public TextMeshProUGUI dialogueText;
    //TODO: Uncomment Animator Commands
    public Animator anim;
    private Queue<string> sentences;
    private QuestSystem taskManager;
    public void Start()
    {
        CurrentTasks = new List<QuestGoal>();
        AvailableTasks = new List<QuestGoal>();
        gridController = GridController.instance;
        
        probabilityOfTask = baseProbability;
        lightManager = LightingManager.instance;

        sentences = new Queue<string>();
        taskManager = FindObjectOfType<QuestSystem>();
        PopulateTaskList();
    }

     private void PopulateTaskList()
    {
        AvailableTasks = new List<QuestGoal>()
        {
            new QuestGoal(
                (int)BuildingType.SOLAR_PANEL,
                "Build", 
                69,
                420, 
                10000,
                2,
                new List<string>
                {
                        "Governor: 'Our renewable energy output is lagging behind other cities.'",
                        "Governor: 'I need you to build 2 solar panels to help meet our sustainability goals.'"
                },
                "Governor Glen Youngkin",
                null
            )
        };
    }


    // Update is called once per frame
    public void Update()
    {
        timeElapsed = lightManager.TimeElapsed;
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

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        //anim.SetBool("isOpen", false);
    }

    public void StartRandomDialogue()
    {
        //TODO: Randomly assign the key
        int dialogueKey = 0;

        //anim.SetBool("isOpen", true);
        nameText.text = AvailableTasks[dialogueKey].name;
        npcImage.sprite = AvailableTasks[dialogueKey].image;
        sentences.Clear();

        List<string> randSent = AvailableTasks[dialogueKey].sentences;
        AssignTask(dialogueKey);

        foreach (string sentence in randSent)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
}
