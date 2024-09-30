using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject buildMenuPanel, resButton, mailButton, PABar, EcoBar;
    public TextMeshProUGUI moneyCounter, energyCounter;
    private bool buildMenuOpen = false;
    public static UIManager instance;

    private void Awake()
    {
        LeanTween.init(800);
        instance = this;
    }

    private void Update()
    {
        moneyCounter.text = FormatNumberAsK(GameManager.Instance.Money);
        energyCounter.text = FormatNumberAsK(GameManager.Instance.CityEnergy);
    }

    public void UpdatePABar()
    {
        LeanTween.scale(PABar, new Vector3(GameManager.Instance.PublicApproval / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
    }

    public void UpdateEcoBar()
    {
        LeanTween.scale(EcoBar, new Vector3(GameManager.Instance.EcoScore / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
    }


    public static string FormatNumberAsK(int n)
    {
        if (n > 999999)
        {
            return (n / 1000000f).ToString("F2") + "M";
        }
        else if(n > 99999)
        {
            return (n / 1000).ToString("D") + "K";
        }
        else if (n > 9999)
        {
            return (n/1000f).ToString("F1") + "K";
        }
        else
        {
            return n.ToString();
        }
    }

    public void ToggleBuildMenu()
    { 
        buildMenuOpen = !buildMenuOpen;
        if (buildMenuOpen) { OpenBuildMenu(); } else { CloseBuildMenu(); }
    }
    public void OpenBuildMenu()
    {
        LeanTween.scale(mailButton, new Vector3(0f, 0f, 1f), 0.2f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(buildMenuPanel, new Vector3(1f, 1f, 1f), 0.4f).setEase(LeanTweenType.easeOutExpo);  
    }

    public void CloseBuildMenu()
    {
        LeanTween.scale(buildMenuPanel, new Vector3(0f, 1f, 1f), 0.4f).setEase(LeanTweenType.easeOutExpo).setOnComplete(delegate ()
        {
            buildMenuPanel.transform.localScale = new Vector3(0f, 1f, 1f);
        });
        LeanTween.scale(mailButton, new Vector3(1f, 1f, 1f), 0.2f).setEase(LeanTweenType.easeOutBounce);
    }

    public static void CloseButton(GameObject g) {
        LeanTween.scale(g, new Vector3(0f, 0f, 0f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }

    public static void OpenButton(GameObject g)
    {
        LeanTween.scale(g, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }
}
