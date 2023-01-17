using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class CoinScript : Interactable
{
    // [SerializeField] Animator anim;

    // declaring the ecent that can be subscribed to by the many other classes that interact with coins
    public static event Action OnCoinCollect;

    public override void Interacion(GameObject p)
    {
        gameObject.SetActive(false);
        OnCoinCollect?.Invoke(); //the question mark identifies if the event exists first
        //release particles and some sfx
    }
}