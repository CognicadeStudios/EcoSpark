using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoScoreBar : MonoBehaviour
{
    public Slider slide;
    private float lerp;
    public GameManager gameManager;

    public void setMaxScore(int approval)
    {
        slide.maxValue = approval;
    }
    void Start()
    {
        setMaxScore(100);
        gameManager.SetEcoScore(100);

    }



    void Update()
    {
        slide.value = Mathf.Lerp(slide.value, gameManager.GetEcoScore(), 0.01f);
    }
}