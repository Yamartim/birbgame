using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, ICollectable
{
    // [SerializeField] Animator anim;

    public static event Action OnCoinCollect;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
    public void Collect()
    {
        gameObject.SetActive(false);
        OnCoinCollect?.Invoke();
        //release particles and some sfx
    }

}
