using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    [Header("Physics")]
    public float rollForce = 20f;
    public float rollTorque = 10f;
    public float jumpSpeed = 20f;
    public float maxAV;
    public float Gravity;

    [Header("Other")]
    public GameObject Camera;
    

    private int Numnow, Numprev;
    private Vector3 normal;
    private bool isGrounded,Touching,Release;
    private int  NonGroundCount; //holds up to 5 contact surfaces. 9 is ground layrer

    //FOR DEBUGGING
    private int counter;

	// Use this for initialization
	void Start () {
        Camera = GameObject.Find("Main Camera");
        Touching = false;
        Release = false;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Gravity * Vector3.down;
        rb.maxAngularVelocity=maxAV;
        counter = 0;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
         
        //Check for roll of wall or surface
        if (Numnow>Numprev) //for on collisionstay to communicate with fixed update, an int will update each
        { Touching = true; } //time oncollisionstay is called, fixedupdate knows if it was just called if this
        else   
        { Touching = false; }
        if(Release && !Touching)
        {isGrounded = false;}
        
        //off ground if jumped (or roll off ^)
        if (Input.GetButton("Jump") && isGrounded /*&& rb.angularVelocity== Vector3.zero*/) //can only have no spinon flat stuface
        {
            rb.velocity += jumpSpeed * normal;
            isGrounded = false;
        }
        Vector3 cross = Vector3.Cross(-Physics.gravity, Camera.transform.forward);
        if (isGrounded)
        {
            rb.AddForce(rollForce*(Input.GetAxisRaw("Vertical") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.forward, -Physics.gravity)) +
                Input.GetAxisRaw("Horizontal") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.right, -Physics.gravity))));
        }
        else
        {
            rb.AddTorque(rollTorque * (-Input.GetAxisRaw("Horizontal") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.forward, -Physics.gravity)) +
                Input.GetAxisRaw("Vertical") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.right, -Physics.gravity))));
            rb.AddForce(rollForce/2 * (Input.GetAxisRaw("Vertical") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.forward, -Physics.gravity)) +
                Input.GetAxisRaw("Horizontal") * Vector3.Normalize(Vector3.ProjectOnPlane(Camera.transform.right, -Physics.gravity))));
        }

        
        Numprev = Numnow;
    }
    
    void OnCollisionEnter(Collision OCE)
    {
        Release = false; //no longer off
        if (OCE.contacts[0].otherCollider.gameObject.layer.ToString() == "9" || 
            Vector3.Dot(OCE.contacts[0].normal,-Vector3.Normalize(Physics.gravity)) > 0.4f)
        { //can jump off ground layer or if normal is angled upward
            isGrounded = true;
            normal = Vector3.Normalize(OCE.contacts[0].normal);
        }
    }
    void OnCollisionStay(Collision OCE)
    {
        Numnow += 1;
        normal = Vector3.Normalize(OCE.contacts[0].normal);
    }
    void OnCollisionExit(Collision OCE)
    {
        Release = true;
    }


}
