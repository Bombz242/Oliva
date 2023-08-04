using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Trigger : MonoBehaviour {

    static public Trigger MyTrigger;

    public int myVida = 3;

    public Inventory_Logic PlayerInventory;



    public GameObject CanvasTexto;
    public GameObject CanvasCombate;

    public List<Enemigo> enemigos;

    public Level_MenuInit LevelOptions;
    public Camera Cam;

    public Vector3 camOriginalOffset;

    private void Start () {
        MyTrigger = this;
        PlayerInventory = GetComponent<Inventory_Logic>();
        LevelOptions = GameObject.Find("Canvas_Level").GetComponent<Level_MenuInit>();
        Cam = Camera.main;
        CanvasTexto.SetActive(false);
        CanvasCombate.SetActive(false);
    }

    public void CallCombate (int id) {
        foreach(Enemigo enemigo in enemigos) {
            enemigo.GetComponent<Combate>().Resolviendo(id);
        }
    }

    //public void CameraFade (float init, float final) {

    //}

    public IEnumerator CameraFade (bool status, float duration) {

        float fovinit;
        float fovfinal;
        Vector3 init;
        Vector3 final;

        if (status) { //Alejando el zoom por batalla
            fovinit = 60;
            fovfinal = 72;
            camOriginalOffset = Cam.transform.position;
            init = new Vector3 ( Cam.transform.position.x , Cam.transform.position.y, Cam.transform.position.z);
            final = new Vector3 (Cam.transform.position.x, Cam.transform.localPosition.y, Cam.transform.position.z - 1.25f);
        } else { //Acercando y normalizando zoom post-batalla
            fovinit = 72;
            fovfinal = 60;
            init = Cam.transform.position;
            final = camOriginalOffset;
        }

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            Cam.fieldOfView = Mathf.Lerp(fovinit, fovfinal, normalizedTime);
            Cam.transform.position = Vector3.Lerp(init, final, normalizedTime );
 
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            //someColorValue = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        if (!status) {
            LevelOptions.PlayerCanMove = true;
        }
        //LevelOptions.PlayerCanMove = true;
        //someColorValue = end; //without this, the value will end at something like 0.9992367
    }



    private void OnTriggerEnter (Collider other) {
        //print (other.tag);

        string taginfo = other.tag;
        switch (taginfo) {

            case "Combate":
                CanvasCombate.SetActive(true);
                other.GetComponent<Combate>().FirstStep();
                //GetComponent<StarterAssets.StarterAssetsInputs>().cursorLocked = false;

                break;
            case "Puerta":
                //print ("Tengo la llave? Abre");
                //print ("Caso contrario, envio mensaje que requiero llave");

                other.GetComponent<Requiere>().Lista(PlayerInventory.myItems);
                break;
            case "Item":
                //print ("Agrega los items" + other.GetComponent<Item_Container>().contains[0] );
                string texto = "Add items:\n ";
                foreach (Item_Logic itemToAdd in other.GetComponent<Item_Container>().contains) {
                    texto += itemToAdd.name + "\n";
                    PlayerInventory.AddItem(itemToAdd);
                }
                CanvasTexto.transform.GetChild(0).GetComponent<Text>().text = texto;
                CanvasTexto.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit (Collider other) {
        string taginfo = other.tag;
        switch (taginfo) {
            case "Combate":
                CanvasCombate.SetActive(false);
                enemigos.Remove(other.GetComponent<Enemigo>());

                //GetComponent<StarterAssets.StarterAssetsInputs>().cursorLocked = true;

                break;
            case "Puerta":
                CanvasTexto.SetActive(false);
                break;
        }
        CanvasTexto.SetActive(false);
    }

}
