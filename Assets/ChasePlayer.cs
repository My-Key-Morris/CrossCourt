using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform player;         // Player to chase
    public float detectionRadius = 5f;  // How close the player has to be to start chasing
    public float moveSpeed = 2f;        // Speed of enemy movement

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
