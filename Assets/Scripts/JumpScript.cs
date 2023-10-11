using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    private float jumpForce;
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
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
}
