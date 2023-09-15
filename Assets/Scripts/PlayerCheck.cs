using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCheck : MonoBehaviour
{
    #region vars
    public AnimationStateController animationStateController;
    public MyCharacterController propController;
    [SerializeField] PhotonView padre;
    private PhotonView view;
    #endregion
    void OnTransformParentChanged()
    {
        view = GetComponent<PhotonView>();
        view.TransferOwnership(padre.ViewID);

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
            if (animationStateController)
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
