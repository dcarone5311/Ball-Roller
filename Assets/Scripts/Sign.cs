using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false); //hide test
    }
    
    void OnTriggerEnter(Collider Other)
    {
        if (Other.name == "Player")
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    void OnTriggerExit (Collider Other) //show text when player is in certain radius.
    {
        if (Other.name == "Player")
        {

            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
