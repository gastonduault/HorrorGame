using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/****************************************
 * Author: Julie
 * Date: 14/01/2025
 * General description: This script causes a mirror to shatter on impact with the ground. The intact mirror is replaced by a broken version, and physics is activated on the broken pieces to simulate the breaking effect.
 ****************************************/
public class MirrorBreak : MonoBehaviour
{

    public GameObject glass;
    public GameObject brokenGlass;
    public Rigidbody rb;
    public XRGrabInteractable grabInteractable;

    public Vector3 offsetOnGrab = new Vector3(0, 0, 0.2f); // Décalage en avant

    void Start()
    {
        //Set the broken glass to be inactive at the start of the game
        glass.SetActive(true);
        brokenGlass.SetActive(false);
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnCollisionEnter(Collision collision)
    {
        BreakMirror();
    }

    public void OnReleased(XRBaseInteractor interactor)
    {
        Transform interactorTransform = interactor.transform;
        glass.transform.position = interactorTransform.position + interactorTransform.forward * offsetOnGrab.z
                                  + interactorTransform.right * offsetOnGrab.x
                                  + interactorTransform.up * offsetOnGrab.y;
        rb.useGravity = true;
    }

    void BreakMirror(){
        brokenGlass.transform.position = glass.transform.position;
        brokenGlass.transform.rotation = glass.transform.rotation;
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
