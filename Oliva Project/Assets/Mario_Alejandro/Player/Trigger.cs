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

    public GameObject CanvasPrincipal;
    public InputField input;

    public List<EnemyCombat> enemigos;

    public Level_MenuInit LevelOptions;
    public Camera Cam;

    public Vector3 camOriginalOffset;

    private void Start () {
        MyTrigger = this;
        PlayerInventory = GetComponent<Inventory_Logic>();
        LevelOptions = GameObject.Find("Canvas_Level").GetComponent<Level_MenuInit>();
        Cam = Camera.main;
        CanvasTexto.SetActive(false);
        CanvasPrincipal.SetActive(false);
    }

    public void CheckUIEnemies () {
        if (enemigos.Count <= 0) {
            CanvasPrincipal.SetActive(false);
        } else {
            CanvasPrincipal.SetActive(true);
        }
    }



    private void Update () {

        LevelOptions.PlayerCanMove = !input.isFocused;


        if (Input.GetKeyDown(KeyCode.F1)) {
            //print ("F1");
            CanvasPrincipal.SetActive(!CanvasPrincipal.activeInHierarchy);
        }

        if (CanvasPrincipal) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                input.Select();
            }
        }

        if (enemigos.Count > 0) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                CombatEnemie(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                CombatEnemie(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                CombatEnemie(2);
            }


            if (Input.GetKeyDown(KeyCode.Return)) {
                input.Select();
            }
        }
    }


    public List<string> cmdText;


    public void SubmitCommand () {
        //print ("LIMPIANDO");
        input.transform.parent.GetChild(0).GetChild(0).GetComponent<Text>().text = "";

        if (cmdText.Count > 5) {
            //borra el primer indice
            cmdText.RemoveAt(0);
        }


        if (ResultCommand(input.text, "enemy")) {
            print ("DAÑO");
        }

        string  talk = TalkCommand(input.text, "enemy");
        cmdText.Add(talk);

        foreach ( string txt in cmdText) {
            input.transform.parent.GetChild(0).GetChild(0).GetComponent<Text>().text += txt + "\n";
        }
        //input.transform.parent.GetChild(0).GetChild(0).GetComponent<Text>().text += input.text + "\n" ;
        input.text = "";
    }


    public string[] CheckCommands(string mytxt, string enemytxt) {
        string[] t;

        mytxt = mytxt.ToLower(); //Se asegura de convertir el string en lower
        mytxt = mytxt.Replace(" ", ""); //Reemplaza cualquier espacio por una cadena unica de texto

        t = mytxt.Split(".");

        return t;
    }


    public bool ResultCommand (string mytxt, string enemytxt) {

       // mytxt = mytxt.ToLower(); //Se asegura de convertir el string en lower
       // mytxt = mytxt.Replace(" ", ""); //Reemplaza cualquier espacio por una cadena unica de texto
       // bool result = mytxt == enemytxt; // Compara que el resultado final sea el mismo tipo


        string[] t = CheckCommands(mytxt, enemytxt);
        bool result = false;

        //print (t.Length);

        if (t.Length > 1) {
            result = t[1] == enemytxt;
        }

        return result;
    }

    public string TalkCommand (string mytxt, string enemytxt){
        string finaltxt = "";

        string[] t = CheckCommands(mytxt, enemytxt);
        bool result = false;
        if (t.Length > 1) {
            result = t[1] == enemytxt;
        }

        //mytxt = mytxt.ToLower(); //Se asegura de convertir el string en lower
        //mytxt = mytxt.Replace(" ", ""); //Reemplaza cualquier espacio por una cadena unica de texto
        //string[] cadena = mytxt.Split(".");


        //bool result = mytxt == enemytxt; // Compara que el resultado final sea el mismo tipo

        if (result) {
            finaltxt += mytxt + " / " + enemytxt + " OVERLOAD";
        } else {


            switch (t[0]) {

                case "help":
                    finaltxt += "$sudo "+ mytxt + " #HELP COMMANDS: 'clean.type' 'frezze.type' 'help.type' 'open.virus/spam/glitch' ";
                    break;
                
                case "use":
                    finaltxt += "$sudo "+ mytxt + " #USING ITEM";
                    break;

                case "clean":
                    finaltxt += "$sudo "+ mytxt + " #CLEAN DATA CORRUPTION";
                    break;

                case "open":
                    finaltxt += "$sudo " +mytxt + " #OPEN FILE " + enemytxt;
                    break;


                default:
                    finaltxt += "$sudo " +mytxt + " #ERROR UNKNOW COMMAND";
                    //finaltxt += mytxt + " / " + enemytxt + " ERROR UNKNOW COMMAND";
                    break;
            }
        }


        return finaltxt;
    }

    




    void CombatEnemie (int myAttack) {

        int enemieWeightAttack = 0;
        int enemieAttack = 0;
        //string txt = GetComponent<Textos>().objMsg + "\n";

        foreach (EnemyCombat enemy in enemigos) {
            for (int i = 0; i < 3; i++) {
                enemieWeightAttack += enemy.afinidad[i];
            }
            int dice = Random.Range(0, enemieWeightAttack);
            for (int t = 0; t < 3; t++) {
                if (dice <= enemy.afinidad[t]) {
                    enemieAttack = t;
                    break;
                }
                dice -= enemy.afinidad[t];
            }

            if (enemieAttack == myAttack) {
                print ("DAÑASTE");
            } else {
                print ("SOBRECARGA");
            }

        }


    }














    /*
    public void CallCombate (int id) {
        foreach(Enemigo enemigo in enemigos) {
            enemigo.GetComponent<Combate>().Resolviendo(id);
        }
    }*/

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
                print ("Abre opciones de combate");

                //enemigos.Add(other.GetComponent<EnemyCombat>());
                //CanvasCombate.SetActive(true);
                //other.GetComponent<Combate>().FirstStep();
                //GetComponent<StarterAssets.StarterAssetsInputs>().cursorLocked = false;

                break;
            case "Puerta":
                //print ("Tengo la llave? Abre");
                //print ("Caso contrario, envio mensaje que requiero llave");

                other.GetComponent<Requiere>().Lista(PlayerInventory.myItems);

                other.GetComponent<Puerta_AnimCode>().anim.SetBool("Show", true);
                other.GetComponent<Puerta_AnimCode>().anim.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = other.GetComponent<Requiere>().Texto(PlayerInventory.myItems);


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
                //enemigos.Remove(other.GetComponent<Enemigo>());

                //GetComponent<StarterAssets.StarterAssetsInputs>().cursorLocked = true;

                break;
            case "Puerta":
                other.GetComponent<Puerta_AnimCode>().anim.SetBool("Show", false);
                CanvasTexto.SetActive(false);
                break;
        }
        CanvasTexto.SetActive(false);
    }

}
