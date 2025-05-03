using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomTargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int targetsPerSurface = 3;
    public float spawnDistance = 0.05f;

    IEnumerator Start()
    {
        Debug.Log("[TargetSpawner] Waiting for mesh surfaces...");

        // Wait until at least one mesh appears
        while (FindObjectsOfType<MeshRenderer>().Length == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f); // Extra wait for EffectMesh setup

        if (!targetPrefab)
        {
            Debug.LogError("[TargetSpawner] No target prefab assigned.");
            yield break;
        }

        Renderer targetRenderer = targetPrefab.GetComponent<Renderer>();
        if (!targetRenderer)
        {
            Debug.LogError("[TargetSpawner] Target prefab has no Renderer.");
            yield break;
        }

        Vector3 targetSize = targetRenderer.bounds.size;
        float targetWidth = targetSize.x;
        float targetHeight = targetSize.y;

        Debug.Log($"[TargetSpawner] Target size: {targetWidth:F2} x {targetHeight:F2}");

        var meshObjects = FindObjectsOfType<MeshRenderer>();
        Debug.Log($"[TargetSpawner] MeshRenderer count: {meshObjects.Length}");

        int surfaceCount = 0;
        int totalTargetsSpawned = 0;

        foreach (var mesh in meshObjects)
        {
            if (!mesh.gameObject.name.EndsWith("_EffectMesh")) continue;

            Bounds wallBounds = mesh.localBounds;
            Vector3 wallSize = wallBounds.size;

            // Skip walls that can't fit even one target
            if (wallSize.x < targetWidth || wallSize.y < targetHeight)
            {
                Debug.Log($"[TargetSpawner] Skipping {mesh.name} (too small: {wallSize.x:F2} x {wallSize.y:F2})");
                continue;
            }

            surfaceCount++;
            Debug.Log($"[TargetSpawner] Spawning on: {mesh.name}");

            List<Bounds> placedBounds = new List<Bounds>();
            int attempts = 0;
            int maxAttempts = targetsPerSurface * 10;

            while (placedBounds.Count < targetsPerSurface && attempts < maxAttempts)
            {
                attempts++;

                float x = Random.Range(-wallSize.x / 2f + targetWidth / 2f, wallSize.x / 2f - targetWidth / 2f);
                float y = Random.Range(-wallSize.y / 2f + targetHeight / 2f, wallSize.y / 2f - targetHeight / 2f);
                float z = 0f;

                Vector3 localOffset = new Vector3(x, y, z);
                Bounds candidate = new Bounds(localOffset, new Vector3(targetWidth, targetHeight, 0.01f));

                bool overlaps = false;
                foreach (var b in placedBounds)
                {
                    if (b.Intersects(candidate))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (overlaps) continue;

                Vector3 spawnPoint = mesh.transform.TransformPoint(localOffset + mesh.transform.forward * spawnDistance);
                Vector3 normal = -mesh.transform.forward;

                var target = Instantiate(targetPrefab, spawnPoint, Quaternion.LookRotation(normal));
                target.transform.SetParent(mesh.transform);

                Debug.Log($"[TargetSpawner] Spawned target {placedBounds.Count + 1} on {mesh.name} at {spawnPoint}");
                placedBounds.Add(candidate);
                totalTargetsSpawned++;
            }
        }

        Debug.Log($"[TargetSpawner] ✅ Done. Spawned {totalTargetsSpawned} targets on {surfaceCount} surfaces.");
    }
}