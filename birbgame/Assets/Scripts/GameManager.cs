using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// class that coordinates the rest of the game
public class GameManager : MonoBehaviour
{

    public float bestTime { get; private set;}
    public bool gameCleared { get; private set;} = false;

    // singleton pattern for this object to be accessible always
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // for an unused data persistance feaature
/* 
    private void OnEnable() {
        CoinScript.OnCoinCollect += SaveCoins;
    }
    private void OnDisable() {
        CoinScript.OnCoinCollect -= SaveCoins;
    }
 */

#region scene management functions

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


#endregion

#region setting player data

    public bool SetBestTime()
    {
        float clearTime = Time.timeSinceLevelLoad;

        if(!gameCleared)
        {
            gameCleared = true;
            bestTime = clearTime;
            PlayerPrefs.SetFloat("BestTime", clearTime);

            return true;

        } else if (clearTime < PlayerPrefs.GetFloat("BestTime")){

            PlayerPrefs.SetFloat("BestTime", clearTime);
            return true;

        } else {
            return false;
        }

    }


    void SaveCoins()
    {
        CoinManager cm = null; //FindObjectOfType<CoinManager>();
        CoinScript[] coins = cm.coinsInScene;
        int total = cm.coinTotal;

        bool[] coinsTracker = new bool[total];

        for (int i = 0; i < total; i++)
        {
            if(coins[i].gameObject.activeInHierarchy) {
                coinsTracker[i] = true;
            } else {
                coinsTracker[i] = false;
            }
        }

        BitArray bits = new BitArray(coinsTracker);
        
        int[] binConversion = new int[1];
        bits.CopyTo(binConversion, 0);

        PlayerPrefs.SetInt("CoinsCollected", binConversion[0]);

        //I wont go into implementing loading as this is already quite out of scope, but this is a very optimal way of storing data such as collectables obtained

    }

#endregion
}
