using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Unity.Entities;*/

public class cubes_generator : MonoBehaviour
{
    public GameObject cube;
    public bool stop;
    public float time; //zacne na
    public float delay; //opakuje sa
    private List<GameObject> destroy_cube = new List<GameObject>();

    public GameObject player;
    public GameObject jump_cam;
    public GameObject flash;
    public AudioSource scream;

    float x_p;
    float z_p;
    public static room_generator.room spawn_room;

    void Start() {
        //https://forum.unity.com/threads/spawning-cubes.304873/
        InvokeRepeating("spawn", time, delay);
    }

    public void spawn() {
        x_p = Random.Range(spawn_room.get_position().x, spawn_room.get_position().x + spawn_room.get_width());
        z_p = Random.Range(spawn_room.get_position().z, spawn_room.get_position().z + spawn_room.get_height());

        transform.position = new Vector3(x_p, 0, z_p);
        GameObject go = Instantiate(cube, transform.position, transform.rotation) as GameObject;

        destroy_cube.Add(go);

        if (delay > 0.1f) {
            delay -= 5;
        }
        else {
            delay = 0.1f;
        }

        if (time > 0.1f) {
            time -= 5;
        }
        else {
            time = 0.1f;
        }

    //https://www.youtube.com/watch?v=v-cSaUGTLEY
        if (stop_generate.hit_cube) {
            scream.Play();
            jump_cam.SetActive(true);
            player.SetActive(false);
            flash.SetActive(true);
            StartCoroutine(end_jump());

            CancelInvoke("spawn");

            for(int i = destroy_cube.Count - 1; i >= 0; i--) {
                GameObject clone = destroy_cube[i];
                destroy_cube.RemoveAt(i);
                Destroy(clone);
            }
        }

        
    }

    IEnumerator end_jump() {
        yield return new WaitForSeconds(2.03f);
        player.SetActive(true);
        jump_cam.SetActive(false);
        flash.SetActive(false);
    }
}
