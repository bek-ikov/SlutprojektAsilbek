using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
        public int unlockAtWave;  // Which wave this enemy type starts appearing
    }

    public List<EnemyType> enemyTypes;
    public Transform[] spawnPoints;

    public float timeBetweenSpawns = 1f;
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;
    private int enemiesToSpawn;
    private int aliveEnemies;

    private bool isSpawningWave = false;

    public delegate void OnWaveStart(int wave);
    public static event OnWaveStart WaveStarted;

    void Start()
    {
        Target.OnAnyTargetDeath += HandleEnemyDeath;
        StartCoroutine(BeginNextWave());
    }

    void Update()
    {
        if (!isSpawningWave && aliveEnemies <= 0 && enemiesToSpawn == 0)
        {
            StartCoroutine(BeginNextWave());
        }
    }

    IEnumerator BeginNextWave()
    {
        isSpawningWave = true;

        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        Debug.Log($"Wave {currentWave} is starting!");

        enemiesToSpawn = Mathf.FloorToInt(5 + currentWave * 1.5f);  // scalable formula
        aliveEnemies = enemiesToSpawn;

        WaveStarted?.Invoke(currentWave);
        UIManager.Instance?.UpdateWave(currentWave);

        StartCoroutine(SpawnEnemies());

        isSpawningWave = false;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        enemiesToSpawn = 0;
    }

    void SpawnEnemy()
    {
        List<EnemyType> availableEnemies = enemyTypes.FindAll(e => currentWave >= e.unlockAtWave);
        if (availableEnemies.Count == 0) return;

        EnemyType chosen = availableEnemies[Random.Range(0, availableEnemies.Count)];

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(chosen.prefab, spawnPoint.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        Target.OnAnyTargetDeath -= HandleEnemyDeath;
    }

    void HandleEnemyDeath(Target deadTarget)
    {
        aliveEnemies--;
    }
}
