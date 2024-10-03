using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefab del enemigo a spawnear
    public GameObject enemyPrefab;

    // Puntos de spawn (asignados en el Inspector)
    public Transform[] spawnPoints;

    // Configuración de las oleadas
    public int initialEnemyCount = 3;   // Número de enemigos en la primera oleada
    public float timeBetweenWaves = 5f; // Tiempo entre oleadas
    public float timeBetweenSpawns = 1f; // Tiempo entre spawneos dentro de una oleada
    private int currentWave = 0; // Contador de la oleada actual

    // Radio aleatorio alrededor del spawn point
    public float spawnRadius = 2.0f; // Define el radio para randomizar la posición
    private bool isSpawning = false;

    void Start()
    {
        // Comenzar el ciclo de oleadas
        isSpawning = false;
    }

    public void StartWaves()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    // Coroutine que maneja el spawneo de oleadas
    IEnumerator SpawnWaves()
    {
        // Bucle infinito para las oleadas
        while (true)
        {
            // Incrementar número de enemigos por cada oleada
            currentWave++;
            int enemiesToSpawn = initialEnemyCount + currentWave;

            // Spawnear enemigos de forma progresiva
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();

                // Esperar un tiempo antes de spawnear el siguiente enemigo
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            // Esperar antes de la siguiente oleada
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    // Método para spawnear un enemigo en uno de los puntos aleatorios
    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoints.Length > 0)
        {
            // Seleccionar un punto de spawn aleatorio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Crear una posición aleatoria dentro del radio alrededor del punto de spawn
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0; // Asegurarse de que no se mueva en el eje Y (plano)

            // Determinar la posición final del enemigo
            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            // Instanciar el enemigo en la posición randomizada del punto de spawn
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay prefab de enemigo o puntos de spawn asignados.");
        }
    }
}
