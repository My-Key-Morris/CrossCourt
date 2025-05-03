using UnityEngine;
using UnityEngine.SceneManagement;

public class winner : MonoBehaviour
{
    // The name of the scene to load (set this in the Unity Inspector or hardcode it)
    [SerializeField]
    private string lobbySceneName = "SampleScene";

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player (assumes the player has a "Player" tag)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Load the lobby scene
            SceneManager.LoadScene(lobbySceneName);
        }
    }
}