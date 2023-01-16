using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject htppanel;

    [SerializeField]
    private TMP_Text besttime;

    private GameManager gm;




    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;


        if (gm.gamecleared) {
            besttime.text = "Your best time is: " + gm.GetBestTimeStr();
        } else {
            besttime.text = "";
        }
        
    }

    public void StartGame()
    {
        gm.LoadGame();
    }

    public void ShowHTP()
    {
        htppanel.SetActive(true);
    }

    public void HideHTP()
    {
        htppanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
