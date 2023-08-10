using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class Requiere : MonoBehaviour {



    public List<Item_Logic> requiereItems;
    public GameObject objetoADestruir;

    public string Texto (List<Item_Inventory> myItems ) {


        string txtrequerido = GetComponent<Textos>().objMsg + "\n";
        foreach (Item_Logic itemRequerido in requiereItems) { //other.GetComponent<Item_Container>().contains) {
            Item_Inventory item = new Item_Inventory();
            item._objName = itemRequerido.name;

            txtrequerido += itemRequerido.name + "\n";

            /*
            foreach (Item_Inventory i in myItems) {
                if (i._objName != item._objName) {
                    txtrequerido += itemRequerido.name + "\n";
                    //break;
                } 
            }*/

        }

        return txtrequerido;
    }

    public void Lista (List<Item_Inventory> myItems ) {
        string txtrequerido = GetComponent<Textos>().objMsg + "\n";



        foreach (Item_Logic itemRequerido in requiereItems ) { //other.GetComponent<Item_Container>().contains) {
            Item_Inventory item = new Item_Inventory();
            item._objName = itemRequerido.name;

            bool existe = false;
            foreach (Item_Inventory i in myItems) {
                if (i._objName == item._objName) {
                    existe = true;
                    break;
                }
            }

            if (existe ) {
                print ("Abre la puerta");
                Destroy (objetoADestruir);
                txtrequerido = "Puerta abierta";

            } else {
                print ("No abre y tira mensaje de error y que requiere?");
                txtrequerido += itemRequerido.name + "\n";

            }

        }

        Trigger.MyTrigger.CanvasTexto.transform.GetChild(0).GetComponent<Text>().text = txtrequerido;
        Trigger.MyTrigger.CanvasTexto.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<Textos>().objSprite;
        Trigger.MyTrigger.CanvasTexto.SetActive(true);
    }

}
