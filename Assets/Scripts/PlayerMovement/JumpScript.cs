using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class JumpScript : MonoBehaviour
{
    MyCharacterController controller;
    [SerializeField]
    private float jumpSpeed;
    private bool tryingToJump;

    private float lastJumpPressTime;
    private float lastGroundedTime;

    [SerializeField]
    private float jumpPressBufferTime = .05f;
    [SerializeField]
    private float jumpGroundGraceTime = .2f;

    

    void Awake()
    {
        controller = GetComponent<MyCharacterController>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            tryingToJump = true;
            lastJumpPressTime = Time.time;
        }
        
    }

    private void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;
        bool wasGrounded = Time.time - lastGroundedTime < jumpGroundGraceTime;

        bool isOrWasTryingToJump = tryingToJump || (wasTryingToJump && controller.IsGrounded);
        bool isOrWasGrounded = controller.IsGrounded || wasGrounded; 

        if(isOrWasTryingToJump && isOrWasGrounded) 
        {
            Debug.Log(isOrWasGrounded);
            Debug.Log(isOrWasTryingToJump);
            controller.velocity.y += jumpSpeed;
        }

        tryingToJump = false;
    }

    void OnGroundStateChange(bool isgrounded) 
    {
        if(!isgrounded) lastGroundedTime = Time.time;
    }

    void OnEnable()
    {
        controller.OnBeforeMove += OnBeforeMove;
        controller.OnGroundStateChange += OnGroundStateChange;
    }
    void OnDisable()
    {
        controller.OnBeforeMove -= OnBeforeMove;
        controller.OnGroundStateChange -= OnGroundStateChange;
    }
}
