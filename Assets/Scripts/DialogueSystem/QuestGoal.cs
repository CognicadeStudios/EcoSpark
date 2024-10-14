using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGoal
{
    //Quest completion rewards
    public int ecoBoost { get; set; }
    public int approvalBoost { get; set; }
    public int moneyBoost { get; set; }

    //Quest information
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public int ID { get; set; }
    public string type { get; set; }

    //Dialogue stuff
    public string name;
    public Sprite image;
    public List<string> sentences;

    public QuestGoal(int id, string type, int ecoBoost, int approvalBoost, int moneyBoost, int requiredAmount, List<string> sentences, string name, Sprite image)
    {
        this.ID = id;
        this.Completed = false;
        this.ecoBoost = ecoBoost;
        this.approvalBoost= approvalBoost;
        this.moneyBoost = moneyBoost;
        this.type = type;
        this.RequiredAmount = requiredAmount;
        this.CurrentAmount = 0;
        this.sentences = sentences;
        this.name = name;
        this.image = image;
    }
    public void Check()
    {
            Evaluate();
            if (Completed)
            {
                Debug.Log("Quest Completed");
                GiveReward();
            }
    }

    public void Evaluate()
    {
        if (this.type.Equals("Build"))
        {
            CurrentAmount = GridController.instance.BuildingsBuilt[this.ID];
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
    
    void GiveReward()
    {
        GameManager.Instance.Money += moneyBoost;
        GameManager.Instance.PublicApproval += approvalBoost;
        GameManager.Instance.EcoScore += ecoBoost;
    }

}
