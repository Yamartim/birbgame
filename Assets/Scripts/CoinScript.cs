using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : Interactable
{
    // [SerializeField] Animator anim;

    public static event Action OnCoinCollect;

    public override void Interacion(GameObject p)
    {
        gameObject.SetActive(false);
        OnCoinCollect?.Invoke();
        //release particles and some sfx
    }
}