using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject BulletPrfab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval = 7f;
    [SerializeField] float BulletSpeed = 5f;

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            Bullet();
            timeSinceLastSpawn = 0f;
        }
    }

    void Bullet()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        GameObject Bullet = Instantiate(BulletPrfab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-BulletSpeed, 0f); // Move towards left
        }
        else
        {
            Debug.LogWarning("Jet Prefab doesn't have Rigidbody2D component.");
        }

        Destroy(Bullet,2.5f);
    }
}
