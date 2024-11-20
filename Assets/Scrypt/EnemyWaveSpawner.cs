using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public int enemyType;         // �ndice del tipo de enemigo (en la lista de prefabs)
    public int spawnPointIndex;   // �ndice del punto de spawn (en la lista de spawners)
    public int enemyCount;        // Cantidad de enemigos de este tipo
    public string pathName;       // Nombre del camino que deben seguir los enemigos
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

    // Caminos (definidos en el Inspector)
    public Transform[] waypointParents;

    // Diccionario para almacenar los caminos
    private Dictionary<string, Transform[]> waypoints = new Dictionary<string, Transform[]>();

    // Lista de oleadas
    public List<WaveConfig> waves;

    // Configuraci�n de tiempos entre oleadas y spawns
    public float timeBetweenWaves = 5f;   // Tiempo entre oleadas
    public float timeBetweenSpawns = 1f;  // Tiempo entre spawns de enemigos dentro de una oleada

    public int currentWaveIndex = 0;     // �ndice de la oleada actual

    void Start()
    {
        // Inicializar los caminos
        InitializeWaypoints();

        // Comenzar la secuencia de oleadas
        StartCoroutine(SpawnWaves());
    }

    // M�todo para inicializar los caminos
    void InitializeWaypoints()
    {
        foreach (Transform parent in waypointParents)
        {
            Transform[] path = new Transform[parent.childCount];
            for (int i = 0; i < parent.childCount; i++)
            {
                path[i] = parent.GetChild(i);
            }
            waypoints[parent.name] = path;
        }
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
                    SpawnEnemy(enemyWave.enemyType, enemyWave.spawnPointIndex, enemyWave.pathName);

                    // Esperar antes de spawnear el siguiente enemigo del grupo
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }

            // Esperar el tiempo especificado antes de la siguiente oleada
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
        }
    }

    // M�todo para spawnear un enemigo de un tipo espec�fico en un punto espec�fico
    void SpawnEnemy(int enemyType, int spawnPointIndex, string pathName)
    {
        Debug.Log($"Intentando spawnear enemigo de tipo {enemyType} en el punto de spawn {spawnPointIndex} con el camino {pathName}");

        if (enemyType >= 0 && enemyType < enemyPrefabs.Length && spawnPointIndex >= 0 && spawnPointIndex < spawnPoints.Length)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyType], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            Enemigodiego enemyMovement = enemy.GetComponent<Enemigodiego>();
            if (enemyMovement != null)
            {
                if (waypoints.TryGetValue(pathName, out Transform[] path))
                {
                    enemyMovement.SetWaypoints(path);
                    Debug.Log("Waypoints asignados al enemigo: " + pathName);
                }
                else
                {
                    Debug.LogError("Path not found: " + pathName);
                }
            }
        }
        else
        {
            Debug.LogWarning("Tipo de enemigo o �ndice de spawner fuera de rango.");
        }
    }
}
