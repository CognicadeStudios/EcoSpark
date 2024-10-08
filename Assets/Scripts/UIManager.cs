using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject buildMenuPanel, resButton, mailButton, PABar, EcoBar, researchDim, researchMenu;
    public TextMeshProUGUI moneyCounter, energyCounter;
    private bool buildMenuOpen = false;
    public static UIManager instance;
    public List<GameObject> buildButtons;
    public UI_SkillTree skillTree;
    private void Awake()
    {
        LeanTween.init(800);
        instance = this;
    }

    private void Start()
    {
        researchMenu.transform.LeanScale(new(0, 0, 0), 0);
    }

    private void Update()
    {
        moneyCounter.text = FormatNumberAsK(Mathf.RoundToInt(GameManager.Instance.Money));
        energyCounter.text = FormatNumberAsK(Mathf.RoundToInt(GameManager.Instance.CityEnergy));

        buildButtons[0].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.SOLAR_PANEL) <= GameManager.Instance.Money;
        buildButtons[1].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.WIND_TURBINE) <= GameManager.Instance.Money;
        buildButtons[2].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.WATER_TURBINE) <= GameManager.Instance.Money;
        buildButtons[3].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.NUCLEAR_PLANT) <= GameManager.Instance.Money;
        //buildButtons[4].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.OIL_DRILL) <= GameManager.Instance.Money;
        buildButtons[4].GetComponent<Button>().enabled = BuildingController.GetCostToBuild(BuildingType.COAL_MINE) <= GameManager.Instance.Money;
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
