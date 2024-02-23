using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class walk : MonoBehaviour
{
    private NavMeshAgent agent;

    public float radius;

    GameObject target;
    float dist;
    float last_attac_time = 0;
    float attac_calm_down = 5;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void Update() {
        dist = Vector3.Distance(transform.position, target.transform.position); 

        if (dist < 10) {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position); //go to the target
            //https://www.youtube.com/watch?v=26Oavz7WTC0
            if (dist < 2) {
                stop_enemy(); //ak je moc blizko zastavim
                if(Time.time - last_attac_time >= attac_calm_down) {
                    last_attac_time = Time.time;
                    player.actual_health -= 10;
                    game_object_manager.health_text.text = $"HP:{player.actual_health}";

                    if (player.actual_health == 0) {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene("GameOver");
                    }
                }
                

            }
        }
        //https://github.com/Xemicolon1/Unity-Random-AI-movement/blob/main/Scripts/Ai.cs
        else if (!agent.hasPath) {
            agent.isStopped = false;
            agent.SetDestination(get_point_ghost.Instance.get_random_point_ghost(transform, radius));
        }
    }

    private void stop_enemy() {
        agent.isStopped = true;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}
