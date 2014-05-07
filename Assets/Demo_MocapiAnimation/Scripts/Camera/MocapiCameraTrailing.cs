using UnityEngine;
using System.Collections;

namespace Mocapianimation
{
    public class MocapiCameraTrailing : MonoBehaviour
    {
        public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
        Transform standardPos;			// the usual position for the camera, specified by a transform in the game

        GameObject CamPosBehind;
        GameObject CamPosFront;
        GameObject CamPosLeft;
        GameObject CamPosRight;

        public float camZoom = 60f;         //camera FieldOfView

        void Start()
        {

            CamPosBehind = GameObject.Find("CameraPosition_Behind");
            CamPosFront = GameObject.Find("CameraPosition_Front");
            CamPosLeft = GameObject.Find("CameraPosition_Left");
            CamPosRight = GameObject.Find("CameraPosition_Right");

            standardPos = CamPosBehind.transform;

        }

        void FixedUpdate()
        {

            // set the camera to standard position and direction
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.transform.forward, Time.deltaTime * smooth);

            PositionChange();

        }

        //Camera Position Control
        void PositionChange()
        {

            //Rotate Camera Around
            if (Input.GetKey(KeyCode.Keypad8) || (Input.GetAxis("7th axis") < -0.1f)) //from behind
            {
                standardPos = CamPosBehind.transform;
            }
            else if (Input.GetKey(KeyCode.Keypad2) || (Input.GetAxis("7th axis") > 0.1f)) //from front
            {
                standardPos = CamPosFront.transform;
            }
            else if (Input.GetKey(KeyCode.Keypad4) || (Input.GetAxis("6th axis") > 0.1f)) //from left
            {
                standardPos = CamPosLeft.transform;
            }
            else if (Input.GetKey(KeyCode.Keypad6) || (Input.GetAxis("6th axis") < -0.1f)) //from right
            {
                standardPos = CamPosRight.transform;
            }


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
                standardPos = CamPosBehind.transform;
                camZoom = 60f;
            }
        }
    }
}
