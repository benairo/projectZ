using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] zombieSpawnPoints;

    public GameObject[] zombiePrefabs;

    public int zombieRoundAmount;

    public bool isSpawning;

    public int currentZombieAmount;
    
    public float zombieHealth = 100.0f;

    private float _cooldown;
    
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
        Vector3 spawnPoint;
        spawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length + 1)].position;
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        Instantiate(zombiePrefab, spawnPoint, Quaternion.identity);
        currentZombieAmount++;
    }
    
    // Update zombie health and amount on each round
    public void UpdateZombieHealth()
    {
        zombieHealth *= 1.1f;
    }

    public void UpdateZombieAmount()
    {
        zombieRoundAmount = 6 * _waveManager.waveNumber;
    }
}
