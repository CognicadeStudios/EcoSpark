using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class UI_SkillTree : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("SolarPanel").Find("Button").GetComponent<Button_UI>().ClickFunc = () =>
        {
            GetComponent<ResearchManager>().UnlockUpgrade(Upgrade.solar);
        };
    }
}
