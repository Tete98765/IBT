using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_flicker : MonoBehaviour
{
    public Light broken_light;
    public float min; //rozsah v ktorom sa bude svetlo zapínať/vypínať
    public float max;
    public float timer; //Časovač, ktorý počíta zostávajúci čas do ďalšieho zapnutia alebo vypnutia svetla.
    public AudioSource light_sound;
    
    void Start() {
        timer = Random.Range(min, max);
    }

   
    void Update() {
        lights_flickering();
    }

    void lights_flickering() {
        //https://www.youtube.com/watch?v=iCCFPOdUaNI
        if (timer > 0) {
            timer -= Time.deltaTime; // Time.deltaTime je čas v sekundách, ktorý uplynul od posledného snímku. Týmto spôsobom zabezpečíme, že počkáme určený čas, než znova zmeníme stav svetla (zapneme alebo vypneme).
        }
        if (timer <= 0) {
            broken_light.enabled = !broken_light.enabled; //prepnutie medzi zapnutým/vypnutým stavom
            timer = Random.Range(min, max); //náhodná hodnota timeru
            light_sound.Play();
        }
    }
}





