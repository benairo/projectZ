using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] zombieSpawnPoints;

    public GameObject[] zombiePrefabs;

    public int zombieRoundAmount;

    public bool isSpawning;

    public int currentZombieAmount;

    private float _cooldown;
    
    private float _zombieHealth;
    
    private WaveManager _waveManager;

    private float _zombiesThisRound;


    private void Start()
    {
        if (zombieSpawnPoints.Length < 1 || zombiePrefabs.Length < 1)
        {
            return;
        }

        _waveManager = GetComponentInParent<WaveManager>();
    }

    private void Update()
    {
        if (currentZombieAmount >= 24)
        {
            return;
        }
        
        if (isSpawning)
        {
            _cooldown += Time.deltaTime;

            if (_cooldown >= 5.0f)
            {
                SpawnZombie();
                _cooldown = 0.0f;
            }
        }
    }

    private void SpawnZombie()
    {
        Vector3 spawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)].position;
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
        currentZombieAmount++;
    }
    
    public void UpdateZombieHealth()
    {
        _zombieHealth *= 1.1f;
    }

    public void UpdateZombieAmount()
    {
        zombieRoundAmount = 6 * _waveManager.waveNumber;
    }
}
