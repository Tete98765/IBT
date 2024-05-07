using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

//https://www.youtube.com/watch?v=AoD_F1fSFFg
public class item : ScriptableObject {
    public int id;
    public string item_name;
    public int value;
    public Sprite icon;
    public item_type type;

    public enum item_type { 
        health,
        batterie,
        key
    }
}
