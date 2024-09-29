using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject buildMenuPanel, resButton, mailButton, PABar, EcoBar;
    private bool buildMenuOpen = false;
    public static UIManager instance;

    private void Awake()
    {
        LeanTween.init(800);
        instance = this;
    }

    public void UpdatePABar()
    {
        LeanTween.scale(PABar, new Vector3(GameManager.Instance.GetPublicApproval() / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
    }

    public void UpdateEcoBar()
    {
        LeanTween.scale(EcoBar, new Vector3(GameManager.Instance.GetEcoScore() / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
    }


    public static string FormatNumberAsK(int n)
    {
        if (n > 999999)
        {
            return (n / 1000000).ToString("D") + "M";
        }
        if (n > 999)
        {
            return (n/1000).ToString("D") + "K";
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
        LeanTween.scale(buildMenuPanel, new Vector3(1f, 1f, 1f), 0.4f).setEase(LeanTweenType.easeOutExpo);
        
    }

    public void CloseBuildMenu()
    {
        LeanTween.scale(buildMenuPanel, new Vector3(0f, 1f, 1f), 0.4f).setEase(LeanTweenType.easeOutExpo).setOnComplete(delegate ()
        {
            buildMenuPanel.transform.localScale = new Vector3(0f, 1f, 1f);
        });
    }

    public static void CloseButton(GameObject g) {
        LeanTween.scale(g, new Vector3(0f, 0f, 0f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }

    public static void OpenButton(GameObject g)
    {
        LeanTween.scale(g, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }
}
