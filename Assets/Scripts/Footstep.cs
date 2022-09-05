using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource source;

    public AudioClip footstep;

    // Function is called by animation events on the movement animations
    void Step()
    {
        source.clip = footstep;
        source.Play();
    }
}
