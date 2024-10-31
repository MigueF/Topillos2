using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public int enemyType;         // Índice del tipo de enemigo (en la lista de prefabs)
    public int spawnPointIndex;   // Índice del punto de spawn (en la lista de spawners)
    public int enemyCount;        // Cantidad de enemigos de este tipo
}

[System.Serializable]
public class WaveConfig
{
    public List<EnemyWave> enemiesInWave; // Lista de grupos de enemigos que salen en esta oleada
}

public class EnemyWaveSpawner : MonoBehaviour
{
    // Prefabs de los diferentes tipos de enemigos
    public GameObject[] enemyPrefabs;

    // Puntos de spawn (definidos en el Inspector)
    public Transform[] spawnPoints;

    // Lista de oleadas
    public List<WaveConfig> waves;

    // Configuración de tiempos entre oleadas y spawns
    public float timeBetweenWaves = 5f;   // Tiempo entre oleadas
    public float timeBetweenSpawns = 1f;  // Tiempo entre spawns de enemigos dentro de una oleada

    private int currentWaveIndex = 0;     // Índice de la oleada actual

    void Start()
    {
        // Comenzar la secuencia de oleadas
        StartCoroutine(SpawnWaves());
    }

    // Coroutine para manejar el ciclo de oleadas
    IEnumerator SpawnWaves()
    {
        // Mientras haya oleadas por spawnear
        while (currentWaveIndex < waves.Count)
        {
            WaveConfig currentWave = waves[currentWaveIndex];

            // Iterar sobre cada grupo de enemigos configurado para esta oleada
            foreach (EnemyWave enemyWave in currentWave.enemiesInWave)
            {
                // Spawnear la cantidad de enemigos especificada de este tipo
                for (int i = 0; i < enemyWave.enemyCount; i++)
                {
                    SpawnEnemy(enemyWave.enemyType, enemyWave.spawnPointIndex);

                    // Esperar antes de spawnear el siguiente enemigo del grupo
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }

            // Esperar el tiempo especificado antes de la siguiente oleada
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
        }
    }

    // Método para spawnear un enemigo de un tipo específico en un punto específico
    void SpawnEnemy(int enemyType, int spawnPointIndex)
    {
        // Verificar que el tipo de enemigo y el punto de spawn sean válidos
        if (enemyType >= 0 && enemyType < enemyPrefabs.Length && spawnPointIndex >= 0 && spawnPointIndex < spawnPoints.Length)
        {
            // Instanciar el enemigo en el punto de spawn seleccionado
            Instantiate(enemyPrefabs[enemyType], spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Tipo de enemigo o índice de spawner fuera de rango.");
        }
    }
}
