using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    private RagDoll _ragDoll;
    // Start is called before the first frame update
    void Start()
    {
        _ragDoll = GetComponent<RagDoll>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _ragDoll.ActivateRagDoll();
    }
}
