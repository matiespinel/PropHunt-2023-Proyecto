using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    
    int isWalkingHash;
    int isBackingHash;
    int isStrafingLeftHash;
    int isStrafingRightHash;
    //animator puede usar valores hash por sustitucion de int
    bool isBacking;
    bool isWalking;
    bool isStrafingLeft;
    bool isStrafingRight;
    //condiciones
    [SerializeField] GameObject Player;
    [SerializeField] GameObject LookingDirection;
    Animator playerAnimator;
    [SerializeField] Camera MainCamera;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isBackingHash = Animator.StringToHash("isBacking");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isBacking = playerAnimator.GetBool(isBackingHash);
        isWalking = playerAnimator.GetBool(isWalkingHash);
        isStrafingLeft = playerAnimator.GetBool(isStrafingLeftHash);
        isStrafingRight = playerAnimator.GetBool(isStrafingRightHash);
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
             /* if(player.transform.rotation != new Quaternion(0f,90f,0f,0f)){
               player.transform.Rotate(0,-1,0);
             } nofunciona*/
             /* player.transform.rotation = Quaternion.LookRotation(new Vector3(-90,0,0), Vector3.zero); */
             
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
        
       /*  if(MainCamera.transform.rotation.y > 101)
        {
             Quaternion.LookRotation(Vector3.forward, Vector3.left);
        } */
        
       /*  if(MainCamera.transform.rotation.y > player.transform.rotation.y)
        {
          RotateTo(player,45f);
          Debug.Log("mmmo");
        } */
    }
     
}
