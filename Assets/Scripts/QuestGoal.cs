using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGoal : Task
{
    // Start is called before the first frame
    public int BuildingID { get; set; }
    public int ecoBoost { get; set; }
    public int approvalBoost { get; set; }
    public int moneyBoost { get; set; }

    public QuestGoal(int id, string Description, bool Completed, int CurrentAmount, int RequiredAmount, int ecoBoost, int approvalBoost, int moneyBoost)
    {
        this.BuildingID = id;
        this.Description = Description;
        this.Completed = Completed;
        this.CurrentAmount = CurrentAmount;
        this.RequiredAmount = RequiredAmount;
        this.ecoBoost = ecoBoost;
        this.approvalBoost = approvalBoost;
        this.moneyBoost = moneyBoost;

    }

    public override void Init()
    {
        base.Init();
        //Make an event when a building is set down
    }

    void BuildingBuilt(int id)
    {
        if(id == this.BuildingID)
        {
            this.CurrentAmount++;
            Evaluate();
            if (Completed)
            {
                GiveReward();
            }
        }
    }
    
    void GiveReward()
    {
        GameManager.Instance.Money += moneyBoost;
        GameManager.Instance.PublicApproval += approvalBoost;
        GameManager.Instance.EcoScore += ecoBoost;
    }

}
