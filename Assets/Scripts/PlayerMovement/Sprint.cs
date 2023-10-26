using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCharacterController))]
public class Sprint : MonoBehaviour
{
    MyCharacterController mController;

    [SerializeField]
    float speedMultiplier = 2f;

    private void Awake()
    {
        mController = GetComponent<MyCharacterController>();
    }

    private void OnEnable() => mController.OnBeforeMove += OnBeforeMove;
    private void OnDisable() => mController.OnBeforeMove -= OnBeforeMove;

    void OnBeforeMove() 
    {
        var sprintInput = Input.GetAxis("Fire3");
        mController.movementSpeedMultiplier *= sprintInput > 0 ? speedMultiplier : 1f;

    }
}
