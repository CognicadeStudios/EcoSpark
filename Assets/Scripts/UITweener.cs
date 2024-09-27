using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITweener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float tweenTime = 1f;
    public LeanTweenType ease = LeanTweenType.easeOutElastic;
    public Vector3 scaleTo;
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, scaleTo, tweenTime).setEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(1f,1f,1f), tweenTime).setEase(ease);
    }
}
