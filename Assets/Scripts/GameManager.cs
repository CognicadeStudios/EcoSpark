using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public class OnValueUpdatedArgs : EventArgs{ 
        public float newValue; 
        public OnValueUpdatedArgs(float n) { newValue = n; } 
    }

    [SerializeField]
    private float money;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            OnMoneyChanged?.Invoke(this, new(value));
        }
    }
    public event EventHandler<OnValueUpdatedArgs> OnMoneyChanged;

    [Range(0,100), SerializeField]
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
            OnPublicApprovalChanged?.Invoke(this, new(value)); 
        }
    }
    public event EventHandler<OnValueUpdatedArgs> OnPublicApprovalChanged;

    [Range(0,100), SerializeField]
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
            OnEcoScoreChanged?.Invoke(this, new(value));
        }
    }
    public event EventHandler<OnValueUpdatedArgs> OnEcoScoreChanged;

    [SerializeField]
    private float cityEnergy;
    public float CityEnergy
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
            OnResearchPointsChanged?.Invoke(this, new(value));
        }
    }
    public event EventHandler<OnValueUpdatedArgs> OnResearchPointsChanged;

    void Awake()
    {
        Instance = this;
    }
}

public class Cost
{
    public int ResearchPoints, PublicApproval, EcoScore;
    public float CityEnergy, Money;
    public Cost(){
        ResearchPoints = 0;
        PublicApproval = 0;
        EcoScore = 0;
        CityEnergy = 0f;
        Money = 0f;
    }
};