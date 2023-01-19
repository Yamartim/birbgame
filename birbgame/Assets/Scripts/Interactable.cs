using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an abstract class for objects the player interacts with by entering a trigger
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{

    public abstract void Interacion(GameObject player);
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) // comparetag is more optimal than ==
        {
            Interacion(other.gameObject);
        }
    }

}
