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
            UIManager.instance.UpdatePABar();
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
            UIManager.instance.UpdateEcoBar();
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

    public void Transaction(Cost c)
    {
        money += c.Money;
        publicApproval += c.PublicApproval;
        ecoScore += c.EcoScore;
        cityEnergy += c.CityEnergy;
        researchPoints += c.ResearchPoints;
    }
}

/// <example>
/// Cost c = new() {ResearchPoints = 1, CityEnergy = 0};
/// Cost cTotal = 5.5f * c;
/// Cost d = new() {CityEnergy = 5f, Money = 2.3f};
/// cTotal += d;
/// GameManager.Instance += cTotal;
/// </example>
public class Cost
{
    public int ResearchPoints, PublicApproval, EcoScore;
    public float CityEnergy, Money;
    public Cost() {
        ResearchPoints = 0;
        PublicApproval = 0;
        EcoScore = 0;
        CityEnergy = 0f;
        Money = 0f;
    }

    public Cost(int research, int PublicApproval, int EcoScore, float money, float energy)
    {
        this.ResearchPoints = research;
        this.PublicApproval = PublicApproval;
        this.EcoScore = EcoScore;
        this.CityEnergy = energy;    
        this.Money = money;    
    }

    public static Cost operator *(float a, Cost b)
    {
        return new Cost()
        {
            ResearchPoints = (int)(b.ResearchPoints * a),
            PublicApproval = (int)Mathf.Clamp(b.PublicApproval * a, 0, 100),
            EcoScore = (int)Mathf.Clamp(b.EcoScore * a, 0, 100),
            CityEnergy = b.CityEnergy * a,
            Money = b.Money * a
        };
    }
    public static Cost operator +(Cost a, Cost b)
    {
        return new Cost()
        {
            ResearchPoints = a.ResearchPoints + b.ResearchPoints,
            PublicApproval = Math.Clamp(a.PublicApproval + b.PublicApproval, 0, 100),
            EcoScore = Math.Clamp(a.EcoScore + b.EcoScore, 0, 100),
            CityEnergy = a.CityEnergy + b.CityEnergy,
            Money = a.Money + b.Money
        };
    }
};

