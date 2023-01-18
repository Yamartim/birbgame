using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ui management for the game scene
public class UIGame : UIManager
{

#region references

    [SerializeField] private CoinManager cm;
    [SerializeField] private GameObject coinDisplay, coinPrefab, pausePanel, endPanel, transitionObj; 
    [SerializeField] private TMP_Text resultText;
    
    [SerializeField] private Image[] coinImgs;

    [SerializeField] private TMP_Text timerDisplay;

    
#endregion

    // subscribing to the coin collection event so the display is updated accordingly
    private void OnEnable() {
        CoinScript.OnCoinCollect += UpdateDisplay;
    }
    private void OnDisable() {
        CoinScript.OnCoinCollect -= UpdateDisplay;
    }

    // when the game starts, an icon is associated with each coin on the map and placed on the ui
    void Start()
    {
        coinImgs = new Image[cm.coinTotal];

        GameObject aux;
        for (int i = 0; i<cm.coinTotal; i++)
        {
            aux = Instantiate(coinPrefab);
            aux.transform.SetParent(coinDisplay.transform);
            coinImgs[i] = aux.GetComponent<Image>();
        }
        UpdateDisplay();

        timerDisplay.text = "";
        
        if(GameManager.Instance.gameCleared)
        {
            InvokeRepeating("UpdateTimer", 0f, 1f);
        }
        
    }

#region methods

    // if a coin is collected the equivalent coin on the ui will turn gray
    void UpdateDisplay()
    {
        for (int i = 0; i<cm.coinTotal; i++)
        {
            if(cm.coinsInScene[i].gameObject.activeInHierarchy){
                coinImgs[i].color = Color.yellow;
            } else {
                coinImgs[i].color = Color.gray;
            }
        } 
    }

    void UpdateTimer()
    {
        timerDisplay.text = FloatTimeToString(Time.timeSinceLevelLoad);
    }

    public void ShowEndPanel(bool newrecord)
    {
        endPanel.SetActive(true);
        resultText.text = $"You reached the top in {FloatTimeToString(Time.timeSinceLevelLoad)}";

        if(newrecord){
            resultText.text += "\nNEW RECORD";
        }
    }

    public void DeathTransition()
    {
        transitionObj.GetComponent<Animator>().SetTrigger("START");
        //transitionobj.GetComponent<Animator>().SetTrigger("END");
    }

#endregion

}
