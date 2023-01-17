using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class holds references for every coin in the scene that can be used by other objects
public class CoinManager : MonoBehaviour
{
    public int coinTotal { get; private set;}
    public CoinScript[] coinsInScene { get; private set;}


    // all coins in the scene have to be chlidren of this object
    void Awake()
    {
        coinTotal = transform.childCount;
        coinsInScene = gameObject.GetComponentsInChildren<CoinScript>();
    }

}
