using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumps : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float floatTime = 2f; // Time before gravity reactivates
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private LayerMask antiGravityLayers;

    private float normalGravity = Physics.gravity.y;
    private float currentGravity;
    private Vector3 movement;
    private bool isFloating = false;
    private float floatTimer = 0f;
    private bool canJump = true;

    void Start()
    {
        currentGravity = normalGravity;
    }

    void Update()
    {
        bool _isGrounded = IsGrounded(out bool isOnAntiGravity);

        if (_isGrounded)
        {
            if (isFloating)
            {
                movement.y = 0.0f; // Prevents stuck velocity issues when landing
            }
            canJump = true; // Reset jump availability when grounded
        }

        if (_isGrounded && canJump && jumpButton.action.WasPressedThisFrame())
        {
            Jump(isOnAntiGravity);
        }

        if (isFloating)
        {
            floatTimer += Time.deltaTime;
            if (floatTimer >= floatTime)
            {
                isFloating = false;
                currentGravity = normalGravity;
                movement.y = 0f; // Reset vertical velocity to prevent falling instantly
            }
        }
        else
        {
            movement.y += currentGravity * Time.deltaTime;
        }

        cc.Move(movement * Time.deltaTime);
    }

    private void Jump(bool isOnAntiGravity)
    {
        if (!canJump) return; // Prevents multiple jumps when not grounded

        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * normalGravity);
        canJump = false; // Prevents jumping again until grounded

        if (isOnAntiGravity)
        {
            movement.y *= 1.6f;
            isFloating = true;
            floatTimer = 0f;
            currentGravity = 0f; // Disable gravity for the float duration
        }
    }

    private bool IsGrounded(out bool isOnAntiGravity)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f, groundLayers | antiGravityLayers);
        foreach (Collider col in colliders)
        {
            if (((1 << col.gameObject.layer) & antiGravityLayers) != 0)
            {
                isOnAntiGravity = true;
                return true;
            }
            if (((1 << col.gameObject.layer) & groundLayers) != 0)
            {
                isOnAntiGravity = false;
                return true;
            }
        }
        isOnAntiGravity = false;
        return false;
    }
}
