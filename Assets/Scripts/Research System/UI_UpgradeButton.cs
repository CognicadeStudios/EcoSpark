using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_UpgradeButton : MonoBehaviour
{
    public Image image;
    public Image background;
    public Upgrade upgrade;

    void Awake()
    {
        transform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            bool b = ResearchManager.Instance.TryUnlockUpgrade(upgrade);
            if (!b)
            {
                Debug.Log(ResearchManager.Instance.ErrorMessage);
            }
        };
        image = transform.Find("image").GetComponent<Image>();
        background = transform.Find("background").GetComponent<Image>();
    }

    public bool IsResearched()
    {
        return ResearchManager.Instance.IsUpgradeResearched(upgrade);
    }
    public bool IsResearchable()
    {
        return ResearchManager.Instance.IsUpgradeResearchable(upgrade);
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