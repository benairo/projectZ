using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCountWidget : MonoBehaviour
{
    public TMPro.TMP_Text waveCountText;
    public void Refresh(int waveNumber)
    {
        waveCountText.text = waveNumber.ToString();
    }
    
}
