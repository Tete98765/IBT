using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_pick_up : MonoBehaviour
{
    public item Item;
    private bool mouse_point = false;

    GameObject target;
    float dist;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void pick_up() {
        //https://www.youtube.com/watch?v=AoD_F1fSFFg
        inventory_manager.instance.add(Item);
        Destroy(gameObject);
    }

    void OnMouseOver() {
        if (dist < 2)
        {
            mouse_point = true;
            game_object_manager.cursor.GetComponent<Animation>().Play("openup");
            game_object_manager.pick_text.SetActive(true);
        }
        else
        {
            mouse_point = false;
            game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            game_object_manager.pick_text.SetActive(false);
        }
    }

    void OnMouseExit() {
        mouse_point = false;
        game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
        game_object_manager.pick_text.SetActive(false);
    }

    void Update() {
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 2 && Input.GetKey(KeyCode.E) && mouse_point)
        {
            game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            game_object_manager.pick_text.SetActive(false);
            pick_up();


        }
    }
}
