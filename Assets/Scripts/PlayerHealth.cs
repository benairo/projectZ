using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : Health
{
    private PlayerAiming _aiming;

    private VolumeProfile _postProcessing;
    protected override void OnStart()
    {
        _aiming = GetComponent<PlayerAiming>();
        _postProcessing = FindObjectOfType<Volume>().profile;
    }

    protected override void OnDamage(Vector3 direction)
    {
        Vignette vignette;
        if (_postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.7f;
        }
    }

    protected override void OnDeath(Vector3 direction)
    {
        _aiming.enabled = false;
        Debug.Log("You have died!");
    }
}
