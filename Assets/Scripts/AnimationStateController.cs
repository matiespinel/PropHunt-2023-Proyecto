using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;
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

    [SerializeField] 
    private GameObject Player;

    [SerializeField]
    private GameObject LookingDirection;


    [SerializeField]
    private Camera MainCamera;

    private Animator playerAnimator;
    private PhotonView view;
    
    void Start()
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
            bool sPressed = Input.GetKey(KeyCode.S);
            bool wPressed = Input.GetKey(KeyCode.W);
            bool aPressed = Input.GetKey(KeyCode.A);
            bool dPressed = Input.GetKey(KeyCode.D);
            //WASD
            //AVANZAR
            if((!isBacking || !isStrafingLeft || !isStrafingRight) && wPressed)
            {
                playerAnimator.SetBool(isWalkingHash, true);
                Steer();
            }
            else
            {
                playerAnimator.SetBool(isWalkingHash, false);
            }
            //RETROCEDER
            if((!isWalking || !isStrafingLeft || !isStrafingRight) && sPressed)
            {
                 playerAnimator.SetBool(isBackingHash, true);
                Steer();
            }
            else
            {
                 playerAnimator.SetBool(isBackingHash, false);
            }
            //LADO IZQUIERDO
            if((!isBacking || !isWalking || !isStrafingRight) && aPressed)
            {
                 playerAnimator.SetBool(isStrafingLeftHash, true);
                Steer();
            }
            else
            {
                 playerAnimator.SetBool(isStrafingLeftHash, false);
            }
            //LADO DERECHO
            if((!isBacking || !isWalking || !isStrafingLeft) && dPressed)
            {
                playerAnimator.SetBool(isStrafingRightHash, true);
                Steer();
            }
            else
            {
                 playerAnimator.SetBool(isStrafingRightHash, false);
            }
            if(playerAnimator.GetBoneTransform(HumanBodyBones.Head).localRotation.y > 0.23f)
            {
              playerAnimator.SetBool(isTurningRightHash, true);
              Steer();
            }
            else
            {
              playerAnimator.SetBool(isTurningRightHash, false);  
            }
            if(playerAnimator.GetBoneTransform(HumanBodyBones.Head).localRotation.y < -0.23f)
            {
              playerAnimator.SetBool(isTurningLeftHash, true);
              Steer();
            }
            else
            {
              playerAnimator.SetBool(isTurningLeftHash, false);  
            }

        }
        else 
        {
            //this.gameObject.GetComponent<RigBuilder>().enabled = false;
            LookingDirection.transform.parent = this.gameObject.transform.parent;
            this.enabled = false;
        }

        
    }
    private void Steer() 
    {
        if(MainCamera.transform.rotation.y !=  Player.transform.rotation.y)
        {
            var directionVector = (new Vector3(LookingDirection.transform.position.x - transform.position.x,0,LookingDirection.transform.position.z - transform.position.z)).normalized;
            Player.transform.rotation = Quaternion.LookRotation(directionVector, Vector3.up);
        }
    }
}
