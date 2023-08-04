using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Level_StartGame : MonoBehaviour {

    public void Game_Play () {
        SceneManager.LoadScene(1 , LoadSceneMode.Single);
    }


}
