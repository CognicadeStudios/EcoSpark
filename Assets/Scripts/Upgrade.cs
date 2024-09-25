using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Upgrade
{
    
    public static SolarUpgrade solar = new SolarUpgrade();
    
    public int level;

    public abstract void LevelUp();
    public abstract int GetCostToUpgrade();
}

public class SolarUpgrade : Upgrade
{
    public int sun = 0;
    public int sunPer = 1;

    public SolarUpgrade()
    {
        level = 1;
    }

    public override void LevelUp()
    {
        level++;
        sunPer++;

    }

    public override int GetCostToUpgrade()
    {
        return level * level;
    }

    public void Update()
    {
        sun += sunPer;
    }
}
