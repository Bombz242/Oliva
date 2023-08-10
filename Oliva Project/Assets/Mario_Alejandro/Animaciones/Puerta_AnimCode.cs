using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_AnimCode : MonoBehaviour {


    public Animator anim;

    public GameObject vfx;

    private void OnDestroy () {
        Instantiate(vfx, transform.position, transform.rotation);
        //GetComponent<Puerta_AnimCode>().anim.SetBool("Show", false);
    }

}
