using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : Interactable
{
    [SerializeField] private UIGame ui;


    public override void Interacion(GameObject p)
    {
        bool gotnewrecord = GameManager.Instance.SetBestTime();
        ui.ShowEndPanel(gotnewrecord);
    }
    
}
