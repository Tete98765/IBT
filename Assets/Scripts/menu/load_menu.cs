using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            //https://gamedevbeginner.com/how-to-lock-hide-the-cursor-in-unity/
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
            SceneManager.LoadScene("Menu");
        }
    }
}
