using UnityEngine;

[RequireComponent(typeof(UniversalCharacterController))]
public class Sprint : MonoBehaviour
{
    UniversalCharacterController mController;

    [SerializeField]
    float speedMultiplier = 2f;

    private void Awake()
    {
        mController = GetComponent<UniversalCharacterController>();
    }

    private void OnEnable() => mController.OnBeforeMove += OnBeforeMove;
    private void OnDisable() => mController.OnBeforeMove -= OnBeforeMove;

    void OnBeforeMove()
    {
        var sprintInput = Input.GetAxis("Fire3");
        if (sprintInput == 0) return;
        var forwardMovementFactor = Mathf.Clamp01(Vector3.Dot(transform.forward, mController.velocity.normalized));
        var multiplier = Mathf.Lerp(1f, speedMultiplier, forwardMovementFactor);
        mController.movementSpeedMultiplier *= multiplier;
    }
}
