using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Combate : MonoBehaviour {


    public void FirstStep () { print ("Abre UI, displayea datos");
        Trigger.MyTrigger.CanvasTexto.SetActive(true);
        //Trigger.MyTrigger.CanvasTexto.transform.GetChild(0).GetComponent<Text>().text = "Combate contra el enemigo " + GetComponent<Enemigo>().afinidad[0] + "%/" + GetComponent<Enemigo>().afinidad[1] + "%/" + GetComponent<Enemigo>().afinidad[2] ;

        Trigger.MyTrigger.CanvasTexto.transform.GetChild(0).GetComponent<Text>().text = GetComponent<Textos>().objMsg;
        Trigger.MyTrigger.CanvasTexto.transform.GetChild(1).GetComponent<Image>().sprite = GetComponent<Textos>().objSprite;

        //Trigger.MyTrigger.enemigos.Add( GetComponent<Enemigo>() );
        Trigger.MyTrigger.CanvasCombate.transform.GetChild(0).GetComponent<Text>().text = "HP " + Trigger.MyTrigger.myVida + " / BOT HP " + GetComponent<Enemigo>().vida;   
        Trigger.MyTrigger.LevelOptions.PlayerCanMove = false;
        
        Trigger.MyTrigger.StartCoroutine( Trigger.MyTrigger.CameraFade(true, 1.5f) );
    }

    public void Resolviendo (int id) {

        int attackEnemyWeight = 0;
        int ataqueEnemigo = 0;
        string txt = GetComponent<Textos>().objMsg + "\n";
        Sprite img = null;

        for (int i=0; i < 3; i++) {
            attackEnemyWeight += GetComponent<Enemigo>().afinidad[i];
        }
        Debug.Log("Weight = " + attackEnemyWeight);

        int randomValue = Random.Range(0, attackEnemyWeight);

        for (int t=0; t < 3; t++) {
            if (randomValue <= GetComponent<Enemigo>().afinidad[t]) {
                ataqueEnemigo = t;
                //txt += "Ataque enemigo " + t + " Resultado:";
                print ("ATACO EN" + t);
                break;
            }
            randomValue -= GetComponent<Enemigo>().afinidad[t];
        }

        switch (id) {
            case 0: //Papel
                if (ataqueEnemigo == 0) {
                    txt += " Papel / Papel = EMPATE";
                } else if (ataqueEnemigo == 1) {
                    txt += " Papel / Piedra = GANASTE";
                    GetComponent<Enemigo>().vida--;
                } else {
                    txt += " Papel / Tijera = PERDISTE";
                    Trigger.MyTrigger.myVida--;
                }
                break;
            case 1: //Piedra
                if (ataqueEnemigo == 1) {
                    txt += " Piedra / Piedra = EMPATE";
                } else if (ataqueEnemigo == 2) {
                    txt += " Piedra / Tijera = GANASTE";
                    GetComponent<Enemigo>().vida--;
                } else {
                    txt += " Piedra / Papel = PERDISTE";
                    Trigger.MyTrigger.myVida--;
                }
                break;
            case 2: //Tijera
                if (ataqueEnemigo == 2) {
                    txt += " Tijera / Tijera = EMPATE UWU";
                } else if (ataqueEnemigo == 0) {
                    txt += " Tijera / Papel = GANASTE";
                    GetComponent<Enemigo>().vida--;
                } else {
                    txt += " Tijera / Piedra = PERDISTE";
                    Trigger.MyTrigger.myVida--;
                }
                break;
        }

        if (GetComponent<Enemigo>().vida <= 0 ) {
            txt = "Has obtenido:\n";
            if (GetComponent<Item_Container>().contains.Count > 0) {
                img = GetComponent<Item_Container>().contains[0].objSprite;
                foreach (Item_Logic itemToAdd in GetComponent<Item_Container>().contains) {
                    txt += itemToAdd.name + "\n";
                    Trigger.MyTrigger.PlayerInventory.AddItem(itemToAdd);
                }

            } else {
                txt += "Nada pt";
            }

            Trigger.MyTrigger.CanvasCombate.SetActive(false);
            //Trigger.MyTrigger.enemigos.Remove(GetComponent<Enemigo>());
            
            Trigger.MyTrigger.StartCoroutine(Trigger.MyTrigger.CameraFade(false, 0.25f));
            Destroy(gameObject);

        } else {
            img = GetComponent<Textos>().objSprite;
        }

        Trigger.MyTrigger.CanvasCombate.transform.GetChild(0).GetComponent<Text>().text = "HP " + Trigger.MyTrigger.myVida + " / BOT HP " + GetComponent<Enemigo>().vida;
        Trigger.MyTrigger.CanvasTexto.transform.GetChild(1).GetComponent<Image>().sprite = img;
        Trigger.MyTrigger.CanvasTexto.transform.GetChild(0).GetComponent<Text>().text = txt;
    }
}
