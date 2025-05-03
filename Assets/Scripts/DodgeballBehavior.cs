using UnityEngine;

public class DodgeballBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;           // Speed of the dodgeball
    [SerializeField]
    private float lifetime = 5.0f;         // How long the dodgeball exists before destroying
    private Transform player;              // Reference to the player
    private Rigidbody rb;                 // Rigidbody for 3D physics

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on dodgeball!");
            return; // Skip further execution if Rigidbody is missing
        }

        // Set initial velocity toward the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
            Debug.Log("Dodgeball launched toward player at position: " + player.position);
        }
        else
        {
            Debug.LogWarning("Player not found! Dodgeball moving downward as default.");
            rb.linearVelocity = Vector3.down * speed; // Default movement if player isn’t found
        }

        // Destroy the dodgeball after its lifetime
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the dodgeball hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerHit();
        }
        // else if (!collision.gameObject.CompareTag("Goal")) // Ignore collisions with the goal
        // {
        //     Destroy(gameObject); // Destroy on collision with anything else (e.g., platforms)
        // }
    }

    void HandlePlayerHit()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            Vector3 knockback = (player.position - transform.position).normalized * 10.0f;
            playerRb.linearVelocity = knockback;

            Transform startPoint = GameObject.FindGameObjectWithTag("StartPoint")?.transform;
            if (startPoint != null)
            {
                player.position = startPoint.position;
            }

            // Notify GameManager (if it exists)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerHit();
            }
            else
            {
                Debug.LogWarning("GameManager.Instance is null! Lives system not available.");
            }
        }
        Destroy(gameObject); // Destroy dodgeball after hitting player
    }
}