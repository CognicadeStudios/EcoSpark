using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class UI_SkillTree : MonoBehaviour
{
    public ResearchManager researchManager;
    private void Awake()
    {
        transform.Find("SolarButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            researchManager.UnlockUpgrade(Upgrades.SolarLevel1);
        };
    }
}
