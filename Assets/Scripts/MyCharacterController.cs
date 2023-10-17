using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;
using System;

public class MyCharacterController : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 10;

    [SerializeField]
    float rSpeed = 10;

    [SerializeField]
    private AudioSource silvido;

    private PhotonView view;
    private CharacterController controller;

    [SerializeField]
    private float mass;

    public Vector3 velocity;

    public bool IsGrounded => controller.isGrounded;

    public bool wasGrounded;
    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {

        if (MetamorfosisScript.isTransformed)
        {
            silvido.Play();
        }
        if (view.IsMine) 
        {
            UpdateMovement();
            UpdateGravity();
            UpdateGround();
        }
        else 
        {
            this.gameObject.GetComponent<RigBuilder>().enabled = false;
            this.enabled = false;
        }

    }

    private void UpdateGround()
    {
        if(wasGrounded != IsGrounded)
        {
            OnGroundStateChange?.Invoke(IsGrounded);
            wasGrounded = IsGrounded;
        }
    }

    private void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.fixedDeltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    private void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);
        if(Input.GetKey(KeyCode.Space))
                {
                    OnBeforeMove?.Invoke();
                }
        controller.Move((input * movementSpeed + velocity) * Time.fixedDeltaTime);
        
        
    }
}
