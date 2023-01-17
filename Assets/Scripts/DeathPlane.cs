using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : Interactable
{
    [SerializeField] private float deathdelay = 1f;
    [SerializeField] private UIGame ui;


    
    public override void Interacion(GameObject player)
    {
        new WaitForSeconds(deathdelay);
        player.GetComponent<PlayerMovement>().ResetPlayer();
        ui.DeathTransition();
    }


}
