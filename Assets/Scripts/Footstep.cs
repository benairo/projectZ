using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource source;

    public AudioClip footstep;

    void Step()
    {
        source.clip = footstep;
        source.Play();
    }
}
