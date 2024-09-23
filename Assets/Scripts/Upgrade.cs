using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public int ID;
    public int level;
    public int maxlevel = 1;

    public float timeLeft;
    public float[] upgradeTimes = { 2 };
    public float upgradeTime
    {
        get
        {
            if (level == maxlevel)
            {
                return 0f;
            }
            return upgradeTimes[level];
        }
    }

    public int[] rpCosts = { 1 };

    public Slider slider;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelUp()
    {
        Debug.Log("Yay, Upgrade Successful");
    }
    
}
