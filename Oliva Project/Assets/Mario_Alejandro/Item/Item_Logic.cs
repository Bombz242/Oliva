using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemName", menuName = "NewItem")]

[System.Serializable]
public class Item_Logic : ScriptableObject {
    //public int ObjectID;
    public ItemType objType;
    public Sprite objSprite;
    public int objPrice;
    public string objExtra;
}

[SerializeField]
public enum ItemType { Asignar, Programa, Llave, Password, Txt};

[System.Serializable]
public class Item_Inventory {
    public string _objName;
    public ItemType _objType;
    public Sprite _objSprite;
    public int _objAmount;
    public string _objExtra;
}



