using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;
using System;
using Cinemachine;
public class AnimationStateController : MonoBehaviour
{
    #region hashes

    private int isWalkingHash;
    private int isBackingHash;
    private int isStrafingLeftHash;
    private int isStrafingRightHash;
    private int isTurningRightHash;
    private int isTurningLeftHash;
    #endregion
    //animator puede usar valores hash por sustitucion de int
    # region conditional bools

    private bool isBacking;
    private bool isWalking;
    private bool isStrafingLeft;
    private bool isStrafingRight;
    private bool isTurningRight;
    private bool isTurningLeft;
    #endregion
    //condiciones  

    private Animator playerAnimator;
    private PhotonView view;

    
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        #region Hashes
        
        isWalkingHash = Animator.StringToHash("isWalking");
        isBackingHash = Animator.StringToHash("isBacking");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isTurningRightHash = Animator.StringToHash("isTurningRight");
        isTurningLeftHash = Animator.StringToHash("isTurningLeft");
        #endregion
        #region Bools
        isBacking = playerAnimator.GetBool(isBackingHash);
        isWalking = playerAnimator.GetBool(isWalkingHash);
        isStrafingLeft = playerAnimator.GetBool(isStrafingLeftHash);
        isStrafingRight = playerAnimator.GetBool(isStrafingRightHash);
        isTurningRight = playerAnimator.GetBool(isTurningRightHash);
        isTurningLeft = playerAnimator.GetBool(isTurningLeftHash);
        #endregion
    }

    void FixedUpdate()
    {
        if (view.IsMine) 
        {
            
            //listeners
            bool spacePressed = Input.GetKey(KeyCode.Space);
            bool sPressed = Input.GetKey(KeyCode.S);
            bool wPressed = Input.GetKey(KeyCode.W);
            bool aPressed = Input.GetKey(KeyCode.A);
            bool dPressed = Input.GetKey(KeyCode.D);
            //WASD
            //AVANZAR
            if((!isBacking || !isStrafingLeft || !isStrafingRight) && wPressed)
            {
                playerAnimator.SetBool(isWalkingHash, true);
            }
            else
            {
                playerAnimator.SetBool(isWalkingHash, false);
            }
            //RETROCEDER
            if((!isWalking || !isStrafingLeft || !isStrafingRight) && sPressed)
            {
                 playerAnimator.SetBool(isBackingHash, true);
            }
            else
            {
                 playerAnimator.SetBool(isBackingHash, false);
            }
            //LADO IZQUIERDO
            if((!isBacking || !isWalking || !isStrafingRight) && aPressed)
            {
                 playerAnimator.SetBool(isStrafingLeftHash, true);
            }
            else
            {
                 playerAnimator.SetBool(isStrafingLeftHash, false);
            }
            //LADO DERECHO
            if((!isBacking || !isWalking || !isStrafingLeft) && dPressed)
            {
                playerAnimator.SetBool(isStrafingRightHash, true);
            }
            else
            {
                 playerAnimator.SetBool(isStrafingRightHash, false);
            }
            

        }
        else 
        {
            //this.gameObject.GetComponent<RigBuilder>().enabled = false;
            //LookingDirection.transform.parent = this.gameObject.transform.parent;
            this.enabled = false;
        }

    }
}
