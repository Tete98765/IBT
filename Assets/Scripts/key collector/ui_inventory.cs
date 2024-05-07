using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//https://dotnetfiddle.net/Rzs92c
public class ui_inventory : MonoBehaviour
{
    private TextMeshProUGUI count_text;
  
    void Start() {
        count_text = GetComponent<TextMeshProUGUI>();
    }

    public void update_text(player_inventory player) {
        count_text.text = player.collected.ToString();
    }
}
