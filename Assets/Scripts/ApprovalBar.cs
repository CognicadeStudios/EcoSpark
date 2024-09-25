using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApprovalBar : MonoBehaviour
{
    public Slider slide;
    private float lerp;
    public GameManager gameManager;
    
    public void setMaxApproval(int approval)
    {
        slide.maxValue = approval;
    }
    void Start()
    {
        setMaxApproval(100);
        gameManager.SetApproval(100);
   
    }


    
    void Update() { 
            slide.value = Mathf.Lerp(slide.value, gameManager.GetPublicApproval(), 0.01f);
    }
}