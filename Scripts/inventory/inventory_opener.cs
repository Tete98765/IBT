using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class inventory_opener : MonoBehaviour
{
    private bool open = false;
    public GameObject inventory;
    public bool fail_save = false;
    public Button close_button;
    GameObject player_camera;
    public GameObject pause_cam;
    private AudioSource[] all_audio;

    void Start() {
        all_audio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.I))
        {

            open = !open;
            if(open == true && fail_save == false) {
                pause_cam.transform.position = player_camera.transform.position;
                pause_cam.transform.rotation = player_camera.transform.rotation;
                inventory_manager.instance.list_items();
                inventory.SetActive(true);

                foreach (AudioSource audio_s in all_audio) {
                    audio_s.Stop();
                }
                 //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0.0f;
                player_camera.SetActive(false);
                pause_cam.SetActive(true);
                StartCoroutine(fail_saving());

                
            }
            else if(open == false && fail_save == false)
            {
                inventory.SetActive(false);

                //vycisti obsah pred znovuotvorenim
                foreach (Transform Item in inventory_manager.item_content) {
                    Destroy(Item.gameObject);
                }

                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player_camera.SetActive(true);
                pause_cam.SetActive(false);
                StartCoroutine(fail_saving());
                
            }
        }
        
        close_button.onClick.AddListener(stop_freezing);


    }

    void stop_freezing() {
        inventory.SetActive(false);

        foreach (Transform Item in inventory_manager.item_content) {
            Destroy(Item.gameObject);
        }

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player_camera.SetActive(true);
        pause_cam.SetActive(false);
    }

    IEnumerator fail_saving() {
        yield return new WaitForSeconds(0.25f);
        fail_save = false;
    }
}
