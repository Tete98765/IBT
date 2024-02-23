using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can_fall : MonoBehaviour
{
    public AudioSource can_sound;
    static public bool can_hit = false;
    public AudioSource mystery;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            can_hit = true;
            can_sound.Play();
            mystery.Play();
        }
    }

    void Update() {
        if (!can_hit)
            mystery.Stop();   
    }
}
