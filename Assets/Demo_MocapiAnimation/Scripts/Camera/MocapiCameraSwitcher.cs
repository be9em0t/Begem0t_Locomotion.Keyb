using UnityEngine;
using System.Collections;

namespace Mocapianimation
{
    public class MocapiCameraSwitcher : MonoBehaviour
    {
        public static Camera camActive;

        // Buttons
        string joyCameraButton = "joystick button 2";

        // Use this for initialization
        void Start()
        {
            Camera[] allCams = FindObjectsOfType(typeof(Camera)) as Camera[];

            // Set initial camera
            foreach (Camera cam in allCams)
            {
                cam.enabled = false;
                //Debug.Log(cam.name);
            }
            allCams[1].enabled = true;
            camActive = allCams[1];

        }

        // Update is called once per frame
        void Update()
        {

            //Process Joystick button
            if (Input.GetButtonDown(joyCameraButton))
            {
                CamSwitch();
            }

        }

        void CamSwitch()
        {
            Camera[] allCams = FindObjectsOfType(typeof(Camera)) as Camera[];

            for (int i = 0; i < allCams.Length; i++)
            {
                if (allCams[i].enabled == true)
                {
                    allCams[i].enabled = false;
                    if (i == allCams.Length - 1)
                    {
                        allCams[0].enabled = true;
                        camActive = allCams[0];
                        //Debug.Log("Switched to camera " + allCams[0].name);
                    }
                    else
                    {
                        allCams[i + 1].enabled = true;
                        camActive = allCams[i + 1];
                        //Debug.Log("Switched to camera " + allCams[i + 1].name);
                    }
                    break;
                }
            }
        }

    }
}
