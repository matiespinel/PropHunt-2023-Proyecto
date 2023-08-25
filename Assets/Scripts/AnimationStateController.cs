using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AnimationStateController : MonoBehaviour
{
    
    int isWalkingHash;
    int isBackingHash;
    int isStrafingLeftHash;
    int isStrafingRightHash;
    int isTurningRightHash;
    int isTurningLeftHash;
    //animator puede usar valores hash por sustitucion de int
    bool isBacking;
    bool isWalking;
    bool isStrafingLeft;
    bool isStrafingRight;
    bool isTurningRight;
    bool isTurningLeft;
    //condiciones
    [SerializeField] GameObject Player;
    [SerializeField] GameObject LookingDirection;
    Animator playerAnimator;
    [SerializeField] Camera MainCamera;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
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
        //listeners
        bool sPressed = Input.GetKey(KeyCode.S);
        bool wPressed = Input.GetKey(KeyCode.W);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        //WASD
        //AVANZAR
        if((!isBacking || !isStrafingLeft || !isStrafingRight) && wPressed)
        {
            playerAnimator.SetBool(isWalkingHash, true);
            if(MainCamera.transform.rotation.y !=  Player.transform.rotation.y)
            {
            var directionVector = (new Vector3(LookingDirection.transform.position.x - transform.position.x,0,LookingDirection.transform.position.z - transform.position.z)).normalized;
            Player.transform.rotation = Quaternion.LookRotation(directionVector, Vector3.up);
            }
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
        if(playerAnimator.GetBoneTransform(HumanBodyBones.Head).localRotation.y > 0.23f)
        {
          playerAnimator.SetBool(isTurningRightHash, true);      
        }
        else
        {
          playerAnimator.SetBool(isTurningRightHash, false);  
        }
        if(playerAnimator.GetBoneTransform(HumanBodyBones.Head).localRotation.y < -0.23f)
        {
          playerAnimator.SetBool(isTurningLeftHash, true);      
        }
        else
        {
          playerAnimator.SetBool(isTurningLeftHash, false);  
        }
    }
     
}
