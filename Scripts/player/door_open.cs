using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door_open : MonoBehaviour
{

    private string is_open = "n";
    private bool hit = false;

    public AudioSource creak_open;
    public AudioSource creak_close;
    public GameObject collider;

    GameObject target;
    float dist;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    //https://www.youtube.com/watch?v=8d1OhNQNSuY
    void Update()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist < 3.5 && Input.GetKey(KeyCode.E) && hit) {
            if (is_open == "n") {
                collider.SetActive(false);
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -2, 0);
                is_open = "o";
                creak_open.Play();
                hit = false;
                StartCoroutine(stopDoor());

            }
            else if (is_open == "y") {
                collider.SetActive(true);
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 2, 0);
                is_open = "c";
                creak_close.pitch = 0.5f;
                creak_close.Play();
                hit = false;
                StartCoroutine(stopDoor());
            }  
        }
    }

    void OnMouseOver()
    {
        if (dist < 3.5) {
            game_object_manager.action_button.SetActive(true);
            game_object_manager.action_text.SetActive(true);
            game_object_manager.cursor.GetComponent<Animation>().Play("openup");
            hit = true;
        }
        else {
           game_object_manager.action_text.SetActive(false);
           game_object_manager.action_button.SetActive(false);
           game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
            hit = false;
        }
        
    }

    void OnMouseExit() {
        game_object_manager.action_text.SetActive(false);
        game_object_manager.action_button.SetActive(false);
        game_object_manager.cursor.GetComponent<Animation>().Stop("openup");
        hit = false;
    }

    IEnumerator stopDoor() {
        yield return new WaitForSeconds(2);

        if (is_open == "o") {
            is_open = "y";
        }

        if (is_open == "c") {
            is_open = "n";
        }
    }
}

