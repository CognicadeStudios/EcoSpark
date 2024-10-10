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

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            if (isPriced)
            {
                TooltipSystem.Show(header, content, UIManager.FormatNumberAsK(price),currencyType);
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
}
