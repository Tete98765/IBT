using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor_manager : MonoBehaviour
{
    //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnApplicationFocus(bool ApplicationIsBack) {
        if (ApplicationIsBack == true) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
