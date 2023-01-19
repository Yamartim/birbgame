using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// basic abstract class just for the ui objects have access to this useful utility function
public abstract class UIManager : MonoBehaviour
{

    // transforms a number of seconds into a string formatted as MM:SS
    protected string FloatTimeToString(float time)
    {
        string minutes, seconds;

        minutes = Mathf.FloorToInt(time / 60).ToString();
        seconds = Mathf.FloorToInt(time % 60).ToString();

        return $"{minutes}:{seconds}";
    }

}
