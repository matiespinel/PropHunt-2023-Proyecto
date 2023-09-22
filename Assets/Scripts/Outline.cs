using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    public void ToggleHighlight(bool seen)
    {
        if (seen) 
        {
            render.material.shader = Shader.Find("Unlit/Outline");
        }
        else 
        {
            render.material.shader = Shader.Find("Standard");
        }
    }
}
