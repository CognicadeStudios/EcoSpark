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
    private List<UpgradeButton> upgradeButtons;
    public List<BasicUpgradeButton> basicUpgradeButtons;
    public List<UpgradeConnection> upgradeConnections;
    public TextMeshProUGUI rpamounttext;

    public void Initialize()
    {
        researchManager = GetComponent<ResearchManager>();
        researchManager.OnUpgradeResearched += OnUpgradeResearched;
        GameManager.Instance.OnResearchPointsChanged += OnResearchPointsChanged;
        
        upgradeButtons = new List<UpgradeButton>();
        foreach (BasicUpgradeButton bu in basicUpgradeButtons)
        {
            upgradeButtons.Add(new UpgradeButton(bu.transform, bu.upgrade));
        }
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
        foreach (UpgradeButton ub in upgradeButtons)
        {
            ub.UpdateVisual();
        }

        foreach (UpgradeConnection uc in upgradeConnections)
        {
            Upgrade up = uc.GetUpgrade();
            if(researchManager.IsUpgradeResearched(up))
            {
                uc.Unlock();
            }
            uc.UpdateVisual();
        }
    }
    [Serializable]
    public class BasicUpgradeButton
    {
        public Transform transform;
        public Upgrade upgrade;
    }
    [Serializable]
    public class UpgradeButton
    {
        public Transform transform;
        private Image image;
        private Image background;
        private Upgrade upgrade;
        
        public UpgradeButton(Transform trans, Upgrade up)
        {
            this.transform = trans;
            upgrade = up;
            transform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                bool b = UI_SkillTree.researchManager.TryUnlockUpgrade(up);
                if (!b)
                {
                    Debug.Log(UI_SkillTree.researchManager.ErrorMessage);
                }
            };
            image = transform.Find("image").GetComponent<Image>();
            background = transform.Find("background").GetComponent<Image>();
        }

        public bool IsResearched()
        {
            return UI_SkillTree.researchManager.IsUpgradeResearched(upgrade);
        }
        public bool IsResearchable()
        {
            return UI_SkillTree.researchManager.IsUpgradeResearchable(upgrade);
        }

        public Image getImage()
        {
            return image;
        }

        public void UpdateVisual()
        {
            if (IsResearched())
            {
                background.color = Color.green;
            }
            else if (IsResearchable())
            {
                background.color = Color.white;
            }
            else
            {
                background.color = Color.red;
            }
        }
    }
    [Serializable]
    public class UpgradeConnection
    {
        private static Color offColor = new Color(.5f, .5f, .5f);
        private static Color onColor = new Color(.95f, .95f, .95f);


        public Upgrade SourceUpgrade;
        public List<Image> images;
        private bool lit;

        public UpgradeConnection(Upgrade up, List<Image> im)
        {
            SourceUpgrade = up;
            images = im;
            lit = false;
        }

        public void UpdateVisual()
        {
            foreach (Image image in images)
            {
                if (lit)
                {
                    image.color = onColor;
                }
                else
                {
                    image.color = offColor;
                }
            }
        }

        public void Unlock()
        {
            lit = true;
        }

        public Upgrade GetUpgrade()
        {
            return SourceUpgrade;
        }
        public List<Image> getImages()
        {
            return images;
        }
    }
}
//Video timestamp: 18:20
//https://www.youtube.com/watch?v=_OQTTKkwZQY&t=1103s
