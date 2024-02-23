using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//https://www.youtube.com/watch?v=AoD_F1fSFFg
public class player : MonoBehaviour
{
    public static player instance;

    public static int actual_health = 100;

    private void Start()
    {
    }

    private void Awake()
    {
        instance = this;
    }

    public void increase_health(int value) {
        if(actual_health + value <= 100)
        {
            actual_health += value;
            game_object_manager.health_text.text = $"HP:{actual_health}";
        }
        
    }
}
