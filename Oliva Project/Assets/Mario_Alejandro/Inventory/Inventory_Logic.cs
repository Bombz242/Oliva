using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Logic : MonoBehaviour {

    private void Start () {
        inventario = this;
    }

    static public Inventory_Logic inventario;
    public List<Item_Inventory> myItems;


    public void AddItem (Item_Logic _itemValue) {

        Item_Inventory item = new Item_Inventory();
        
        item._objName = _itemValue.name;
        item._objSprite = _itemValue.objSprite;
        item._objType = _itemValue.objType;
        item._objExtra = _itemValue.objExtra;
        item._objAmount ++;


        bool exists = false;
        int it = 0;

        foreach (Item_Inventory i in myItems) {
            if (i._objName == item._objName) {
                exists = true;
                break;
            }
            it++;
        }

        if (exists) {
            print ("Existe");
            myItems[it]._objAmount++;
        } else {
            myItems.Add(item);
        }

    }


}
