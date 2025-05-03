using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public string targetSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>()) // Detect the XR player
        {
            Debug.Log("XR Player detected, loading scene: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
