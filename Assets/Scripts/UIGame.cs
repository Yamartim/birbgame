using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIGame : UIManager
{


    [SerializeField] private CoinManager cm;
    [SerializeField] private GameObject coinDisplay, coinPrefab, pausePanel, endPanel, transitionObj; 
    [SerializeField] private Image[] coinImgs;

    GameManager gm;
    [SerializeField] private TMP_Text timerDisplay;
    [SerializeField] private TMP_Text resultText;

    


    private void OnEnable() {
        CoinScript.OnCoinCollect += UpdateDisplay;
    }
    private void OnDisable() {
        CoinScript.OnCoinCollect -= UpdateDisplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
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
        
        if(gm.gameCleared)
        {
            InvokeRepeating("UpdateTimer", 0f, 1f);
        }
        
    }


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

}
