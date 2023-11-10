using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;
using System;

public class UniversalCharacterController : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 10;

    [SerializeField]
    bool prop;

    [SerializeField]
    private AudioSource silvido;

    private PhotonView view;
    private CharacterController controller;

    [SerializeField]
    private float mass;

    [SerializeField]
    float maxRot = -0.19f;
    [SerializeField]
    float minRot = -0.33f;

    internal Vector3 velocity;

    public bool IsGrounded => controller.isGrounded;

    public bool wasGrounded;
    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;

    [SerializeField]
    private GameObject lookAt;

    bool toggleRot = true;

    internal float movementSpeedMultiplier;

    [SerializeField]
    Animator animator;
    [SerializeField] float acceleration = 20f;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        silvido = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                toggleRot = !toggleRot;
            }
        }
        else 
        {
            enabled = false;
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

    Vector3 GetMovementInput() 
    {
         var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed * movementSpeedMultiplier;

        
        return input;
    }
    public void UpdateMovement()
    {
        
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();
        var input = GetMovementInput();

        var factor = acceleration * Time.fixedDeltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        controller.Move(velocity * Time.fixedDeltaTime);

        if(toggleRot && Input.anyKey && prop)
        {
            RecenterForward();
        }
        else if(animator)
        {
            if(animator?.GetBoneTransform(HumanBodyBones.Head).localRotation.y > maxRot || animator?.GetBoneTransform(HumanBodyBones.Head).localRotation.y < minRot || !prop && Input.GetAxis("Horizontal") != 0 || !prop && Input.GetAxis("Vertical") != 0)
            {
                RecenterForward();
            }
        }

        
    }

    void RecenterForward()
    {
        var directionVector = (lookAt.transform.position - transform.position).normalized;
        var rotGoal = Quaternion.LookRotation(new Vector3(directionVector.x,0f ,directionVector.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,rotGoal,.05f);
    }

}
