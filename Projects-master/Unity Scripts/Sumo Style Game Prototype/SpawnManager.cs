﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int rndIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[rndIndex], GenerateSpawnPosition(), powerupPrefabs[rndIndex].transform.rotation);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;


        if (enemyCount == 0 && !playerController.isGameOver)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int rndIndex = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[rndIndex], GenerateSpawnPosition(), powerupPrefabs[rndIndex].transform.rotation);
        }

        

    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
