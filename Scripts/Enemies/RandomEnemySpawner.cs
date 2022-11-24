using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public float spawnTimer;
    public int easySpawn = 5;
    public int hardSpawn = 10;
    public bool easyMode;
    private float startSpawnTime = 1f;
    

    private int spawnAmount;
    private void Start()
    {
        if (easyMode)
        {
            for(int i=0; i < easySpawn; i++)
            {
                int randEnemy = Random.Range(0, enemyPrefabs.Length);
                int randSpawnPts = Random.Range(0, spawnPoints.Length);

                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPts].position, enemyPrefabs[randEnemy].transform.rotation);
            }
            startSpawnTime = spawnTimer;
        }
        else
        {
            for (int i = 0; i < hardSpawn; i++)
            {
                int randEnemy = Random.Range(0, enemyPrefabs.Length);
                int randSpawnPts = Random.Range(0, spawnPoints.Length);

                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPts].position, enemyPrefabs[randEnemy].transform.rotation);
            }
            startSpawnTime = spawnTimer;
        }
    }

    private void Update()
    {
        /*if (spawnTimer < Time.time)
        {
            spawnTimer += Time.time;
            spawnAmount = Random.Range(easyMin, easyMax);

            for (int i = 0; spawnAmount < i; i++)
            {
                int enemy = Random.Range(0, enemyPrefabs.Length);
                int spawnPoint = Random.Range(0, spawnPoints.Length);

                Instantiate(enemyPrefabs[enemy], spawnPoints[spawnPoint].position, transform.rotation);
            }
        }*/
        if (startSpawnTime <= 0)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPts = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPts].position, transform.rotation);

            startSpawnTime = spawnTimer;
        }
        else
        {
            startSpawnTime -= Time.deltaTime;
        }
    }
}
