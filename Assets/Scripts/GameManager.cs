using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float besttime { get; private set;}
    public bool gamecleared { get; private set;} = false;

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

/* 
    private void OnEnable() {
        CoinScript.OnCoinCollect += SaveCoins;
    }
    private void OnDisable() {
        CoinScript.OnCoinCollect -= SaveCoins;
    }
 */


    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


    public bool SetBestTime()
    {
        float cleartime = Time.timeSinceLevelLoad;

        if(!gamecleared)
        {
            gamecleared = true;
            besttime = cleartime;
            PlayerPrefs.SetFloat("BestTime", cleartime);

            return true;

        } else if (cleartime < PlayerPrefs.GetFloat("BestTime")){

            PlayerPrefs.SetFloat("BestTime", cleartime);
            return true;

        } else {
            return false;
        }

    }


    void SaveCoins()
    {
        CoinManager cm = null; //FindObjectOfType<CoinManager>();
        CoinScript[] coins = cm.coinsinscene;
        int total = cm.cointotal;

        bool[] coinstracker = new bool[total];

        for (int i = 0; i < total; i++)
        {
            if(coins[i].gameObject.activeInHierarchy) {
                coinstracker[i] = true;
            } else {
                coinstracker[i] = false;
            }
        }

        BitArray bits = new BitArray(coinstracker);
        
        int[] binconversion = new int[1];
        bits.CopyTo(binconversion, 0);

        PlayerPrefs.SetInt("CoinsCollected", binconversion[0]);

        //I wont go into implementing loading as this is already quite out of scope, but this is a very optimal way of storing data such as collectables obtained

    }

}
