using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    private int money;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            OnMoneyChanged?.Invoke(this, null);
        }
    }
    public event EventHandler OnMoneyChanged;

    [Range(0,100)]
    private int publicApproval;
    public int PublicApproval
    {
        get
        {
            return publicApproval;
        }
        set
        {
            publicApproval = value;
            OnPublicApprovalChanged?.Invoke(this, null); 
        }
    }
    public event EventHandler OnPublicApprovalChanged;

    [Range(0,100)]
    private int ecoScore;
    public int EcoScore
    {
        get
        {
            return ecoScore;
        }
        set
        {
            ecoScore = value;
            OnEcoScoreChanged?.Invoke(this, null);
        }
    }
    public event EventHandler OnEcoScoreChanged;

    private int cityEnergy;
    public int CityEnergy
    {
        get
        {
            return cityEnergy;
        }
        set
        {
            cityEnergy = value;
        }
    }
    [SerializeField]
    private int researchPoints;
    public int ResearchPoints
    {
        get
        {
            return researchPoints;
        }
        set
        {
            researchPoints = value;
            OnResearchPointsChanged?.Invoke(this, null);
        }
    }
    public event EventHandler OnResearchPointsChanged;

    void Awake()
    {
        Instance = this;
    }
}