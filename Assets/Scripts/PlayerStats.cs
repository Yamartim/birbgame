using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int coinstotal { get; private set;}
    [SerializeField] public int coinscollected { get; private set;} = 0;

    private void OnEnable() {
        CoinScript.OnCoinCollect += AddCoin;
    }
    private void OnDisable() {
        CoinScript.OnCoinCollect -= AddCoin;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinscollected = 0; 
    }

    public void AddCoin()
    {
        coinscollected++;
    }

}
