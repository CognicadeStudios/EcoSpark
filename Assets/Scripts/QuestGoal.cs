using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGoal : Task
{
    // Start is called before the first frame
    public int ecoBoost { get; set; }
    public int approvalBoost { get; set; }
    public int moneyBoost { get; set; }
    public string type { get; set; }


    public QuestGoal(int id, bool Completed, int CurrentAmount, int RequiredAmount, int ecoBoost, int approvalBoost, int moneyBoost, string type)
    {
        this.ID = id;
        this.Completed = Completed;
        this.CurrentAmount = CurrentAmount;
        this.RequiredAmount = RequiredAmount;
        this.ecoBoost = ecoBoost;
        this.approvalBoost = approvalBoost;
        this.moneyBoost = moneyBoost;
        this.type = type;

    }

    public QuestGoal(int id, bool Completed, int ecoBoost, int approvalBoost, int moneyBoost, string type)
    {
        this.ID = id;
        this.Completed = Completed;
        this.ecoBoost = ecoBoost;   
        this.approvalBoost= approvalBoost;  
        this.moneyBoost = moneyBoost;
        this.type = type;
        ResearchManager.Instance.OnUpgradeResearched += ResearchManager_OnUpgradeResearched;
    }

    public override void Init()
    {
        base.Init();
    }

    public void Check()
    {
            Evaluate();
            if (Completed)
            {
                GiveReward();
            }
    }

    private void ResearchManager_OnUpgradeResearched(object sender, ResearchManager.OnUpgradeResearchedArgs e)
    {
        if((int) e.upgrade == this.ID)
        {
            Complete();
        }
    }

    public void Evaluate()
    {
        if (this.type.Equals("Build"))
        {
            CurrentAmount = gridController.BuildingsBuilt[this.ID];
            if (CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }
        else if (this.type.Equals("Upgrade"))
        {
            if (ResearchManager.Instance.IsUpgradeResearched((Upgrade)ID))
            {
                Complete();
            }
        }
    }
    
    void GiveReward()
    {
        GameManager.Instance.Money += moneyBoost;
        GameManager.Instance.PublicApproval += approvalBoost;
        GameManager.Instance.EcoScore += ecoBoost;
        if (this.type.Equals("Upgrade"))
        {
            ResearchManager.Instance.OnUpgradeResearched -= ResearchManager_OnUpgradeResearched;
        }
    }

}
