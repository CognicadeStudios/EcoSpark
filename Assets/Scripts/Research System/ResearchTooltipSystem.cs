using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchTooltipSystem : MonoBehaviour
{
    private static ResearchTooltipSystem instance;

    public ResearchTooltip tooltip;

    public void Awake()
    {
        instance = this;
    }

    public static void Show(string header, string price)
    {
        instance.tooltip.SetText(header, price);
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }

}
