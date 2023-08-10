using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Trigger : MonoBehaviour {

    public EnemyController enemyRef;



    private void OnTriggerEnter (Collider other) {
        print ("PLAYER ENTRO");


    }

}
