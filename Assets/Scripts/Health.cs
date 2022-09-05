using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    private float _regenTimer;

    private bool _damaged;

    void Start()
    { 
        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
        
        OnStart();
    }

    private void Update()
    {
        if (_damaged)
        {
            _regenTimer += Time.deltaTime;
            if (_regenTimer >= 10.0f)
            {
                RegenHealth();
                _damaged = false;
            }
        }
        
    }

    void RegenHealth()
    {
        currentHealth = maxHealth;
        OnRegen();
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        _damaged = true;
        _regenTimer = 0.0f;
        OnDamage(direction);
        if (currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void Die(Vector3 direction)
    {
        OnDeath(direction);
    }

    protected virtual void OnStart()
    {
        
    }

    protected virtual void OnRegen()
    {
        
    }

    protected virtual void OnDamage(Vector3 direction)
    {
        
    }

    protected virtual void OnDeath(Vector3 direction)
    {
        
    }
}
