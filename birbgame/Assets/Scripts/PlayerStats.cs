using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for handling the coins the player collects
public class PlayerStats : MonoBehaviour
{
    PlayerWings wingscript;
    [SerializeField] CoinManager cm;

    [SerializeField] public int coinscollected { get; private set;} = 0;

    // subscribe to the coin collection event so the player levels up accordingly
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
        wingscript = gameObject.GetComponent<PlayerWings>();
    }

    public void AddCoin()
    {
        coinscollected++;
        wingscript.UpdateWings(coinscollected, cm.coinTotal);
    }

}
