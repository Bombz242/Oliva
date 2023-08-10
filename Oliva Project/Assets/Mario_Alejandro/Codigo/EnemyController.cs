using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    public int colorMain;
    public Color color;





    public float[] randomColorChance;
    public Color[] randomColorVisual;


    public GameObject Nodo;

    public GameObject[] Nodos;

    public Material mat;


    private void Start () {
        //new Material(Shader.Find("Specular"));
    
        Material matColor = new Material(Shader.Find("Standard" )) ;

        mat = matColor;
        //matColor.color = color;
        matColor.EnableKeyword("_EMISSION");
        matColor.SetColor("_EmissionColor", color);
        transform.GetChild(0).GetComponent<MeshRenderer>().material = matColor;

        onGenerateNodo();
    }


    public void onGenerateNodo () {

        GameObject temp =  Instantiate(Nodo, transform.position, transform.rotation) as GameObject;
        temp.GetComponent<Test_Trigger>().enemyRef = this;
        
        temp.GetComponent<MeshRenderer>().material = mat;



    }

    public void onCombat () {

    }




}
