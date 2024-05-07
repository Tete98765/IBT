using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class game_object_manager : MonoBehaviour
{
    public static GameObject cursor;
    public static GameObject action_button;
    public static GameObject action_text;
    public static GameObject press_text;
    public static GameObject lock_text;
    public static GameObject pick_text;
    public static TextMeshProUGUI health_text;
    public static GameObject light;
    public static GameObject battery_text;

    public GameObject tmp_cursor;
    public GameObject tmp_action_button;
    public GameObject tmp_action_text;
    public GameObject tmp_press_text;
    public GameObject tmp_lock_text;
    public GameObject tmp_pick_text;
    public GameObject tmp_battery_text;
    public GameObject tmp_light;

    public TextMeshProUGUI tmp_health_text;

    [SerializeField]
    public List<GameObject> tmp_kitchen = new List<GameObject>(); //0
    [SerializeField]
    public List<GameObject> tmp_bathroom = new List<GameObject>(); //1 
    [SerializeField]
    public List<GameObject> tmp_room_item = new List<GameObject>(); //2
    [SerializeField]
    public List<GameObject> tmp_hospital = new List<GameObject>(); //3
    [SerializeField]
    public List<GameObject> tmp_living_room = new List<GameObject>(); //4


    void Start() {
        cursor = tmp_cursor;
        action_button = tmp_action_button;
        action_text = tmp_action_text;
        press_text = tmp_press_text;
        pick_text = tmp_pick_text;
        battery_text = tmp_battery_text;

        item_placement.kitchen = tmp_kitchen; //0
        item_placement.bathroom = tmp_bathroom; //1 
        item_placement.room_item = tmp_room_item; //2
        item_placement.hospital = tmp_hospital; //3
        item_placement.living_room = tmp_living_room; //4

        health_text = tmp_health_text;
        lock_text = tmp_lock_text;

        light = tmp_light;
    }


}
