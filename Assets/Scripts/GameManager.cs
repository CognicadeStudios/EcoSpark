using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public static int money;
    public static int publicApproval;
    public static int ecoScore;
    public static int cityEnergy;

    void Awake()
    {
        Instance = this;
    }

    public void SetMoney(int newState) 
    {
        money = newState;
    }

    public void SetApproval(int approval) 
    {
        publicApproval = approval;
    }

    public void SetEcoScore(int score) 
    {
        ecoScore = score;
    }

    public void SetEnergy(int energy) 
    {
        cityEnergy = energy;
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetPublicApproval()
    {
        return publicApproval; 
    }

    public int GetEcoScore()
    {
        return ecoScore;
    }

    public int GetCityEnergy()
    {
        return cityEnergy;
    }
}