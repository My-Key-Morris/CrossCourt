using UnityEngine;
using System.Collections;

public class CannonShooter : MonoBehaviour
{
    [SerializeField]
    public GameObject dodgeballPrefab; // Assign this in the Inspector
    public Transform firePoint; // The spawn point of the dodgeball
    public float shootForce = 10f; // The force applied to the dodgeball
    public float fireRate = 3f; // Time between shots
    [SerializeField] private float startDelay = 1f;

    void Start()
    {
        // Start shooting repeatedly every fireRate seconds
        InvokeRepeating(nameof(FireDodgeball), startDelay, fireRate);
    }

    void FireDodgeball() {
    if (dodgeballPrefab != null && firePoint != null)
    {
        GameObject dodgeball = Instantiate(dodgeballPrefab, firePoint.position, firePoint.rotation);

        dodgeball.transform.localScale = transform.localScale * 1.5f    ;

        Rigidbody rb = dodgeball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Use TransformDirection to ensure it fires correctly relative to the cannon's facing direction
            Vector3 shootDirection = firePoint.TransformDirection(Vector3.forward);
            rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        }

        Destroy(dodgeball, 3.5f);
    }
}

}
