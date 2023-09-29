using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LookingTargetRecenter : MonoBehaviour
{
    [SerializeField]
    private GameObject lookingTarget;

    [SerializeField]
    private LayerMask ignoreLayer;

    private Camera cam;
    private PhotonView view;
    private RaycastHit hit;
    void Start()
    {
        cam = GetComponent<Camera>();
        view = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine) 
        {
 
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, 100, ignoreLayer))
            {
                if (lookingTarget.transform.position != hit.point)
                {
                    Debug.Log("vvv");
                    lookingTarget.transform.position = hit.collider.transform.position;
                }
               
            }
        }
    }
}
