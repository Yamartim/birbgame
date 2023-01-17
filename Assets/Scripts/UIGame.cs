using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIGame : UIManager
{


    [SerializeField] private CoinManager cm;
    [SerializeField] private GameObject coindisplay, coinprefab, pausepanel, endpanel, transitionobj; 
    [SerializeField] private Image[] coinimgs;

    GameManager gm;
    [SerializeField] private TMP_Text timerdisplay;
    [SerializeField] private TMP_Text resulttext;

    


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
        coinimgs = new Image[cm.cointotal];


        GameObject aux;
        for (int i = 0; i<cm.cointotal; i++)
        {
            aux = Instantiate(coinprefab);
            aux.transform.SetParent(coindisplay.transform);
            coinimgs[i] = aux.GetComponent<Image>();
        }
        UpdateDisplay();

        timerdisplay.text = "";
        
        if(gm.gamecleared)
        {
            InvokeRepeating("UpdateTimer", 0f, 1f);
        }
        
    }


    void UpdateDisplay()
    {
        for (int i = 0; i<cm.cointotal; i++)
        {
            if(cm.coinsinscene[i].gameObject.activeInHierarchy){
                coinimgs[i].color = Color.yellow;
            } else {
                coinimgs[i].color = Color.gray;
            }
        } 
    }

    void UpdateTimer()
    {
        timerdisplay.text = FloatTimeToString(Time.timeSinceLevelLoad);
    }

    public void ShowEndPanel(bool newrecord)
    {
        endpanel.SetActive(true);
        resulttext.text = $"You reached the top in {FloatTimeToString(Time.timeSinceLevelLoad)}";

        if(newrecord){
            resulttext.text += "\nNEW RECORD";
        }
    }

    public void DeathTransition()
    {
        transitionobj.GetComponent<Animator>().SetTrigger("START");
        //transitionobj.GetComponent<Animator>().SetTrigger("END");
    }

}
