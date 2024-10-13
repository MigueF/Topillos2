using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int initialEnemyCount = 3;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 1f;
    private int currentWave = 0;
    public float spawnRadius = 2.0f;
    private bool isSpawning = false;

    // Diccionario para mapear puntos de spawn a nombres de caminos
    public Dictionary<Transform, string> spawnPointToPathMap;

    void Start()
    {
        isSpawning = false;

        // Inicializar el diccionario de mapeo
        spawnPointToPathMap = new Dictionary<Transform, string>
        {
            { spawnPoints[0], "Waypoints1" },
            { spawnPoints[1], "Waypoints2" },
            { spawnPoints[2], "Waypoints3" },
            { spawnPoints[3], "Waypoints4" }
            // Agrega más mapeos según sea necesario
        };
    }

    public void StartWaves()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            int enemiesToSpawn = initialEnemyCount + currentWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            Enemigodiego enemyScript = enemy.GetComponent<Enemigodiego>();
            if (enemyScript != null)
            {
                if (spawnPointToPathMap.TryGetValue(spawnPoint, out string pathName))
                {
                    enemyScript.pathName = pathName;
                }
                else
                {
                    Debug.LogWarning("No se encontró un camino para el punto de spawn: " + spawnPoint.name);
                }
            }
        }
        else
        {
            Debug.LogWarning("No hay prefab de enemigo o puntos de spawn asignados.");
        }
    }
}
