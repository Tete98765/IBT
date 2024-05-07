using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class flashlight : MonoBehaviour
{
    public static flashlight instance;

    public float battery_life_s = 60f;
    public float max_intensity = 1f;

    private Light my_light;
    private float battery_life;
    private bool is_active;

    public TextMeshProUGUI battery_text;

    void Start() {
        my_light = GetComponent<Light>();
        battery_life = my_light.intensity;
        battery_text.gameObject.SetActive(true);
        battery_text.text = $"BL:{battery_life * 100f}";

    }

    private void Awake() {
        instance = this;
    }

    void Update()  {
        if (Input.GetKey(KeyCode.F)) {
            is_active = !is_active;
        }

        if(my_light.intensity == 0) {
           room_generator.ghost.SetActive(true);
        }

        if (is_active) {
            //https://forum.unity.com/threads/help-with-my-advanced-flashlight-script.418680/
            my_light.enabled = true;
            my_light.intensity -= battery_life / battery_life_s * Time.deltaTime;
            battery_text.text = $"BL:{System.Math.Round(my_light.intensity * 100f, 0)}";

            if (my_light.intensity == 0)  {
                room_generator.ghost.SetActive(true);
            }
            else  {
                room_generator.ghost.SetActive(false);
            }

        }
        else {
            my_light.enabled = false;
            room_generator.ghost.SetActive(true);
        }

        
    }

    public void add_battery_life(float power) {
        my_light.intensity += power;
        if (my_light.intensity > max_intensity)
            my_light.intensity = max_intensity;

        if (my_light.intensity == 1) {
            battery_text.text = $"BL:{battery_life * 100f}";
        }
        else {
           battery_text.text = $"BL:{my_light.intensity}"; 
        }
        
    }
}
