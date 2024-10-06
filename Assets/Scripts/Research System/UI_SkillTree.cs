using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;
using UnityEditor;
using System;
using TMPro;
public class UI_SkillTree : MonoBehaviour
{
    public static ResearchManager researchManager;
    public List<UI_UpgradeButton> upgradeButtons;
    public List<UI_UpgradeConnection> upgradeConnections;
    public TextMeshProUGUI rpamounttext;

    public void Awake()
    {
        upgradeButtons = new(transform.GetComponentsInChildren<UI_UpgradeButton>());
        upgradeConnections = new(transform.GetComponentsInChildren<UI_UpgradeConnection>());
    }
    public void Initialize()
    {
        researchManager = ResearchManager.Instance;
        researchManager.OnUpgradeResearched += OnUpgradeResearched;
        GameManager.Instance.OnResearchPointsChanged += OnResearchPointsChanged;

        UpdateVisual();
    }

    private void OnUpgradeResearched(object sender, ResearchManager.OnUpgradeResearchedArgs e)
    {
        UpdateVisual();
    }
    private void OnResearchPointsChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }


    public void UpdateVisual()
    {
        rpamounttext.SetText(GameManager.Instance.ResearchPoints.ToString());
        foreach (UI_UpgradeButton ub in upgradeButtons)
        {
            ub.UpdateVisual();
        }

        foreach (UI_UpgradeConnection uc in upgradeConnections)
        {
            Upgrade up = uc.GetUpgrade();
            if (researchManager.IsUpgradeResearched(up))
            {
                uc.Unlock();
            }
            uc.UpdateVisual();
        }
    }
}
//Video timestamp: 18:20
//https://www.youtube.com/watch?v=_OQTTKkwZQY&t=1103s
