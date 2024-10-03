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

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetText(string header, string content)
    {
        price.gameObject.SetActive(false);
        head.text = header;
        cont.text = content;
    }

    public void SetText(string header, string content, string price)
    {
        this.price.gameObject.SetActive(true);
        head.text = header;
        cont.text = content; 
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
