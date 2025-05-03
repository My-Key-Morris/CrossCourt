using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[Projectile] Hit: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Target"))
        {
            FindObjectOfType<TargetTracker>()?.RegisterTargetHit();
            Destroy(collision.gameObject); // destroy the target
            Debug.Log("[Projectile] Target destroyed!");
        }

        Destroy(gameObject); // destroy the projectile itself
    }
}
