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
    public Objective type { get; set; }

    public int StartingAmount { get; set; }

    //Dialogue stuff
    public Sprite image;
    public string name, dialogue, mission;

    public QuestGoal(int id, Objective type, Cost reward, int requiredAmount, string dialogue, string mission, string name, Sprite image)
    {
        this.ID = id;
        this.Completed = false;
        this.cost = reward;
        this.type = type;
        this.RequiredAmount = requiredAmount;
        this.StartingAmount = type switch
        {
            Objective.Upgrade => 0,
            Objective.Build => BuildingInfo.NumberBuilt[(BuildingType)ID],
            Objective.Destroy => BuildingInfo.NumberBuilt[(BuildingType)ID],
            Objective.HaveBuildings => BuildingInfo.NumberBuilt[(BuildingType)ID],
            _ => 0
        };
        this.CurrentAmount = 0;
        this.dialogue = dialogue;
        this.mission = mission;
        this.name = name;
        this.image = image;
    }

    public void Evaluate()
    {
        switch (type)
        {
            case Objective.Build:
                CurrentAmount = BuildingInfo.NumberBuilt[(BuildingType)ID] - StartingAmount;
                if (CurrentAmount >= RequiredAmount)
                {
                    Completed = true;
                }
                break;
            case Objective.Destroy:
                CurrentAmount = -(BuildingInfo.NumberBuilt[(BuildingType)ID] - StartingAmount);
                if (CurrentAmount >= RequiredAmount)
                {
                    Completed = true;
                }
                break;
            case Objective.Upgrade:
                if (ResearchManager.Instance.IsUpgradeResearched((Upgrade)ID))
                {
                    Completed = true;
                    CurrentAmount = 1;
                }
                break;
            case Objective.HaveBuildings:
                CurrentAmount = BuildingInfo.NumberBuilt[(BuildingType)(ID)];
                if (CurrentAmount >= RequiredAmount)
                {
                    Completed = true;
                }
                break;
        }
    }
    
    public void GiveReward()
    {
        GameManager.Instance.Transaction(cost);
    }

}
public enum Objective
{
    Build,
    Upgrade,
    Destroy,
    HaveBuildings
}
