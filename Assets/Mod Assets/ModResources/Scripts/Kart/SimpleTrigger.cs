using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{

    public Rigidbody triggerBody; 
    public UnityEvent onTriggerEnter;


    void OnTriggerEnter(Collider other){
        //do not trigger if there's no trigger target object
        if (triggerBody == null) return;        // code de securité, pour que seulement un rigidbody sais le trigger

        //only trigger if the triggerBody matches
        var hitRb = other.attachedRigidbody;    //hitRb = hit Rigidbody 
        if (hitRb == triggerBody){
            onTriggerEnter.Invoke();            // Invoke() = appelle la fonction dans unity
        }
    }

}
