using UnityEngine;

public class RL_movement : MonoBehaviour
{
    // Movement settings (editable in the Inspector)
    [SerializeField]
    private float moveDistance = 2.0f; // How far the platform moves (in units)
    [SerializeField]
    private float moveSpeed = 2.0f;    // Speed of movement
    [SerializeField]
    private bool moveRightFirst = true; // Start by moving right or left

    private Vector3 startPosition;      // Initial position of the platform
    private float moveDirection;        // 1 for right, -1 for left

    void Start()
    {
        // Record the starting position
        startPosition = transform.position;

        // Set initial direction based on the moveRightFirst flag
        moveDirection = moveRightFirst ? 1.0f : -1.0f;
    }

    void Update()
    {
        // Calculate the new position with oscillation
        float newX = startPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveDistance * moveDirection;

        // Update the platform's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Optional: Reverse direction when reaching the end (using a simple toggle)
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveDistance)
        {
            moveDirection *= -1; // Reverse direction
        }
    }
}