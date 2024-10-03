using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public Tooltip tooltip;

    public void Awake()
    {
        instance = this;
    }

    public static void Show(string header, string content)
    {
        instance.tooltip.SetText(header, content);
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Show(string header, string content, string price)
    {
        instance.tooltip.SetText(header, content, price);
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
