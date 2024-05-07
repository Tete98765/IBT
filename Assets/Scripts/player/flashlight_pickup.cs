using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight_pickup : MonoBehaviour
{
    private bool hit = false;
    private bool mouse_point = false;
    static public bool have_flash = false;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            hit = true;
        }
    }

    void pick_up() {
        Destroy(gameObject);
        have_flash = true;
        game_object_manager.light.SetActive(true);
    }

    void OnMouseOver() {
        if (hit)
        {
            mouse_point = true;
            game_object_manager.cursor.GetComponent<Animation>().Play("openup");
            game_object_manager.pick_text.SetActive(true);
        }
    }

    void OnMouseExit() {
        mouse_point = false;
        game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
        game_object_manager.pick_text.SetActive(false);
    }

    void Update() {
        if (hit && Input.GetKey(KeyCode.E) && mouse_point) {
            game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            game_object_manager.pick_text.SetActive(false);
            game_object_manager.battery_text.SetActive(true);
            pick_up();
        }
    }
}
