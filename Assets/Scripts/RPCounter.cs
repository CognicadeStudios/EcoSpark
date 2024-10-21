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
        UpdateCounter(this, new GameManager.OnValueUpdatedArgs(GameManager.Instance.ResearchPoints));
    }

    void UpdateCounter(object sender, GameManager.OnValueUpdatedArgs e)
    {
        countText.SetText(Mathf.FloorToInt(e.newValue).ToString());
    }
}
