using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{

    public MeshRenderer MR;
    public float RespawnTime = 5f;
    public string PowerUpName;
    //public GameObject PickupEffect;
    private float count;
    private Vector3 InitialPosition;
    private float PlayerJump;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation/hover effect of powerup model
        if (count >= 360* 200 *Mathf.PI)
        {count = 0f; }//prevent overflow.
        count += 1f;
        transform.position =  InitialPosition + new Vector3(0f, .5f * Mathf.Sin(count/100f), 0f);
        transform.RotateAroundLocal(gameObject.transform.up,.03f);
    }
    void OnTriggerEnter(Collider Other) //activated when touched by other
    {



        if (Other.name == "Player" &&
            !(PowerUpName == "Gravity Modifier" && Vector3.Angle(gameObject.transform.up, Vector3.Normalize(Physics.gravity)) < 0.1f))
            //if gravity already set, don't pickup!
            StartCoroutine (Pickup(Other));
    }
    IEnumerator Pickup(Collider Player)
    {//? to prevent NullReferenceError
        if (Player.GetComponent<PowerUps>().CurrentPowerUp == "None" || (PowerUpName=="Gravity Modifier") )
        { //gravity modifiers don't have to have empty slot to pick up; also, must be oriented to CHANGE gravity.
            
            //spawn cool effeft

            //Instantiate(PickupEffect, transform.position, transform.rotation);

            //grant effect to player
            PowerUps PU = Player.GetComponent<PowerUps>();
            if (PowerUpName == "Gravity Modifier")
                Physics.gravity = gameObject.transform.up*Vector3.Magnitude(Physics.gravity);
            else
            PU.CurrentPowerUp = PowerUpName;

            //destory object [Destroy(gameObject);] or disappear
            MR.enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(RespawnTime);//dissappear for a preset 
                                                         //respawn time then reappear into the scene.
            MR.enabled = true;
            GetComponent<Collider>().enabled = true;

        }
    }
}
