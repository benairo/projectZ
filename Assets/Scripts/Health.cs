using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    
// Start is called before the first frame update
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

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
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

    protected virtual void OnDamage(Vector3 direction)
    {
        
    }

    protected virtual void OnDeath(Vector3 direction)
    {
        
    }
}
