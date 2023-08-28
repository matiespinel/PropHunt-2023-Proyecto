using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCheck : MonoBehaviour
{
    #region vars
    public AnimationStateController animationStateController;
    private PhotonView view;
    #endregion
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
}
