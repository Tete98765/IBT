using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_effects : MonoBehaviour
{
   AudioSource scary_sound;

    void Start()
    {
        scary_sound = GetComponent<AudioSource>();
        InvokeRepeating("playing", 1.0f, 10.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scary_sound.Stop();
            CancelInvoke("playing");
        }
    }

    void playing()
    {
        scary_sound.Play();
    }
}
