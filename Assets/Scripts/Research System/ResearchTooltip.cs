using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResearchTooltip : MonoBehaviour
{

    public TextMeshProUGUI head;
    public TextMeshProUGUI price;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetText(string header, string price)
    { 
        head.text = header;
        this.price.text = price;
    }

    private void Update()
    { 
        Vector2 position = Input.mousePosition;

        float pivX = position.x/Screen.width;
        float pivY = position.y/Screen.width;


        rect.pivot = new Vector2(pivX, pivY);
        transform.position = position;
    }
}
