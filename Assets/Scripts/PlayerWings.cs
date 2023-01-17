using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWings : MonoBehaviour
{
    [SerializeField] Transform wingObj;
    List<GameObject> wingList;

    // Start is called before the first frame update
    void Start()
    {
        wingList = new List<GameObject>();
        foreach (Transform wingPair in wingObj)
        {
            //Debug.Log(wingpair.name);
            wingList.Add(wingPair.gameObject);
        }
    }

    public void UpdateWings(int currentwings, int maxwings)
    {
        if(currentwings <= maxwings) {
            wingList[currentwings].SetActive(true);
        }
    }

}
