using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCheck : MonoBehaviour
{
    public AnimationStateController animationStateController;
    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            animationStateController.enabled = true;
        }
        else
        {
            animationStateController.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
