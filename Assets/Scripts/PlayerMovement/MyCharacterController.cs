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
    bool prop;

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

    [SerializeField]
    private GameObject lookAt;

    bool toggleRot = true;

    internal float movementSpeedMultiplier;

    [SerializeField]
    Animator animator;

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

    public void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed * movementSpeedMultiplier;

        controller.Move((input * movementSpeed + velocity) * Time.fixedDeltaTime);

        if (toggleRot && Input.anyKey && prop || animator.GetBoneTransform(HumanBodyBones.Head).localRotation.y > -0.21f || animator.GetBoneTransform(HumanBodyBones.Head).localRotation.y < -0.33f || !prop && Input.GetAxis("Horizontal") != 0 || !prop && Input.GetAxis("Vertical") != 0)
        {
            var directionVector = (new Vector3(lookAt.transform.position.x - transform.position.x, 0, lookAt.transform.position.z - transform.position.z)).normalized;
            var lookatquat = Quaternion.LookRotation(directionVector, Vector3.up);
            SmoothRotate(transform.rotation, lookatquat, 10f);
        }

        
    }

    void SmoothRotate(Quaternion currentrot, Quaternion targetrot, float speed) 
    {
        float rspeed = speed * (1f - Mathf.Exp(-Time.deltaTime));
        for (int i = 0 ; i < speed; i++) 
        {
            transform.rotation = Quaternion.Slerp(currentrot, targetrot, rspeed);
        }
    }

}
