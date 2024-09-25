using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// / Controls the level of each upgrade, and answers queries about what things are possible
/// </summary>
public class ResearchManager : MonoBehaviour
{
    public static ResearchManager Instance;
    public TextMeshProUGUI sunText;
    public TextMeshProUGUI rpText;
    public GameManager gameManager;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("OneSecond", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        sunText.SetText(Upgrade.solar.sun.ToString());
        rpText.SetText(gameManager.ResearchPoints.ToString());
    }

    void OneSecond()
    {
        int x = Upgrade.solar.sun;
        Upgrade.solar.Update();
    }

    public void UnlockUpgrade(Upgrade upgrade)
    {
        Debug.Log("Upgrade Unlocking");
        int rps = gameManager.ResearchPoints;
        int cost = upgrade.GetCostToUpgrade();
        if(rps >= cost)
        {
            gameManager.ResearchPoints -= cost;
            Debug.Log("Upgraded for " + cost);
            upgrade.LevelUp();
        }
    }
}
