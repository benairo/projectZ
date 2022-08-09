using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    
    private ZombieAgent _agent;

    private UIHealthBar _healthBar;
// Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<ZombieAgent>();
        currentHealth = maxHealth;
        _healthBar = GetComponentInChildren<UIHealthBar>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        _healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        ZombieDeathState deathState = _agent.stateMachine.GetState(ZombieStateID.Death) as ZombieDeathState;
        deathState.direction = direction;
        _agent.stateMachine.ChangeState(ZombieStateID.Death);
    }
}
