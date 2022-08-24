using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text weaponAmmoText;

    // public TMPro.TMP_Text totalAmmoText;

    public Image ammoImage;
    
    private ActiveWeapon _weapon;

    public void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveWeapon>();
        weaponAmmoText.enabled = false;
        // totalAmmoText.enabled = false;
        ammoImage.enabled = false;
    }

    public void Refresh(int ammoCount)
    {
        var weapon = _weapon.GetActiveWeapon();
        if (weapon)
        {
            weaponAmmoText.enabled = true;
            weaponAmmoText.text = ammoCount.ToString();
            ammoImage.enabled = true;
        }
    }
}
