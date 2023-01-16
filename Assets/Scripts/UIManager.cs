using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class UIManager : MonoBehaviour
{

    protected string FloatTimeToString(float time)
    {
        string minutes, seconds;

        minutes = Mathf.FloorToInt(time / 60).ToString();
        seconds = Mathf.FloorToInt(time % 60).ToString();

        return $"{minutes}:{seconds}";
    }

}
