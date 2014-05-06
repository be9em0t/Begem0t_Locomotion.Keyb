﻿using UnityEngine;
using System.Collections;

public class MocapiCameraScrolling : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
    public float camZoom = 60f;         //camera FieldOfView

    Vector3 cameraOffset;
    Transform avatarTransf;

	void Start()
	{

        avatarTransf = GameObject.Find("Hips").transform;  //get target avatar's transform

    }
	
	void FixedUpdate ()
	{
        PositionChange();

        cameraOffset = new Vector3(0f, 1f, -3f);
        
		// set the camera position and direction
        transform.position = Vector3.Lerp(transform.position, avatarTransf.position + cameraOffset, Time.deltaTime * smooth);	
	}

    //Camera Placement Control
    void PositionChange()
    {
        //Camera Zoom
        camera.fieldOfView = camZoom;
        if (Input.GetKey(KeyCode.PageUp) || Input.GetKey(KeyCode.KeypadMinus))
        {
            camZoom = camZoom - (10 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.PageDown) || Input.GetKey(KeyCode.KeypadPlus))
        {
            camZoom = camZoom + (10 * Time.deltaTime);
        }        

        //Reset Camera
        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Keypad5) || Input.GetButtonDown("joystick button 6"))
        {
            camZoom = 60f;
        }
    }
}
