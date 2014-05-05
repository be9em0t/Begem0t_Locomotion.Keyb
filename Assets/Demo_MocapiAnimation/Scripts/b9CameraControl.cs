using UnityEngine;
using System.Collections;
using System;

public class b9CameraControl : MonoBehaviour {

    float DampCamera = 2f;	        //Adjust smoothing of camera motion: 0 - infinity, 10 almost instant, default 2
	float camDist = -2.5f;          //Initial Camera distance
	float camHeight = 1f;          //Initial Camera height
	float rotAround = 0f;           //cameraParent rotation around target
	float vertOffset = -1f;          //camera vertial offset
    float cameraZoom = 60f;         //camera FieldOfView

	Transform avatarTransf;         //target avatar's transform
    //Transform avatarLookAt;         //target avatar's hips

    Vector3 camOffset;              //camera offset
    Vector3 targetPos;              //target offset
    Vector3 LookAtTarget;

    public GameObject cameraParent;     //camera's parent object

	void Start () {
        //Create camera hierarchy
        cameraParent = new GameObject("cameraParent");              //create camera's parent object at 0,0,0
        camOffset = new Vector3(0f, camHeight, camDist);            //define the camera offset
        transform.position = camOffset;                             //reposition camera relative to 0,0,0
        transform.parent = cameraParent.transform;                  //parent camera to cameraParent, so that it follows avatar

        avatarTransf = GameObject.Find("MocapiMan").transform;  //get target avatar's transform

    }
	
	void Update () {
        //follow Avatar with Camera 
        targetPos= new Vector3(avatarTransf.position.x, avatarTransf.position.y-vertOffset, avatarTransf.position.z);
        cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, targetPos, DampCamera * Time.deltaTime);
        cameraParent.transform.rotation = Quaternion.Slerp(cameraParent.transform.rotation, avatarTransf.rotation * Quaternion.Euler(0f, rotAround, 0f), DampCamera * Time.deltaTime); 

        //zoom Camera in and out
        camera.fieldOfView = cameraZoom;

        //tweak Camera up and down 
        LookAtTarget = new Vector3(avatarTransf.position.x, avatarTransf.position.y + camHeight + (vertOffset/10), avatarTransf.position.z);
        transform.LookAt(LookAtTarget, Vector3.up);


        PositionChange();
	}

	//Camera Control
	void PositionChange () {
        //Rotate Camera Around
		if (Input.GetKey(KeyCode.Period))
		{
            rotAround = rotAround - (200f * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.Comma))
		{
            rotAround = rotAround + (200f * Time.deltaTime);
		}
        rotAround = rotAround - (Input.GetAxis("HatHorizontal")*3f);     //stick input
        //Camera up and down
		if (Input.GetKey(KeyCode.Quote))
		{
			vertOffset=vertOffset-(1 * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.Slash))
		{
			vertOffset=vertOffset+(1 * Time.deltaTime);
		}
        //vertOffset = vertOffset + (Input.GetAxis("HatVertical") / 50);  //stick input
        //Camera Zoom
        if (Input.GetKey(KeyCode.PageUp))
        {
            cameraZoom = cameraZoom - (10 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.PageDown))
        {
            cameraZoom = cameraZoom + (10 * Time.deltaTime);
        }
        cameraZoom = cameraZoom + (Input.GetAxis("HatVertical")/2);  //stick input
        //Reset Camera
        if (Input.GetKey(KeyCode.Home) || Input.GetButtonDown("joystick button 6"))
        {
            rotAround = 0f;
            vertOffset = 0f;
            cameraZoom = 60f;
        }

	}

}

