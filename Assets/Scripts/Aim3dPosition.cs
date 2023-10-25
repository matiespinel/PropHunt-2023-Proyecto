using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim3dPosition : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    LayerMask layerMask;

    void Awake()
    {
        cam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f,0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,20f, layerMask)) 
        {
            if (hit.distance > 4) 
            {
                Debug.Log("hit");
                transform.position = hit.point;
            }
           
        }
    }
}
