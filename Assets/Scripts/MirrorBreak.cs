using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/****************************************
 * Author: Julie
 * Date: 14/01/22025
 * General description: This script causes a mirror to shatter on impact with the ground. The intact mirror is replaced by a broken version, and physics is activated on the broken pieces to simulate the breaking effect.
 ****************************************/
public class MirrorBreak : MonoBehaviour
{

    public GameObject glass;
    public GameObject brokenGlass;
    void Start()
    {
        //Set the broken glass to be inactive at the start of the game
        glass.SetActive(true);
        brokenGlass.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        BreakMirror();
    }

    void BreakMirror(){
        glass.SetActive(false);
        brokenGlass.SetActive(true);
        Rigidbody[] rbs = brokenGlass.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        
    }
}
