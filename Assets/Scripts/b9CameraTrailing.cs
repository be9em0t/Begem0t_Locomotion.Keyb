using UnityEngine;
using System.Collections;
using System;

public class b9CameraTrailing : MonoBehaviour {

    public float dampPosition = 2f;
    public float dampRotation = .5f;
    public float verticalComponent = 0f;    //should we follow target on y axis. 0=off, 1=100%
 
    public float camDist = 2.5f;          //Initial Camera distance
	public float camHeight = .8f;          //Initial Camera height

    //tweak parameters
    public float rotAround = 0f;           //cameraParent rotation around target
    public float rotDown = 20f;           //cameraParent rotation around target
    public float camVertOffset = 0f;          //camera vertial offset
    public float camZoom = 60f;         //camera FieldOfView
    
    public Transform targetTransf;         //target avatar's transform
    GameObject cameraParent;     //camera's parent object
    Vector3 wantedParentPosition;              //target offset

	void Start () {
        //define target object
        if (targetTransf == null)   //if no target defined
        {
            Debug.Log("Error: no camera target assigned. Assuming Default Avatar.");
            targetTransf = GameObject.Find("DefaultAvatar").transform;                  //get target avatar's transform
        }

        //Create camera rig
        cameraParent = new GameObject("cameraParent");              //create camera's parent object at 0,0,0
        transform.position = new Vector3(0f, 0f, -2.5f);            
        transform.parent = cameraParent.transform;                  //parent camera to cameraParent, so that it follows avatar

    }
	
	void LateUpdate () {

        //follow Avatar with Camera 
        wantedParentPosition = new Vector3(targetTransf.position.x, (targetTransf.position.y * verticalComponent) + camHeight - camVertOffset, targetTransf.position.z + 2.5f - camDist);
        cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, wantedParentPosition, dampPosition * Time.deltaTime);
        cameraParent.transform.rotation = Quaternion.Slerp(cameraParent.transform.rotation, targetTransf.rotation * Quaternion.Euler(rotDown, rotAround, 0f), dampRotation * Time.deltaTime); 

        //Camera look up and down 
        transform.forward = Vector3.Lerp(transform.forward, cameraParent.transform.forward, Time.deltaTime * 2);

        //zoom Camera in and out
        camera.fieldOfView = camZoom;


        PositionChange();
	}

    //Camera Control
    void PositionChange()
    {
        //Rotate Camera Around
        if (Input.GetKey(KeyCode.Period))
        {
            rotAround = rotAround - (200f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Comma))
        {
            rotAround = rotAround + (200f * Time.deltaTime);
        }
        rotAround = rotAround - (Input.GetAxis("HatHorizontal") * 3f);     //stick input
        //Camera up and down
        if (Input.GetKey(KeyCode.Quote))
        {
            rotDown = rotDown - (30 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Slash))
        {
            rotDown = rotDown + (30 * Time.deltaTime);
        }
        //vertOffset = vertOffset + (Input.GetAxis("HatVertical") / 50);  //stick input
        //Camera Zoom
        if (Input.GetKey(KeyCode.PageUp))
        {
            camZoom = camZoom - (10 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.PageDown))
        {
            camZoom = camZoom + (10 * Time.deltaTime);
        }
        camZoom = camZoom + (Input.GetAxis("HatVertical") / 2);  //stick input
        //Reset Camera
        if (Input.GetKey(KeyCode.Home) || Input.GetButtonDown("joystick button 6"))
        {
            rotAround = 0f;
            rotDown = 20f;
            camZoom = 60f;
        }
    }

}

