using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public int coinid = -1;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AddCoin();

            GameManager.Instance.SaveCoin(coinid);

            Collect();
        }
    }
    private void Collect()
    {
        //release particles and some sfx
        Destroy(gameObject);
    }

}
