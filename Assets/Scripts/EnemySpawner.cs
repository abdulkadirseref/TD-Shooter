using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();



    IEnumerator Start()
    {
        yield return new WaitForSeconds(0);

        if (waves.Count > 0)
        {
            StartCoroutine(SpawnWave());
        }
    }


    IEnumerator SpawnWave()
    {
        Wave currentWave = waves[WaveManager.Instance.waveIndex - 1];

        foreach (Enemy enemy in currentWave.enemies)
        {
            StartCoroutine(enemy.Spawn());
        }

        yield return new WaitWhile(() => WaveManager.Instance.canSpawnEnemy);

        if (WaveManager.Instance.waveIndex < waves.Count)
        {      
            StartCoroutine(SpawnWave());
        }
    }


    [System.Serializable]
    public class Wave
    {      
        public List<Enemy> enemies = new List<Enemy>();
    }


    [System.Serializable]
    public class Enemy
    {
        public BaseEnemyData enemyData;


        public IEnumerator Spawn()
        {
            yield return new WaitForSeconds(enemyData.startDelay);

            while (enemyData.canSpawn && WaveManager.Instance.canSpawnEnemy)
            {
                for (int i = 0; i < enemyData.quantityPerSpawn; i++)
                {
                    SpawnSingleEnemy();
                }
                yield return new WaitForSeconds(enemyData.spawnDelay);
            }
        }


        void SpawnSingleEnemy()
        {
            int randomX = Random.Range(-90, 90);
            int randomY = Random.Range(-90, 90);
            GameObject enemyInstance = Instantiate(enemyData.enemyPrefab, new Vector2(randomX, randomY), Quaternion.identity);
        }
    }
}