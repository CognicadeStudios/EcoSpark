using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;
using UnityEditor;
public class UI_SkillTree : MonoBehaviour
{
    public ResearchManager researchManager;
    private List<UpgradeButton> upgradeButtons;

    public void Initialize()
    {
        researchManager.OnUpgradeResearched += UI_Skill_Tree_OnUpgradeResearched;
        UpdateVisual();
    }
    private void Awake()
    {
        upgradeButtons = new List<UpgradeButton>();
        upgradeButtons.Add(new UpgradeButton(transform.Find("SolarUpgrade1"), researchManager, Upgrades.SolarLevel1));
        upgradeButtons.Add(new UpgradeButton(transform.Find("SolarUpgrade2"), researchManager, Upgrades.SolarLevel2));
        /*transform.Find("SolarUpgrade1").GetComponent<Button_UI>().ClickFunc = () =>
        {
            researchManager.UnlockUpgrade(Upgrades.SolarLevel1);
        };
        transform.Find("SolarUpgrade2").GetComponent<Button_UI>().ClickFunc = () =>
        {
            researchManager.UnlockUpgrade(Upgrades.SolarLevel2);
        };*/
    }

    private void UI_Skill_Tree_OnUpgradeResearched(object sender, ResearchManager.OnUpgradeResearchedArgs e)
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        foreach (UpgradeButton ub in upgradeButtons)
        {
            if (ub.IsResearched())
            {
                ub.getImage().color = Color.green;
            }
            else if (ub.IsResearchable())
            {
                ub.getImage().color = Color.white;
            }
            else
            {
                ub.getImage().color = Color.red;
            }
        }
        //if (researchManager.IsUpgradeResearched(Upgrades.SolarLevel1))
        //{
        //    transform.Find("SolarUpgrade1").Find("image").GetComponent<Image>().color = Color.green;
        //}
        //else if (researchManager.IsUpgradeResearchable(Upgrades.SolarLevel1))
        //{
        //    transform.Find("SolarUpgrade1").Find("image").GetComponent<Image>().color = Color.white;
        //}
        //else
        //{
        //    transform.Find("SolarUpgrade1").Find("image").GetComponent<Image>().color = Color.red;
        //}
        //if (researchManager.IsUpgradeResearched(Upgrades.SolarLevel2))
        //{
        //    transform.Find("SolarUpgrade2").Find("image").GetComponent<Image>().color = Color.green;
        //}
        //else if (researchManager.IsUpgradeResearchable(Upgrades.SolarLevel2))
        //{
        //    transform.Find("SolarUpgrade2").Find("image").GetComponent<Image>().color = Color.white;
        //}
        //else
        //{
        //    transform.Find("SolarUpgrade2").Find("image").GetComponent<Image>().color = Color.red;
        //}
    }

    private class UpgradeButton
    {
        private Transform transform;
        private Image image;
        private Image background;
        private Upgrades upgrade;
        private ResearchManager researchManager;

        public UpgradeButton(Transform trans, ResearchManager rm, Upgrades up)
        {
            this.transform = trans;
            upgrade = up;
            researchManager = rm;

            transform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                researchManager.UnlockUpgrade(up);
            };
            image = transform.Find("image").GetComponent<Image>();
            background = transform.Find("background").GetComponent<Image>();
        }

        public bool IsResearched()
        {
            return researchManager.IsUpgradeResearched(upgrade);
        }
        public bool IsResearchable()
        {
            return researchManager.IsUpgradeResearchable(upgrade);
        }

        public Image getImage()
        {
            return image;
        }
    }
}
