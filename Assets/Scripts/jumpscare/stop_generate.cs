using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_generate : MonoBehaviour
{

    static public bool hit_cube = false;

    void OnTriggerEnter(Collider other) {
        hit_cube = true;
    }

}
