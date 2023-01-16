using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int cointotal { get; private set;}

    public CoinScript[] coinsinscene { get; private set;}



    void Awake()
    {
        cointotal = transform.childCount;
        coinsinscene = gameObject.GetComponentsInChildren<CoinScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
