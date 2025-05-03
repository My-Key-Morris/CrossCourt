using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    // Reference to the Rigidbody
    private Rigidbody rb;

    // Initial position and rotation of the king
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Thresholds for movement and rotation
    public float movementThreshold = 1.0f; // Distance in units
    public float rotationThreshold = 45.0f; // Angle in degrees

    // Name of the scene to load
    public string sceneToLoad = "SampleScene"; // Replace with your scene name

    // Flag to prevent multiple scene loads
    private bool hasTriggered = false;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on Chess King Black!");
            return;
        }

        // Store the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Don't proceed if the scene has already been triggered
        if (hasTriggered) return;

        // Check movement
        float distanceMoved = Vector3.Distance(transform.position, initialPosition);
        if (distanceMoved > movementThreshold)
        {
            LoadNewScene();
            return;
        }

        // Check rotation (tilt)
        float angleDifference = Quaternion.Angle(transform.rotation, initialRotation);
        if (angleDifference > rotationThreshold)
        {
            LoadNewScene();
        }
    }

    void LoadNewScene()
    {
        hasTriggered = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}