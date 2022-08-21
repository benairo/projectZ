﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    public int zombiesKilled;
    
    public int waveNumber;

    public WaveCountWidget waveCountWidget;
    
    private int _roundCooldown = 5;
    
    private ZombieSpawner _zombieSpawner;
    void Start()
    {
        _zombieSpawner = GetComponentInChildren<ZombieSpawner>();
    }

    private void Update()
    {
        if (waveNumber == 0)
        {
            NextWave();
        }
        if (zombiesKilled == _zombieSpawner.zombieRoundAmount)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        waveNumber++;
        waveCountWidget.Refresh(waveNumber);
        _zombieSpawner.UpdateZombieHealth();
        _zombieSpawner.UpdateZombieAmount();
        StartCoroutine(StartSpawning(_roundCooldown));

    }

    IEnumerator StartSpawning(int time)
    {
        yield return new WaitForSeconds(time);
        _zombieSpawner.isSpawning = true;
    }
    
}