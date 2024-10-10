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
        instance.tooltip.Hide();
    }

    public static void Show(string header, string content)
    {
        instance.tooltip.SetText(header, content);
        instance.tooltip.Show();
    }

    public static void Show(string header, string content, string price, CurrencyType c)
    {
        instance.tooltip.SetText(header, content, price, c);
        instance.tooltip.Show();
    }

    public static void Hide()
    {
        instance.tooltip.Hide();
    }



}
