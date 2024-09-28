using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI cont;
    public TextMeshProUGUI head;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetText(string header, string content)
    { 
        head.text = header;
        cont.text = content; 
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
