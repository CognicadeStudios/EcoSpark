using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// / Controls the level of each upgrade, and answers queries about what things are possible
/// </summary>
public class ResearchManager : MonoBehaviour
{    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("OneSecond", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OneSecond()
    {
        int x = Upgrade.solar.sun;
        Debug.Log("sun: " + x);
        Upgrade.solar.Update();
    }

    public void UnlockUpgrade(Upgrade upgrade)
    {
        Debug.Log("Upgrade Unlocking");
        upgrade.LevelUp();
    }
}
