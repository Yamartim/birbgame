using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] [Range (0, 5)]
    private int coinscollected = 0;
    //private time playtime



    // Start is called before the first frame update
    void Start()
    {
        coinscollected = 0;
    }


    public int GetCoinTotal()
    {
        return coinscollected;
    }

    public void AddCoin()
    {
        coinscollected++;
    }

}
