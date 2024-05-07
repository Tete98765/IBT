using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_collector : MonoBehaviour
{
    /*public*/ player_inventory player;

    private bool mouse_point = false;
    float distane_from_target = 3;

    GameObject target;
    float dist;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<player_inventory>();
    }

    void pick_up() {
        //https://dotnetfiddle.net/Rzs92c
        player.keys_collected();
        Destroy(gameObject);
    }

    void OnMouseOver() {
        if (dist < distane_from_target) {
            mouse_point = true;
            game_object_manager.cursor.GetComponent<Animation>().Play("openup");
            game_object_manager.press_text.SetActive(true);
        }
        else {
            mouse_point = false;
            game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            game_object_manager.press_text.SetActive(false);
        }
    }

    void OnMouseExit() {
        mouse_point = false;
        game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
        game_object_manager.press_text.SetActive(false);
    }

    void Update() {
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < distane_from_target && Input.GetKey(KeyCode.E) && mouse_point) {
            game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            game_object_manager.press_text.SetActive(false);
            pick_up();
        }
    }
}
