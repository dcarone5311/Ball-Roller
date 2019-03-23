using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider Other) //activated when touched by other
    {
        if (Other.name == "Player" && GameObject.Find("Player").GetComponent<TimeScore>().gems== GameObject.Find("Player").GetComponent<TimeScore>().TotalGems) //must also have collected all gems
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
