using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : Interactable
{
    [SerializeField] private GameObject endpanel;


    public override void Interacion(GameObject p)
    {
        GameManager.Instance.SetBestTime();
        endpanel.SetActive(true);
    }
    
}
