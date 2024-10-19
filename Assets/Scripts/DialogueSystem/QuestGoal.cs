using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGoal
{
    //Quest completion rewards
    public Cost cost;

    //Quest information
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public int ID { get; set; }
    public string type { get; set; }

    //Dialogue stuff
    public Sprite image;
    public string name, dialogue, mission;

    public QuestGoal(int id, string type, Cost reward, int requiredAmount, string dialogue, string mission, string name, Sprite image)
    {
        this.ID = id;
        this.Completed = false;
        this.cost = reward;
        this.type = type;
        this.RequiredAmount = requiredAmount + GridController.Instance.BuildingsBuilt[this.ID];
        this.CurrentAmount = 0;
        this.dialogue = dialogue;
        this.mission = mission;
        this.name = name;
        this.image = image;
    }

    public void Evaluate()
    {
        if (this.type.Equals("Build"))
        {
            CurrentAmount = GridController.Instance.BuildingsBuilt[this.ID];
            if (CurrentAmount >= RequiredAmount)
            {
                Completed = true;
            }
        }
        else if (this.type.Equals("Upgrade"))
        {
            if (ResearchManager.Instance.IsUpgradeResearched((Upgrade)ID))
            {
                Completed = true;
            }
        }
    }
    
    public void GiveReward()
    {
        GameManager.Instance.Transaction(cost);
    }

}
