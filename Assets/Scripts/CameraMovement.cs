using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public GameObject Follow;
    public Transform cameraTransform;
    public float height,radius;

    private Vector3 grav,gravprev;//gravity from previous frams

    private Vector3 disp;//displacement between cam and player to keep the camera follwing the player
	// Use this for initialization
	void Start () {
        Follow = GameObject.Find("Player");
        transform.position = Follow.transform.position + Vector3.back * radius;
        disp = cameraTransform.position - Follow.transform.position; //new use of disp
        grav = Vector3.Normalize(Physics.gravity);
        gravprev = grav;
    }
	
	// Update is called once per frame
	void Update () {
        cameraTransform.position = radius* Vector3.Normalize(disp) + Follow.transform.position;
        grav = Vector3.Normalize(Physics.gravity);
        
        if(grav != gravprev)
        {
            cameraTransform.LookAt(Follow.transform, -grav);
        }
        if (Input.GetButton("Mouse Right"))
        {
            float angle = Vector3.Angle(grav, cameraTransform.forward);
            if (angle < 20f && Input.GetAxis("Mouse Y") > 0f //this keeps the camera from flipping over vertial axis
                || angle > 160f && Input.GetAxis("Mouse Y") < 0f
                || (angle <160f && angle >20f))//in valid range or exiting invalid range.
            {
                cameraTransform.RotateAround(Follow.transform.position,
                    Vector3.Cross(grav, cameraTransform.forward), Input.GetAxis("Mouse Y") * 20f /* sensetivity 180 / Mathf.PI*/);
            }
        }
        else
        {
            cameraTransform.RotateAround(Follow.transform.position, -grav, Input.GetAxis("Mouse X") * 180 / Mathf.PI);
        }
        
        radius = Mathf.Clamp(radius-5*Input.GetAxis("Mouse ScrollWheel"), 3f,7f);
        disp = cameraTransform.position - Follow.transform.position;
        cameraTransform.Translate(height *Vector3.up);//calculations done without this extra height, but it
        //is added at the end of the frame.
        gravprev = grav;
        
    }
}
