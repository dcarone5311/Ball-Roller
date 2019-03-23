using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public string CurrentPowerUp = "None";
    public float SuperJumpSpeed = 50f;
    public float SuperSpeed = 30f;
    public GameObject JetPackPrefab;
    public float PowerUpDuration = 10f;
    public float JetPackForce = 10f;

    private float TimeLeft;
    private GameObject Camera,JetPack,FT0,FT1;
    private PlayerMovement MoveData;
    
    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = 0f;
        CurrentPowerUp = "None";
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLeft >0f) //when using a powerup that lasts a duration
        {
            if (CurrentPowerUp == "JetPack")
            {
                JetPack.transform.position = transform.position - 0.5f*Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.forward, -Physics.gravity));
                JetPack.transform.LookAt(transform, -Physics.gravity);
                JetPack.transform.Rotate(0f, -90f, 0f);

                if (Input.GetButton("Jump"))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(- JetPackForce*Vector3.Normalize(Physics.gravity));
                    FT0.active = true;
                    FT1.active = true;
                }
                else
                {
                    FT0.active = false; //disable fire when jetpack isnt being used.
                    FT1.active = false;
                }
            }
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)//timme runs out on duration powerup
            {
                Destroy(JetPack);
                CurrentPowerUp = "None";
                TimeLeft = 0f;
            }
        }


        if(CurrentPowerUp=="JetPack" && Input.GetButtonDown("PowerUp") && TimeLeft == 0f) //initial
        {
            TimeLeft = PowerUpDuration;
            JetPack=Instantiate(JetPackPrefab,transform.position,new Quaternion (0f,0f,0f,0f) );
            FT0 = GameObject.Find("FlameThrower");
            FT1 = GameObject.Find("FlameThrower (1)");
        }


        //Non duration powerups
        if (Input.GetButtonDown("PowerUp") && TimeLeft == 0f) //press powerup button to use powerup
        {
            switch (CurrentPowerUp)
            {
                case "SuperJump":                                    //jump upwards, cancelling ALL downward momentum!
                    GetComponent<Rigidbody>().velocity += SuperJumpSpeed* Vector3.Normalize(-Physics.gravity) - Vector3.Project(GetComponent<Rigidbody>().velocity,Physics.gravity);
                    break;
                case "SuperSpeed":
                    GetComponent<Rigidbody>().velocity += SuperSpeed * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.forward, -Physics.gravity));
                    break;
                case "None":
                    break;
                default:
                    break;
            }
            if (CurrentPowerUp != "JetPack")
            {
                CurrentPowerUp = "None"; //powerup has been used.
            }
            
        } 
    }

}
