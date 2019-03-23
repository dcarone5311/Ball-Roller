using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScore : MonoBehaviour
{
    public Text TimeText, GemsText;
    public int gems, TotalGems;
    
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        gems = 0; //start with none collected
        TotalGems= GameObject.FindGameObjectsWithTag("Gem").Length; //get how many gems are present.
        TimeText.text = "0.00s";
        GemsText.text = "0/0";

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        string seconds = (time%60).ToString("0#.00");
        string minutes = Mathf.FloorToInt(time / 60).ToString("0#");

        TimeText.text = minutes + ":" + seconds;
        if (TotalGems== 0)
        {

            GemsText.text = "";
        }
        else
        {

            GemsText.text = "Gems Collected: " + gems.ToString() + '/' + TotalGems.ToString();
        }
    }
}
