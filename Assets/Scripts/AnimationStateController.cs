using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    
    int isWalkingHash;
    int isBackingHash;
    int isStrafingLeftHash;
    int isStrafingRightHash;
    public GameObject player;
    public GameObject LookingDirection;
    Animator playerAnimator;
    public Camera MainCamera;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isBackingHash = Animator.StringToHash("isBacking");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
    }

    void Update()
    {
        //listeners
        bool sPressed = Input.GetKey(KeyCode.S);
        bool wPressed = Input.GetKey(KeyCode.W);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        //hashes
        bool isBacking = playerAnimator.GetBool(isBackingHash);
        bool isWalking = playerAnimator.GetBool(isWalkingHash);
        bool isStrafingLeft = playerAnimator.GetBool(isStrafingLeftHash);
        bool isStrafingRight = playerAnimator.GetBool(isStrafingRightHash);
        //WASD
        //AVANZAR
        if((!isBacking || !isStrafingLeft || !isStrafingRight) && wPressed)
        {
            playerAnimator.SetBool(isWalkingHash, true);
            if(MainCamera.transform.rotation.y !=  player.transform.rotation.y)
            {
            var directionVector = (new Vector3(LookingDirection.transform.position.x - transform.position.x,0,LookingDirection.transform.position.z - transform.position.z)).normalized;
            player.transform.rotation = Quaternion.LookRotation(directionVector, Vector3.up);
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
        

       /*  if(MainCamera.transform.rotation.y > player.transform.rotation.y)
        {
          RotateTo(player,45f);
          Debug.Log("mmmo");
        } */
    }
    private void RotateTo(GameObject gameobject, float angle)
    {
     while(gameObject.transform.rotation.y != angle)
     {
          gameobject.transform.Rotate(0,angle,0); 
     }
     }
     
}
