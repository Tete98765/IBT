using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class field_of_view : MonoBehaviour
{
    public float radius; //maximalny dosah dohladu nepriatela
    [Range(0, 360)] //limitacia pre uhol lebo nemoze byt viac jak 360
    public float angle; //uhol v ktorom bude nepriatel detekovat objekty

    public LayerMask target_mask; //vrstva na ktorej je hráč
    public LayerMask obstruction_mask; //vrstva na ktorej je prekážka (stena)

    public bool can_see_player;

    private NavMeshAgent agent;

    public float radiuss;

    GameObject target;
    float dist;
    float last_attac_time = 0;
    float attac_calm_down = 5;
    //

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine()); // korutina, ktorá kontroluje, či nepriatel vidí hráča alebo nie
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f); //korutina ktorá sa volá s časovým oneskorením 0,2 sekundy

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    //https://github.com/Comp3interactive/FieldOfView/blob/main/FieldOfView.cs
    private void FieldOfViewCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, target_mask); //získa všetky objekty, nachádajúce sa v okruhu určenou premennou radius 

        if (rangeChecks.Length != 0) { //kontrola či sa hráč alebo iný objekt nenachádza v okruhu pola 
        
            Transform target = rangeChecks[0].transform;
            Vector3 direction_to_target = (target.position - transform.position).normalized;

                float distance_to_target = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, direction_to_target, distance_to_target, obstruction_mask)) {
                    can_see_player = true; //hit the obstruction mask
                    //Debug.Log("i see you");
                }
                    
                else {
                    can_see_player = false;

                    //Debug.Log("i cant see you");
                }
        }
    }


    private void Update() {
        dist = Vector3.Distance(transform.position, target.transform.position);

        if (can_fall.can_hit) {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);

            //https://www.youtube.com/watch?v=26Oavz7WTC0
            if (dist < 2) {
                stop_enemy(); //ak je moc blizko zastavim
                if (Time.time - last_attac_time >= attac_calm_down) {
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

            if(!can_see_player) {//ked ma nevidi prestane ma sledovat
            
                can_fall.can_hit = false;
            }
        }
        else if (!agent.hasPath) {
            agent.isStopped = false;
            //https://github.com/Xemicolon1/Unity-Random-AI-movement/blob/main/Scripts/Ai.cs
            agent.SetDestination(get_point_crawler.Instance.get_random_point_crawler(transform, radiuss));
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
 ////////////////////////////////////////////////////////////////////////////
}
