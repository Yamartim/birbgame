using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : Interactable
{
    [SerializeField] private float deathDelay = 3f;
    [SerializeField] private UIGame ui;
    bool sequenceRunning = false;


    // when the player touches the death plane we have to reset their position and play a transition
    public override void Interacion(GameObject player)
    {
        if (!sequenceRunning)
        {
            ui.DeathTransition();
            sequenceRunning = true;
            StartCoroutine(DeathSequence(player));
        }
    }


    IEnumerator DeathSequence(GameObject player)
    {
        yield return new WaitForSeconds(deathDelay);
        player.GetComponent<PlayerMovement>().ResetPlayer();
        sequenceRunning = false;
    }


}
