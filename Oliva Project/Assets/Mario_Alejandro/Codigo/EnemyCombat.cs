using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour {


    //afinidad define q tipo de chobi es
    public float vida;
    public List<int> afinidad = new List<int>(3) { 33, 33, 33 };

    public GameObject canvasEnemy;


    private void OnTriggerEnter (Collider other) {

        if (other.tag == "Player") {
            other.GetComponent<Trigger>().enemigos.Add(this);
            other.GetComponent<Trigger>().CheckUIEnemies();
            //enemigos.Add(other.GetComponent<EnemyCombat>());
        }
        print (other.name);
        ActivateCanvas();
    }

    private void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<Trigger>().enemigos.Remove(this);
            other.GetComponent<Trigger>().CheckUIEnemies();
            //enemigos.Add(other.GetComponent<EnemyCombat>());
        }
        canvasEnemy.SetActive(false);
    }

    void ActivateCanvas () {
        canvasEnemy.SetActive(true);
        canvasEnemy.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "afinidad " + afinidad.ToString();
    }

    public void Resolviendo (int myValue) {

        int attackEnemyWeight = 0;
        int ataqueEnemigo = 0;
        int randomValue = 0;
        //string txt = GetComponent<Textos>().objMsg + "\n";
        //Sprite img = null;

        for (int i = 0; i < 3; i++) {
            attackEnemyWeight += GetComponent<Enemigo>().afinidad[i];
        }
        randomValue = Random.Range(0, attackEnemyWeight);
        Debug.Log("Weight = " + attackEnemyWeight);

        for (int t = 0; t < 3; t++) {
            if (randomValue <= GetComponent<Enemigo>().afinidad[t]) {
                ataqueEnemigo = t;
                //txt += "Ataque enemigo " + t + " Resultado:";
                //print("ATACO EN" + t);
                break;
            }
            randomValue -= GetComponent<Enemigo>().afinidad[t];
        }

        if (randomValue == myValue) {
            vida --;

        } else { 
            print ("Me sobrecarga en " + randomValue);
        }
         

        if (vida <= 0) {
            Destroy (gameObject);
        }


    }


}
