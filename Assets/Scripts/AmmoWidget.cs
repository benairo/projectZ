using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text weaponAmmoText;

    public TMPro.TMP_Text totalAmmoText;

    public Image ammoImage;
    
    private ActiveWeapon _weapon;

    public void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveWeapon>();
        // Start the game with no widgets enabled
        weaponAmmoText.enabled = false;
        totalAmmoText.enabled = false;
        ammoImage.enabled = false;
    }

    public void Refresh(int ammoCount, int totalAmmo)
    {
        var weapon = _weapon.GetActiveWeapon();
        if (weapon)
        {
            weaponAmmoText.enabled = true;
            weaponAmmoText.text = ammoCount.ToString();
            totalAmmoText.enabled = true;
            totalAmmoText.text = totalAmmo.ToString();
            ammoImage.enabled = true;
        }
    }
}
