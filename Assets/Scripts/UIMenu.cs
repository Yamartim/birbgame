using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenu : UIManager
{

#region variables

    [SerializeField]
    private GameObject htpPanel;

    [SerializeField]
    private TMP_Text bestTime;

    private GameManager gm;

#endregion


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        if (gm.gameCleared) {
            bestTime.text = "Your best time is: " + FloatTimeToString(gm.bestTime);
        } else {
            bestTime.text = "";
        }
        
    }

#region menu button functions

    public void StartGame()
    {
        gm.LoadGame();
    }

    public void ShowHTP()
    {
        htpPanel.SetActive(true);
    }

    public void HideHTP()
    {
        htpPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

#endregion

}
