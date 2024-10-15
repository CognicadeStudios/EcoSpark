using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject buildMenuPanel, resButton, mailButton, PABar, EcoBar, researchDim, researchMenu, mailboxMenu;
    public TextMeshProUGUI moneyCounter, energyCounter;
    private bool buildMenuOpen = false;
    public static UIManager instance;
    public List<BuildButton> buildButtons;
    public UI_SkillTree skillTree;

    [Header("Sprites")]
    public Sprite money;
    public Sprite energy;
    public Sprite ecoScore;
    public Sprite publicScore;
    public Sprite researchPoints;


    private void Awake()
    {
        LeanTween.init(800);
        instance = this;
    }

    private void Start()
    {
        researchMenu.transform.LeanScale(new(0, 0, 0), 0);
        mailboxMenu.transform.LeanScale(new(0, 0, 0), 0);
        buildButtons = new(GetComponentsInChildren<BuildButton>());
    }

    private void Update()
    {
        moneyCounter.text = FormatNumberAsK(Mathf.RoundToInt(GameManager.Instance.Money));
        energyCounter.text = FormatNumberAsK(Mathf.RoundToInt(GameManager.Instance.CityEnergy));

            UpdatePABar();
            UpdateEcoBar();

        foreach (BuildButton b in buildButtons)
        {
            b.GetComponent<Button>().enabled = BuildingController.GetCostToBuild(b.buildingType) <= GameManager.Instance.Money;
        }
    }

    float pt,et = 0;
    public void UpdatePABar()
    {
        pt += Time.deltaTime;
        //LeanTween.scale(PABar, new Vector3(GameManager.Instance.PublicApproval / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
        PABar.transform.localScale = Vector3.Lerp(PABar.transform.localScale, new Vector3(GameManager.Instance.PublicApproval / 100f, 1, 0), pt);
        if (PABar.transform.localScale.Equals(new Vector3(GameManager.Instance.PublicApproval / 100f, 1, 0)))
        {
            pt = 0;
        }
    }

    public void UpdateEcoBar()
    {
        et += Time.deltaTime;
        //LeanTween.scale(EcoBar, new Vector3(GameManager.Instance.EcoScore / 100f, 1, 0), 1f).setEase(LeanTweenType.easeOutExpo);
        EcoBar.transform.localScale = Vector3.Lerp(EcoBar.transform.localScale, new Vector3(GameManager.Instance.EcoScore / 100f, 1, 0), et);
        if (EcoBar.transform.localScale.Equals(new Vector3(GameManager.Instance.EcoScore / 100f, 1, 0)))
        {
            et = 0;
        }
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

    public void OpenResearchMenu()
    {
        skillTree.UpdateVisual();
        researchDim.SetActive(true);
        LeanTween.value(researchDim, 0, 0.85f, 0.2f).setOnUpdate(UpdateDimAlpha);
        researchMenu.SetActive(true);
        LeanTween.scale(researchMenu, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBounce);
    }

    public void CloseResearchMenu()
    {
        LeanTween.scale(researchMenu, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.value(researchDim, 0.85f, 0, 0.2f).setOnUpdate(UpdateDimAlpha);
        researchDim.SetActive(false);
    }

    public void OpenMailboxMenu()
    {
        researchDim.SetActive(true);
        LeanTween.value(researchDim, 0, 0.85f, 0.2f).setOnUpdate(UpdateDimAlpha);
        mailboxMenu.SetActive(true);
        LeanTween.scale(mailboxMenu, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutBounce);
    }

    public void CloseMailboxMenu()
    {
        LeanTween.scale(mailboxMenu, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.value(researchDim, 0.85f, 0, 0.2f).setOnUpdate(UpdateDimAlpha);
        researchDim.SetActive(false);
    }

    public static void CloseButton(GameObject g) {
        LeanTween.scale(g, new Vector3(0f, 0f, 0f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }

    public static void OpenButton(GameObject g)
    {
        LeanTween.scale(g, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutElastic);
    }
    
    private void UpdateDimAlpha(float alpha)
    {
        Color color = researchDim.GetComponent<Image>().color;
        color.a = alpha;
        researchDim.GetComponent<Image>().color = color;
    }
}
