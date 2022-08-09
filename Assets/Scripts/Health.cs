using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    public float dieForce;

    private RagDoll _ragDoll;

    private UIHealthBar _healthBar;
// Start is called before the first frame update
    void Start()
    {
        _ragDoll = GetComponent<RagDoll>();
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
        _ragDoll.ActivateRagDoll();
        direction.y = 1;
        _ragDoll.ApplyForce(direction * dieForce);
        _healthBar.gameObject.SetActive(false);
    }
}
