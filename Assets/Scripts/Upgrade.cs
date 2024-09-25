using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Upgrade
{
    public static SolarUpgrade solar = new SolarUpgrade();
    //I need multiple types of upgrade, each with a different 
    public int level;

    public abstract void LevelUp();
}

public class SolarUpgrade : Upgrade
{
    public int sun = 0;
    public int sunPer = 1;

    public SolarUpgrade()
    {
        level = 0;
    }

    public override void LevelUp()
    {
        level++;
        sunPer++;
    }

    public void Update()
    {
        sun += sunPer;
    }
}
