using UnityEngine;

public class FW_Movement : MonoBehaviour
{
    // Movement settings (editable in the Inspector)
    [SerializeField]
    private float moveDistance = 2.0f; // How far the platform moves forward/backward (in units)
    [SerializeField]
    private float moveSpeed = 2.0f;    // Speed of movement
    [SerializeField]
    private bool moveForwardFirst = true; // Start by moving forward or backward

    private Vector3 startPosition;      // Initial position of the platform
    private float moveDirection;        // 1 for forward, -1 for backward

    void Start()
    {
        // Record the starting position
        startPosition = transform.position;

        // Set initial direction based on the moveForwardFirst flag
        moveDirection = moveForwardFirst ? 1.0f : -1.0f;
    }

    void Update()
    {
        // Calculate the new position with sinusoidal movement along the Z-axis
        float newZ = startPosition.z + Mathf.Sin(Time.time * moveSpeed) * moveDistance * moveDirection;

        // Update the platform's position (only Z changes, X and Y stay the same)
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}