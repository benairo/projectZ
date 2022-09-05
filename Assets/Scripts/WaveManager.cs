using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    public int zombiesKilled;
    
    public int waveNumber;

    public WaveCountWidget waveCountWidget;
    
    public int roundCooldown = 5;

    public AudioClip roundSound;

    private AudioSource _audioSource;
    
    private ZombieSpawner _zombieSpawner;
    
    void Start()
    {
        _zombieSpawner = GetComponentInChildren<ZombieSpawner>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Skip past wave 0
        if (waveNumber == 0)
        {
            NextWave();
        }

        // Check to see if all of the zombies in the round are killed
        if (zombiesKilled == _zombieSpawner.zombieRoundAmount)
        {
            _zombieSpawner.isSpawning = false;
            NextWave();
            _audioSource.PlayOneShot(roundSound);
        }
    }

    private void NextWave()
    {
        waveNumber++;
        waveCountWidget.Refresh(waveNumber);
        _zombieSpawner.UpdateZombieHealth();
        _zombieSpawner.UpdateZombieAmount();
        StartCoroutine(StartSpawning(roundCooldown));

    }

    // Set isSpawning to true after a certain amount of time
    IEnumerator StartSpawning(int time)
    {
        yield return new WaitForSeconds(time);
        _zombieSpawner.isSpawning = true;
    }
    
}
