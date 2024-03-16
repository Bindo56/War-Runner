using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    [SerializeField] GameObject jetPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval = 7f;
    [SerializeField] float jetSpeed = 5f;

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnJet();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnJet()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        GameObject jet = Instantiate(jetPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = jet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-jetSpeed, 0f); // Move towards left
        }
        else
        {
            Debug.LogWarning("Jet Prefab doesn't have Rigidbody2D component.");
        }
    }
}
