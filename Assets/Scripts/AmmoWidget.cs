using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWidget : MonoBehaviour
{
    public TMPro.TMP_Text ammoText;

    public Image ammoImage;
    
    private ActiveWeapon _weapon;

    public void Start()
    {
        _weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveWeapon>();
        ammoText.enabled = false;
        ammoImage.enabled = false;
    }

    public void Refresh(int ammoCount)
    {
        var weapon = _weapon.GetActiveWeapon();
        if (weapon)
        {
            ammoText.enabled = true;
            ammoText.text = ammoCount.ToString();
            ammoImage.enabled = true;
        }
    }
}
