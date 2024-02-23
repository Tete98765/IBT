using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=AoD_F1fSFFg
public class inventory_item_controler : MonoBehaviour
{
    item Item;
    public Button remove_button;

    public void remove_item()
    {
        inventory_manager.instance.remove(Item);
        Destroy(gameObject);
    }

    public void add_item(item new_item)
    {
        Item = new_item;
    }

    public void use_item()
    {
        switch(Item.type)
        {
           case item.item_type.health:
                player.instance.increase_health(Item.value);
                remove_item();
                break;
            case item.item_type.batterie:
                if (flashlight_pickup.have_flash == true)
                {
                    flashlight.instance.add_battery_life(Item.value);
                    remove_item();
                }
                break;
        }

        //remove_item();
    }
}
