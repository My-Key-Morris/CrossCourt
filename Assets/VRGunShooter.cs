using UnityEngine;

public class VRGunShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    public void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Only fire when the button is fully pressed
        if (!context.performed) return;

        Debug.Log("[VRGunShooter] OnFire() triggered by PlayerInput.");

        if (!projectilePrefab)
        {
            Debug.LogError("[VRGunShooter] No projectile prefab assigned!");
            return;
        }

        if (!firePoint)
        {
            Debug.LogError("[VRGunShooter] No fire point assigned!");
            return;
        }

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if (bullet == null)
        {
            Debug.LogError("[VRGunShooter] Failed to instantiate projectile.");
            return;
        }

        Debug.Log($"[VRGunShooter] Spawned projectile at {firePoint.position}");

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
            Debug.Log($"[VRGunShooter] Set projectile velocity: {rb.linearVelocity}");
        }
        else
        {
            Debug.LogWarning("[VRGunShooter] Projectile has no Rigidbody!");
        }

        Destroy(bullet, 5f);
    }
}
