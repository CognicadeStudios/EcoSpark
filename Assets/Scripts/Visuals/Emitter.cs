using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Vector3 emissionColor;
    [SerializeField]
    private bool isRandom, isNightOnly;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                meshRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
                if(meshRenderer != null) break;
            }
        }
        
        if(isRandom)
        {
            emissionColor.x = UnityEngine.Random.Range(1.0f, 3.7f);
            emissionColor.y = UnityEngine.Random.Range(1.0f, 3.7f);
            emissionColor.z = UnityEngine.Random.Range(1.0f, 3.7f);
        }
    }

    void Update()
    {
        Vector3 cur = emissionColor;
        if(isNightOnly && LightingManager.instance.TimeOfDay > 6f && LightingManager.instance.TimeOfDay < 18f) cur = new Vector3(1.0f, 1.0f, 1.0f);
        meshRenderer.material.color = new Color(cur.x, cur.y, cur.z, 1.0f);
    }
}
