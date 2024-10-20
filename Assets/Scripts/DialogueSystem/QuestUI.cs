using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public GameObject questUIEntryPrefab, gridParent;
    private List<Transform> entries;
    public GameObject sidebar;
    public GameObject counter;
    public List<Sprite> people;
    public static QuestUI instance;

    public void Awake()
    {
        entries = new List<Transform>();
        InvokeRepeating(nameof(UpdateListUI), 5f, 2f);
        rewardObjects = new List<GameObject>();
        instance = this;
    }

    public void AddNewQuest()
    {
        entries.Add(Instantiate(questUIEntryPrefab, gridParent.transform).transform);
        UpdateListUI();
    }

    public void UpdateListUI()
    {
        for(int i = 0; i < Math.Min(entries.Count, QuestSystem.instance.CurrentTasks.Count); i++)
        {
            QuestGoal quest = QuestSystem.instance.CurrentTasks[i];
            Transform entry = entries[i];

            entry.Find("Character").Find("CharacterIcon").GetComponent<Image>().sprite = quest.image;
            entry.Find("QuestText").GetComponent<TextMeshProUGUI>().text = quest.mission;
            entry.Find("Progress").GetComponent<TextMeshProUGUI>().text = quest.CurrentAmount + "/" + quest.RequiredAmount;
            entry.GetComponent<QuestButton>().index = i;
        }
        
        if(entries.Count > 0)
        {
            counter.SetActive(true);
            counter.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = entries.Count.ToString();
        }
        else 
        {
            counter.SetActive(false);
        }
    }

    public void EnableCollectButton(int key)
    {
        Transform entry = entries[key];
        entry.Find("ClaimButton").gameObject.SetActive(true);
        entry.Find("Progress").gameObject.SetActive(false);
    }

    public void FinishQuest(int key)
    {
        Destroy(entries[key].gameObject);
        QuestSystem.instance.CurrentTasks[key].GiveReward();
        QuestSystem.instance.CurrentTasks.RemoveAt(key);
        entries.RemoveAt(key);
    }

    public GameObject rewardPrefab;
    public Sprite ecoScoreSprite, moneySprite, researchPointsSprite, energySprite, publicApprovalSprite;
    List<GameObject> rewardObjects;

    private void CreateReward(float amount, Sprite sprite)
    {
        GameObject go = Instantiate(rewardPrefab, sidebar.transform.Find("Rewards Panel"));
        go.transform.Find("RewardIcon").GetComponent<Image>().sprite = sprite;
        go.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(amount).ToString();
        rewardObjects.Add(go);
    }

    private void DestroyRewards()
    {
        foreach(GameObject go in rewardObjects)
        {
            Destroy(go);
        }
        rewardObjects.Clear();
    }

    public void HighlightQuest(int key)
    {
        QuestGoal quest = QuestSystem.instance.CurrentTasks[key];
        sidebar.transform.Find("Character").Find("CharacterIcon").GetComponent<Image>().sprite = quest.image;
        sidebar.transform.Find("Quest Title").GetComponent<TextMeshProUGUI>().text = quest.name;    
        sidebar.transform.Find("QuestDialogue").GetComponent<TextMeshProUGUI>().text = quest.dialogue;
        sidebar.transform.Find("Quest Info").Find("Quest Statement").GetComponent<TextMeshProUGUI>().text = quest.mission;
        sidebar.transform.Find("Quest Info").Find("QuestProgress").GetComponent<TextMeshProUGUI>().text = quest.CurrentAmount + "/" + quest.RequiredAmount;

        DestroyRewards();
        //Set Rewards
        if(quest.cost.Money != 0)
        {
            CreateReward(quest.cost.Money, moneySprite);
        }

        if(quest.cost.ResearchPoints != 0)
        {
            CreateReward(quest.cost.ResearchPoints, researchPointsSprite);
        }

        if(quest.cost.EcoScore != 0)
        {
            CreateReward(quest.cost.EcoScore, ecoScoreSprite);
        }

        if(quest.cost.CityEnergy != 0)
        {
            CreateReward(quest.cost.EcoScore, energySprite);
        }

        if(quest.cost.PublicApproval != 0)
        {
            CreateReward(quest.cost.PublicApproval, publicApprovalSprite);
        }
    }
}
