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
        if (sprintInput == 0) return;
        Debug.Log("funciono?");
        var forwardMovementFactor = Mathf.Clamp01(Vector3.Dot(transform.forward, mController.velocity.normalized));
        var multiplier = Mathf.Lerp(forwardMovementFactor, speedMultiplier, 1f);
        mController.movementSpeedMultiplier *= sprintInput > 0 ? multiplier : 1f;
        Debug.Log(multiplier);

    }
}
