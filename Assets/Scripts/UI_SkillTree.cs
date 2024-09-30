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
    public ResearchManager researchManager;
    private List<UpgradeButton> upgradeButtons;
    public List<UpgradeConnection> upgradeConnections;
    public TextMeshProUGUI rpamounttext;

    public void Initialize()
    {
        researchManager.OnUpgradeResearched += OnUpgradeResearched;
        GameManager.Instance.OnResearchPointsChanged += OnResearchPointsChanged;
        UpdateVisual();
    }

    

    private void Awake()
    {
        upgradeButtons = new List<UpgradeButton>();
        upgradeButtons.Add(new UpgradeButton(transform.Find("SolarUpgrade1"), researchManager, Upgrades.SolarLevel1));
        upgradeButtons.Add(new UpgradeButton(transform.Find("SolarUpgrade2"), researchManager, Upgrades.SolarLevel2));
        upgradeButtons.Add(new UpgradeButton(transform.Find("HydroUpgrade1"), researchManager, Upgrades.HydroLevel1));
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
            Upgrades up = uc.GetUpgrade();
            if(researchManager.IsUpgradeResearched(up))
            {
                uc.Unlock();
            }
            uc.UpdateVisual();
        }
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
                researchManager.TryUnlockUpgrade(up);
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


        public Upgrades upgrade;
        public List<Image> images;
        private bool lit;

        public UpgradeConnection(Upgrades up, List<Image> im)
        {
            upgrade = up;
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

        public Upgrades GetUpgrade()
        {
            return upgrade;
        }
        public List<Image> getImages()
        {
            return images;
        }
    }
}
//Video timestamp: 18:20
//https://www.youtube.com/watch?v=_OQTTKkwZQY&t=1103s
