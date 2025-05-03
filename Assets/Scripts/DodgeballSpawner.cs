using UnityEngine;

public class DodgeballSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject dodgeballPrefab;    // The dodgeball prefab
    [SerializeField]
    private float spawnInterval = 2.0f;    // Time between spawns
    [SerializeField]
    private Vector2 spawnArea = new Vector2(5.0f, 5.0f); // Area around the spawner to spawn dodgeballs

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnDodgeball();
            timer = 0.0f;
        }
    }

    void SpawnDodgeball()
    {
        // Random position within the spawn area
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y)
        );

        // Instantiate the dodgeball
        Instantiate(dodgeballPrefab, spawnPosition, Quaternion.identity);
    }
}