using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    private float jumpSpeed;
    [SerializeField]
    private float groundCheckDistance;

    [SerializeField]
    private LayerMask ignoreLayer;

    private bool isGrounded;

    [SerializeField]
    private int YTransformOffset;

    [SerializeField]
    private KeyCode jumpKey;

    private Rigidbody rigidBody;

    private Vector3 rayOrigin;
    private bool tryingToJump;
    private float lastJumpPressTime;
    private float jumpPressBufferTime = 0.5f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rayOrigin = new Vector3(transform.position.x, transform.position.y + YTransformOffset, transform.position.z);
        Ray ray = new Ray(rayOrigin, Vector3.down);
        isGrounded = Physics.Raycast(ray, groundCheckDistance, ignoreLayer);
        bool canJump = Input.GetKey(jumpKey) && isGrounded;
        Debug.Log(isGrounded);
        Debug.Log(canJump);
        Debug.DrawRay(rayOrigin, Vector3.down, Color.red);
        if (canJump) 
        {
            rigidBody.AddForce(new Vector3(0,100,0));
            Debug.Log("SALTO");
        }

    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    private void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;

        bool isOrWasTryingToJump = tryingToJump || (wasTryingToJump && isGrounded);

        bool isOrWasGrounded = isGrounded; //|| wasgrounded blbla compleTAR;
        if(isOrWasTryingToJump && isGrounded) 
        {
            rigidBody.AddForce(0f,jumpSpeed,0f);
        }
        tryingToJump = false;
    }

    void OnGroundStateChange(bool isgrounded) 
    {
        //no terminado
    }
}
