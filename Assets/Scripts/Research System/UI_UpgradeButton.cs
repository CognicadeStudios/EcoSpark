using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_UpgradeButton : MonoBehaviour
{
    public Image image;
    public GameObject background;
    public GameObject border;
    public Upgrade upgrade;

    void Awake()
    {
        transform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Clickevent");
            bool b = ResearchManager.Instance.TryUnlockUpgrade(upgrade);
            if (!b)
            {
                Debug.Log(ResearchManager.Instance.ErrorMessage);
            }
        };
        image = transform.Find("image").GetComponent<Image>();
        background = transform.Find("lock").gameObject;
        border = transform.Find("border").gameObject;
    }

    void Start()
    {
        UpdateVisual();
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
            LeanTween.scale(border, new Vector3(1f,1f,0), 1f).setEase(LeanTweenType.easeOutExpo); ;
        }
        else if (IsResearchable()) {

            LeanTween.scale(background, Vector3.zero, 1f).setEase(LeanTweenType.easeOutExpo).setOnComplete(delegate () { background.SetActive(false); });
        }
        else
        {
            return;
        }
    }
}