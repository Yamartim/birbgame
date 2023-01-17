using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for handling the wing functionality
public class PlayerWings : MonoBehaviour
{
    [SerializeField] Transform wingObj;
    List<GameObject> wingList;

    // fetch a list of pairs of wings that are under this object
    void Start()
    {
        wingList = new List<GameObject>();
        foreach (Transform wingPair in wingObj)
        {
            //Debug.Log(wingpair.name);
            wingList.Add(wingPair.gameObject);
        }
    }

    // this method is called when the player "levels up" and more wings are revealed
    public void UpdateWings(int currentwings, int maxwings)
    {
        if(currentwings <= maxwings) {
            wingList[currentwings].SetActive(true);
        }
    }

}
