using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class on_pick_full : MonoBehaviour
{
    private bool hit = false;

    player_inventory player;

    GameObject target;
    float dist;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<player_inventory>();
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 3.5 && Input.GetKey(KeyCode.E) && hit && player.collected == 12) {
            //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
            SceneManager.LoadScene("Win");
        }
    }

    void OnMouseOver() {
        if(dist < 3.5) {
            game_object_manager.lock_text.SetActive(true);
            hit = true;
        }

    }

    void OnMouseExit() {
        game_object_manager.lock_text.SetActive(false);
        hit = false;   
    }
}
