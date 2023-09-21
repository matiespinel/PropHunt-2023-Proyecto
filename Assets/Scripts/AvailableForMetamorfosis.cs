using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableForMetamorfosis : MonoBehaviour
{
    public bool seen = false;
    private Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void OnDirectSight()
    {
        render.material.shader = Shader.Find("Unlit/Outline");
    }

    void OnIndirectSight()
    {
        render.material.shader = Shader.Find("Standard");
    }
    private void LateUpdate()
    {
        if(seen == true)
        {
            OnDirectSight();
        }
        else
        {
            OnIndirectSight();
        }
    }
}
