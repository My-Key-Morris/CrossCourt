using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int lives = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerHit()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart level
        }
        else
        {
            // Reset player to start (assuming a "StartPoint" tagged object exists)
            Transform startPoint = GameObject.FindGameObjectWithTag("StartPoint")?.transform;
            if (startPoint != null)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = startPoint.position;
            }
        }
    }
}