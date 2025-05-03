using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetTracker : MonoBehaviour
{
    public int targetsToHit = 10;
    private int currentHits = 0;

    public void RegisterTargetHit()
    {
        currentHits++;
        Debug.Log($"[TargetTracker] Target hit! Total: {currentHits}/{targetsToHit}");

        if (currentHits >= targetsToHit)
        {
            Debug.Log("[TargetTracker] Goal reached! Loading next scene...");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Load next scene in build settings, or use a named scene
        SceneManager.LoadScene(0);
    }
}