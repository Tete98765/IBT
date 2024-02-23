using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=AoD_F1fSFFg
public class inventory_manager : MonoBehaviour
{
    public static inventory_manager instance;
    public List<item> items = new List<item>();

    public Transform tmp_item_content;
    public static Transform item_content;
    public GameObject inventory_item; //herný objekt ktorý bude vložený do inventára

    public inventory_item_controler[] inventory_items; //ovládanie jednotlivých prvkov v inventári

    private void Start() {
        item_content = tmp_item_content;
    }

    private void Awake() {
        instance = this;
    }

    public void add(item Item) {
        items.Add(Item);
    }

    public void remove(item Item) {
        items.Remove(Item);
    }

    public void list_items() {
        foreach(var Item in items) {//pri otvorení pre každú položku vytvorím herný objekt
            GameObject obj = Instantiate(inventory_item, item_content);

            var item_name = obj.transform.Find("item_name").GetComponent<TextMeshProUGUI>();
            var item_icon = obj.transform.Find("item_icon").GetComponent<UnityEngine.UI.Image>();
            var remove_b = obj.transform.Find("item_remove").GetComponent<UnityEngine.UI.Button>();

            item_name.text = Item.item_name;
            item_icon.sprite = Item.icon;
        }

        set_inventory_items();
    }

    public void set_inventory_items() {
        inventory_items = item_content.GetComponentsInChildren<inventory_item_controler>();

        for(int i = 0; i < items.Count; i++) {
            inventory_items[i].add_item(items[i]);
        }
    }
}
