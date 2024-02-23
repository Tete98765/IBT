using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_placement : MonoBehaviour
{
    public static List<GameObject> kitchen = new List<GameObject>(); //0
    public static List<GameObject> bathroom = new List<GameObject>(); //1 
    public static List<GameObject> room_item = new List<GameObject>(); //2
    public static List<GameObject> hospital = new List<GameObject>(); //3
    public static List<GameObject> living_room = new List<GameObject>(); //4

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn_object(room_generator.room spawn_room) {
        int rnd_item = Random.Range(0, 5); //vyberiem nahodne typ miestnosti 
        
        switch (rnd_item) {
            case 0:
                spawn_kitchen(spawn_room);
                break;
            case 1:
                spawn_bathroom(spawn_room);
                break;
            case 2:
                spawn_room_item(spawn_room);
                break;
            case 3:
                spawn_hospital(spawn_room);
               break;
            case 4:
                spawn_living_room(spawn_room);
                break;
            default:
                break;
        }
    }

    void spawn_kitchen(room_generator.room spawn_room) {
        int num = Random.Range(1, spawn_room.get_floors_count()/4); //na stvrtinu kociek umiestnim
        Vector3 position;
        string dir = "";

        for(int i = 0; i < num; i++) {
            GameObject item_in_room = kitchen[Random.Range(0, kitchen.Count)];
            bool valid_pos = true;
            if (item_in_room.CompareTag("item")) {
                int rnd = Random.Range(0, spawn_room.get_walls_position().Count);
                position = spawn_room.get_random_wall(rnd);
                dir = spawn_room.get_random_wall_direction(rnd);
                valid_pos = spawn_room.check_door(position);
                if(valid_pos) valid_pos = spawn_room.check_wall(position);

                switch (dir) {
                        case "up" :
                        case "down" :
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(-2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(-2f, 0, 0));
                            break;
                        case "left" :
                        case "right" :
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(0, 0, 2f));
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(0, 0, -2f));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(0, 0, 2f));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(0, 0, -2f));
                            break;
                        default:
                            break;
                    }
            }
            else {
               position = spawn_room.get_floor_position(); 
               valid_pos = spawn_room.check_door(position);
            }

            BoxCollider box_collider = item_in_room.GetComponent<BoxCollider>();

            Vector3 object_size = box_collider.size;
            Vector3 box_size = object_size * 1f; 

            if(!Physics.CheckBox(position, box_size) && valid_pos) {
                GameObject new_item = Instantiate(item_in_room, position + new Vector3(0f, -1.24f, 0f),  Quaternion.identity);
                if (item_in_room.CompareTag("item")) {
                    switch (dir) {
                        case "up" :
                            break;
                        case "down" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                            break;
                        case "left" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                            break;
                        case "right" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                            break;
                        default:
                            break;
                    }
                }
            }
        }   

    }

    void spawn_bathroom(room_generator.room spawn_room) {
        int num = Random.Range(1, spawn_room.get_floors_count()/4);
        Vector3 position;
        string dir = "";
        bool valid_pos = true;


        for(int i = 0; i < num; i++) {
            GameObject item_in_room = bathroom[Random.Range(0,bathroom.Count)];
            if (item_in_room.CompareTag("item")) {
                int rnd = Random.Range(0, spawn_room.get_walls_position().Count);
                position = spawn_room.get_random_wall(rnd);
                dir = spawn_room.get_random_wall_direction(rnd);
                valid_pos = spawn_room.check_door(position);
                if(valid_pos) valid_pos = spawn_room.check_wall(position);
                switch (dir) {
                        case "up" :
                        case "down" :
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(-2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(2f, 0, 0));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(-2f, 0, 0));
                            break;
                        case "left" :
                        case "right" :
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(0, 0, 2f));
                            if(valid_pos) valid_pos = spawn_room.check_wall(position + new Vector3(0, 0, -2f));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(0, 0, 2f));
                            if(valid_pos) valid_pos = spawn_room.check_door(position + new Vector3(0, 0, -2f));
                            break;
                        default:
                            break;
                    }
            }
            else {
               position = spawn_room.get_floor_position(); 
               valid_pos = spawn_room.check_door(position);
            }

            

            BoxCollider box_collider = item_in_room.GetComponent<BoxCollider>();

            Vector3 object_size = box_collider.size;
            Vector3 box_size = object_size * 1f; 

            if(!Physics.CheckBox(position, box_size) && valid_pos) {
                GameObject new_item = Instantiate(item_in_room, position + new Vector3(0f, -1.24f, 0f),  Quaternion.identity);
                if (item_in_room.CompareTag("item")) {
                    switch (dir) {
                        case "up" :
                            break;
                        case "down" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                            break;
                        case "left" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
                            break;
                        case "right" :
                            new_item.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                            break;
                        default:
                            break;
                    }
                }
            }
        }  
    }

    void spawn_hospital(room_generator.room spawn_room) {
        int num = Random.Range(1, spawn_room.get_floors_count()/4);
        Vector3 position;
        bool valid_pos = true;

        for(int i = 0; i < num; i++) {
            position = spawn_room.get_floor_position();
            valid_pos = spawn_room.check_door(position);
            GameObject item_in_room = hospital[Random.Range(0,hospital.Count)];

            BoxCollider box_collider = item_in_room.GetComponent<BoxCollider>();

   
            Vector3 object_size = box_collider.size;
            Vector3 box_size = object_size * 1f; 
            if(!Physics.CheckBox(position, box_size) && valid_pos) {
                Instantiate(item_in_room, position + new Vector3(0f, -1.24f, 0f),  Quaternion.identity);
            }
        }  

    }

    void spawn_living_room(room_generator.room spawn_room) {
        int num = Random.Range(1, spawn_room.get_floors_count()/4);
        Vector3 position;
        bool valid_pos = true;

        for(int i = 0; i < num; i++) {
            position = spawn_room.get_floor_position();
            valid_pos = spawn_room.check_door(position);
            GameObject item_in_room = living_room[Random.Range(0,living_room.Count)];

            BoxCollider box_collider = item_in_room.GetComponent<BoxCollider>();
            Vector3 object_size = box_collider.size;
            Vector3 box_size = object_size * 1f; 

            if(!Physics.CheckBox(position, box_size) && valid_pos) {
                Instantiate(item_in_room, position + new Vector3(0f, -1.24f, 0f),  Quaternion.identity);
            }
        }  

    }

    //0 - stolicka, 1 - stol, 2 - bed table, 3 - broken chair, 4 - broken bed, 5 - bed with person, 6 - bed, 7 - closet
    void spawn_room_item(room_generator.room spawn_room) {
        int num = Random.Range(1, spawn_room.get_floors_count()/4);
        Vector3 position;
        bool valid_pos = true;

        for(int i = 0; i < num; i++) {
            position = spawn_room.get_floor_position();
            valid_pos = spawn_room.check_door(position);
            GameObject item_in_room = room_item[Random.Range(0, room_item.Count)];

            BoxCollider box_collider = item_in_room.GetComponent<BoxCollider>();

            Vector3 object_size = box_collider.size;
            Vector3 box_size = object_size * 1f; 

            if(!Physics.CheckBox(position, box_size) && valid_pos) {
                if (item_in_room.CompareTag("table")) {
                    Instantiate(item_in_room, position + new Vector3(0f, -1.24f, -1.16f),  Quaternion.identity);
                }
                else {
                    Instantiate(item_in_room, position + new Vector3(0f, -1.24f, 0f),  Quaternion.identity);
                }
                
            }
        }  
    }
}
