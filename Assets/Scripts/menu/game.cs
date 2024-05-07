using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_game() {
        //https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
        SceneManager.LoadScene("MainScene");
    }
}
