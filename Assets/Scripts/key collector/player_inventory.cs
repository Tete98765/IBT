using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//https://dotnetfiddle.net/Rzs92c
public class player_inventory : MonoBehaviour
{
    public int collected { get; private set; }

    public UnityEvent<player_inventory> on_pickig;

    public void keys_collected()
    {
        collected++;
        on_pickig.Invoke(this);
    }
}
