using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_MenuInit : MonoBehaviour {

    public GameObject CanvasLevelMenu;
    public FirstPersonController FpsController;
    public bool PlayerCanMove = false;


    private void Start () {
        FpsController = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }

    public void StartLevel () {
        FpsController.lockCursor = true;
        CanvasLevelMenu.SetActive(false);
        PlayerCanMove = true;
    }

}
