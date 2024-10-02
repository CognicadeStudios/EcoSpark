using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public ResearchManager researchManager;
    public BuildingController buildingController;
    public Button_UI addRPButton;
    public UI_SkillTree skillTree;
    // Start is called before the first frame update
    void Start()
    {
        addRPButton.ClickFunc = () =>
        {
            GameManager.Instance.ResearchPoints++;
            skillTree.UpdateVisual();
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(buildingController.GetCostToBuild(BuildingController.BuildingType.SOLAR_PANEL));
        }
    }
}
