using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWings : MonoBehaviour
{
    [SerializeField] Transform wingobj;
    List<GameObject> winglist;

    // Start is called before the first frame update
    void Start()
    {
        winglist = new List<GameObject>();
        foreach (Transform wingpair in wingobj)
        {
            //Debug.Log(wingpair.name);
            winglist.Add(wingpair.gameObject);
        }
    }

    public void UpdateWings(int currentwings, int maxwings)
    {
        if(currentwings <= maxwings) {
            winglist[currentwings].SetActive(true);
        }
    }

}
