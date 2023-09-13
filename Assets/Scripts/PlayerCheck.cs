using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCheck : MonoBehaviour
{
    #region vars
    public AnimationStateController animationStateController;
    public MyCharacterController propController;
    private PhotonView view;
    #endregion
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            if (animationStateController) 
            {
                animationStateController.enabled = true;
            }
            else 
            {
                propController.enabled = true;
            }
            
        }
        else
        {
            if (propController)
            {
                animationStateController.enabled = false;
            }
            else
            {
                propController.enabled = false;
            }
        }
    }
}
