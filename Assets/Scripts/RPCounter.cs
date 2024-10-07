using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RPCounter : MonoBehaviour
{
    public TextMeshProUGUI countText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnResearchPointsChanged += UpdateCounter;
    }

    void UpdateCounter(object sender, GameManager.OnValueUpdatedArgs e)
    {
        countText.SetText(e.newValue.ToString());
    }
}