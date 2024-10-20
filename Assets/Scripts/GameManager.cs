using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private float publicApproval;
    public float PublicApproval
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
    private float ecoScore;
    public float EcoScore
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
            OnCityEnergyChanged?.Invoke(this, new(value));
        }
    }
    public event EventHandler<OnValueUpdatedArgs> OnCityEnergyChanged;

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

    public TextMeshProUGUI mayorName;
    void Start()
    {
        if(PlayerPrefs.HasKey("Volume")) SoundManager.instance.audioSource.volume = PlayerPrefs.GetFloat("Volume");
        if(PlayerPrefs.HasKey("MayorName")) mayorName.text = PlayerPrefs.GetString("MayorName");
    }

    public void Transaction(Cost c)
    {
        Money += c.Money;
        PublicApproval = (float)PublicApproval + c.PublicApproval;
        EcoScore += c.EcoScore;
        CityEnergy += c.CityEnergy;
        ResearchPoints += c.ResearchPoints;
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
    public int ResearchPoints;
    public float PublicApproval, EcoScore;
    public float CityEnergy, Money;
    public Cost() {
        ResearchPoints = 0;
        PublicApproval = 0;
        EcoScore = 0;
        CityEnergy = 0f;
        Money = 0f;
    }

    public Cost(int research, float PublicApproval, float EcoScore, float money, float energy)
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
            PublicApproval = b.PublicApproval * a,
            EcoScore = b.EcoScore * a,
            CityEnergy = b.CityEnergy * a,
            Money = b.Money * a
        };
    }
    public static Cost operator +(Cost a, Cost b)
    {
        return new Cost()
        {
            ResearchPoints = a.ResearchPoints + b.ResearchPoints,
            PublicApproval = a.PublicApproval + b.PublicApproval,
            EcoScore = a.EcoScore + b.EcoScore,
            CityEnergy = a.CityEnergy + b.CityEnergy,
            Money = a.Money + b.Money
        };
    }

    public override string ToString()
    {
        return $"Cost: {{RPs:{ResearchPoints}, PA: {PublicApproval}, EcoScore: {EcoScore}, Energy: {CityEnergy}, Money: {Money} }}";
    }
};

