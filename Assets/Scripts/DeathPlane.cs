using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : Interactable
{
    [SerializeField] private float deathDelay = 1f;
    [SerializeField] private UIGame ui;


    // when the player touches the death plane we have to reset their position and play a transition
    public override void Interacion(GameObject player)
    {
        new WaitForSeconds(deathDelay);
        player.GetComponent<PlayerMovement>().ResetPlayer();
        ui.DeathTransition();
    }


}
