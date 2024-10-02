using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnPoint1; // The first spawn point
    public Transform spawnPoint2; // The second spawn point
    public float initialSpawnRate = 2f; // Initial spawn rate (in seconds)
    public float minSpawnRate = 0.5f; // Minimum spawn rate (in seconds)
    public float spawnRateDecreasePerWave = 0.5f; // Spawn rate decrease per wave
    public int initialEnemyCount = 5; // Initial number of enemies per wave
    public int enemyCountIncreasePerWave = 5; // Enemy count increase per wave
    public float waveInterval = 10f; // Interval between waves (in seconds)

    private int currentWave = 0; // Current wave number
    private float nextWaveTime; // Time for the next wave
    private bool isFirstWaveStarted = false;
    void Start()
    {
        // Calculate the time for the first wave
        nextWaveTime = Time.time + waveInterval;
    }

    void Update()
    {
        if (isFirstWaveStarted)
        {
            if (Time.time >= nextWaveTime)
            {
                StartNextWave();
            }
        }
    }

    public void StartFirstWave()
    {
        // Only start the first wave if it hasn't already started
        if (currentWave == 0)
        {
            StartNextWave();
            isFirstWaveStarted=true;
        }
    }

    void StartNextWave()
    {
        // Increase the wave count
        currentWave++;

        // Decrease spawn rate and increase enemy count
        initialSpawnRate -= spawnRateDecreasePerWave * currentWave;
        initialSpawnRate = Mathf.Max(minSpawnRate, initialSpawnRate);
        int enemiesToSpawn = initialEnemyCount + (currentWave - 1) * enemyCountIncreasePerWave;

        // Spawn enemies for the wave
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }

        // Calculate the time for the next wave
        nextWaveTime = Time.time + waveInterval;
    }

    void SpawnEnemy()
    {
        // Generate a random position between the two spawn points
        Vector2 spawnPosition = Vector2.Lerp(spawnPoint1.position, spawnPoint2.position, Random.value);

        // Spawn the enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
