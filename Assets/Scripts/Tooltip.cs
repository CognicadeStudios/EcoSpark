using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI cont;
    public TextMeshProUGUI head;
    public TextMeshProUGUI price;

    private RectTransform rect;

    public Image currencyImg;
    public Sprite moneyCurrency, researchCurrency, energyCurrency;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void SetText(string header, string content)
    {
        price.gameObject.SetActive(false);
        rect.sizeDelta = new Vector2(173, 75);
        head.text = header;
        cont.text = content;
    }

    public void SetText(string header, string content, string price, CurrencyType c)
    {
        this.price.gameObject.SetActive(true);
        rect.sizeDelta = new Vector2(173, 100);
        head.text = header;
        cont.text = content;
        this.price.text = price;

        switch (c)
        {
            case CurrencyType.Money: currencyImg.sprite = moneyCurrency; break;
            case CurrencyType.Research: currencyImg.sprite = researchCurrency; break;
            case CurrencyType.Energy: currencyImg.sprite = energyCurrency; break;
        }
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivX = position.x / Screen.width;
        float pivY = position.y / Screen.width;


        rect.pivot = new Vector2(pivX, pivY);
        transform.position = position;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, new Vector3(0.75f, 0.75f, 0f), 0.1f).setEase(LeanTweenType.easeOutQuad);
    }

    public void Hide()
    {
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), 0.1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(delegate () { gameObject.SetActive(false); });
        ;
    }

    
}

public enum CurrencyType
{
    Money,
    Research,
    Energy
}
