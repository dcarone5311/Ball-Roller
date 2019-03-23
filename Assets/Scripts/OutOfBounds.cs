using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter(Collider Other)
    {
        if (Other.name == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
