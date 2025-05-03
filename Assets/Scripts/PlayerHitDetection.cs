using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerHitDetection : MonoBehaviour
{
    private List<InputDevice> _inputDevices = new List<InputDevice>();
    public int playerHealth = 3; // Set player health
    public float fallThreshold = -10f; // Adjust based on platform height

    // Vibration settings
    private float lowAmpVibration = 0.5f;  // Low amplitude for ground
    private float highAmpVibration = 1f; // High amplitude for dodgeball
    private float vibrationDuration = 0.2f; // Duration in seconds

    void Start()
    {
        // Initialize input devices list
        InputDevices.GetDevicesWithCharacteristics(
            InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand,
            _inputDevices);
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Debug.Log("Player fell off the platform! Restarting Scene...");
            RestartScene();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dodgeball"))
        {
            Debug.Log("Player was hit by a dodgeball!");
            TriggerVibration(highAmpVibration);
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("Player was hit by a ghost!");
            TriggerVibration(highAmpVibration);
            RestartScene();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player hit the ground!");
            TriggerVibration(lowAmpVibration);
        }
    }

    void TriggerVibration(float amplitude)
    {
        // Send haptic impulse to all controllers
        foreach (InputDevice device in _inputDevices)
        {
            if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    device.SendHapticImpulse(0, amplitude, vibrationDuration);
                }
            }
        }
    }

    void TakeDamage()
    {
        playerHealth--;
        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Player is out! Restarting Scene...");
            RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}