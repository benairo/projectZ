using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : Health
{
    private VolumeProfile _postProcessing;
    protected override void OnStart()
    {
        _postProcessing = FindObjectOfType<Volume>().profile;
    }

    // Remove the vignette effect when player has regened health
    protected override void OnRegen()
    {
        Vignette vignette;
        if (_postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.7f;
        }
    }
    // Add a vignette effect when player takes damage
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
        Debug.Log("You have died!");
    }
}
