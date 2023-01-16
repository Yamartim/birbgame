using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{

    public abstract void Interacion(GameObject player);
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Interacion(other.gameObject);
        }
    }

}
