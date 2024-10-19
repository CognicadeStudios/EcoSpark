using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private LTDescr delay;
    public string header;
    [TextArea(3, 5)]
    public string content;
    public int price;
    public bool isPriced;
    public CurrencyType currencyType;
    public TooltipType tooltipType;

    public enum TooltipType
    {
        ResearchUpgrade,
        BuildButton,
    };

    tooltipinfo GenerateTooltip()
    {
        switch (tooltipType)
        {
            case TooltipType.ResearchUpgrade:
                return new(header, content, price);
            case TooltipType.BuildButton:
                return new(header, content, BuildingController.GetCostToBuild((BuildingType)price));
        }
        return new(header, content, price);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            if (isPriced) {
                tooltipinfo t = GenerateTooltip();
                TooltipSystem.Show(t.head, t.cont, UIManager.FormatNumberAsK(t.price),currencyType);
            }
            else {
                TooltipSystem.Show(header, content);
            }
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

    class tooltipinfo
    {
        public string head, cont;
        public int price;
        public tooltipinfo(string h, string c, int p)
        {
            head = h;
            cont = c;
            price = p;
        }
    }

}

