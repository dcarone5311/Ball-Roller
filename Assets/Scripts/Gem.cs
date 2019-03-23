using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    //public GameObject PickupEffect;
    private float count;
    private Vector3 InitialPosition;
    // Start is called before the first frame update
    void Start()
    { //spawn with random color
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_SpecColor", new Color(Random.value, Random.value, Random.value));
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation/hover effect of powerup model
        if (count >= 360 * 200 * Mathf.PI)
        { count = 0f; }//prevent overflow.
        count += 1f;
        transform.position = InitialPosition + new Vector3(0f, .5f * Mathf.Sin(count / 100f), 0f);
        transform.RotateAroundLocal(gameObject.transform.up, .03f);
    }
    void OnTriggerEnter(Collider Other) //activated when touched by other
    {

        if (Other.name == "Player")
        {
            GameObject.Find("Player").GetComponent<TimeScore>().gems += 1;
            GameObject.Destroy(gameObject, 0f); //0f is adjustable delay time
        }
    }
  
}
