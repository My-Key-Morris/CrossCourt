using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    // Settings (editable in the Inspector)
    [SerializeField]
    private float antiGravityDuration = 2.0f; // Duration of the anti-gravity effect in seconds
    [SerializeField]
    private float antiGravityForce = 9.81f;   // Strength of the anti-gravity (matches Earth's gravity for reversal)

    private Rigidbody playerRigidbody;        // Reference to the player's Rigidbody
    private bool isAntiGravityActive = false; // Track if anti-gravity is currently active
    private float antiGravityTimer = 0.0f;    // Timer for the anti-gravity effect

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the player (assumes the player has a "Player" tag)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's Rigidbody
            playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null && !isAntiGravityActive)
            {
                // Activate anti-gravity
                ApplyAntiGravity();
            }
        }
    }

    void ApplyAntiGravity()
    {
        // Set anti-gravity as active
        isAntiGravityActive = true;
        antiGravityTimer = antiGravityDuration;

        // Reverse gravity by setting a positive upward force (opposite of Earth's gravity)
        if (playerRigidbody != null)
        {
            // Disable gravity on the Rigidbody temporarily
            playerRigidbody.useGravity = false;

            // Apply a constant upward force to simulate anti-gravity
            playerRigidbody.linearVelocity = Vector3.zero; // Reset velocity to prevent sudden jerks
            playerRigidbody.AddForce(Vector3.up * antiGravityForce, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        // Handle the timer for the anti-gravity effect
        if (isAntiGravityActive)
        {
            antiGravityTimer -= Time.deltaTime;

            if (antiGravityTimer <= 0)
            {
                // Restore normal gravity
                RestoreGravity();
            }
        }
    }

    void RestoreGravity()
    {
        if (playerRigidbody != null)
        {
            // Re-enable gravity on the Rigidbody
            playerRigidbody.useGravity = true;

            // Optionally reset any custom forces
            playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0, playerRigidbody.linearVelocity.z);
        }

        // Clear the anti-gravity state
        isAntiGravityActive = false;
        playerRigidbody = null;
    }
}