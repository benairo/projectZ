using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    
    protected override void OnStart()
    {
        
    }

    protected override void OnDamage(Vector3 direction)
    {
        
    }

    protected override void OnDeath(Vector3 direction)
    {
        Debug.Log("You have died!");
    }
}
