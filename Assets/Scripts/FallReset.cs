using UnityEngine;
using UnityEngine.SceneManagement;

public class FallReloadScene : MonoBehaviour
{
    public float fallThreshold = -10f; // Height below which the scene reloads

    void Update()
    {
        // Check if the player's Y position is below the threshold
        if (transform.position.y < fallThreshold)
        {
            ReloadInteractionRoom();
        }
    }

    void ReloadInteractionRoom()
    {
        // Reload the "InteractionRoom" scene
        SceneManager.LoadScene("InteractionRoom");
    }
}