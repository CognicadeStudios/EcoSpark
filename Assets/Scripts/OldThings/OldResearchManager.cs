/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEditor.Rendering.Universal;

public class OldResearchManager : MonoBehaviour
{
    public ResearchPointHolder rp;
    //public int[] researchCosts;
    //public int[] researchLevels;
    //public float[] timeLeft;
    //public float[] researchTimes;
    //public Slider[] researchSliders;
    //public Button[] startButtons;
    public Upgrade[] upgrades;
    int numUpgrades;
    
    // Start is called before the first frame update
    void Start()
    {
        rp = FindObjectOfType<ResearchPointHolder>();
        numUpgrades = upgrades.Length;

        Debug.Log("NumUpgrades: " + numUpgrades);

        for (int i = 0; i < numUpgrades; i++)
        {
            upgrades[i].ID = i;
            upgrades[i].slider.maxValue = 1.0f;
            upgrades[i].slider.value = 0f;
            int ID = i;
            upgrades[i].button.onClick.AddListener(() => StartResearch(ID));
        }
    }

    void StartResearch(int ind)
    {
        Debug.Log("ind:" + ind);
        if (upgrades[ind].timeLeft > 0f){
            Debug.Log("Research Already Running");
            return;
        }
        upgrades[ind].timeLeft = upgrades[ind].upgradeTime;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numUpgrades; i++)
        {
            if (upgrades[i].timeLeft <= 0f)
            {
                continue;
            }
            upgrades[i].timeLeft -= Time.deltaTime;
            upgrades[i].slider.value = 1f - (upgrades[i].timeLeft / upgrades[i].upgradeTime);
            if (upgrades[i].timeLeft < 0f)
            {
                upgrades[i].timeLeft = 0f;
                upgrades[i].levelUp();
            }
        }
    }
}
*/