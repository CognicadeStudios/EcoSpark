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

    public void Awake()
    {
        entries = new List<Transform>();
        InvokeRepeating(nameof(UpdateListUI), 5f, 2f);
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

    public void HighlightQuest(int key)
    {
        QuestGoal quest = QuestSystem.instance.CurrentTasks[key];
        sidebar.transform.Find("Character").Find("CharacterIcon").GetComponent<Image>().sprite = quest.image;
        sidebar.transform.Find("Quest Title").GetComponent<TextMeshProUGUI>().text = quest.name;    
        sidebar.transform.Find("QuestDialogue").GetComponent<TextMeshProUGUI>().text = quest.dialogue;
        sidebar.transform.Find("Quest Info").Find("Quest Statement").GetComponent<TextMeshProUGUI>().text = quest.mission;
        sidebar.transform.Find("Quest Info").Find("QuestProgress").GetComponent<TextMeshProUGUI>().text = quest.CurrentAmount + "/" + quest.RequiredAmount;
    }
}
