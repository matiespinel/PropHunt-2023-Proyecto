using System.Collections;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(UniversalCharacterController))]
public class JumpScript : MonoBehaviour
{
    UniversalCharacterController controller;
    [SerializeField]
    private float jumpSpeed;
    private bool tryingToJump;

    private float lastJumpPressTime;
    private float lastGroundedTime;

    [SerializeField]
    private float jumpPressBufferTime = .05f;
    [SerializeField]
    private float jumpGroundGraceTime = .2f;

    Animator _anim;

    PhotonView view;

    void Awake()
    {
        controller = GetComponent<UniversalCharacterController>();
        _anim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if(!view.IsMine) enabled = false;
        if(Input.GetKey(KeyCode.Space))
        {
            tryingToJump = true;
            lastJumpPressTime = Time.time;
            if(_anim) StartCoroutine(JumpAnimation());
            
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

    IEnumerator JumpAnimation()
    {
        _anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.1f);
        _anim.SetBool("isJumping", false);
    }
}
