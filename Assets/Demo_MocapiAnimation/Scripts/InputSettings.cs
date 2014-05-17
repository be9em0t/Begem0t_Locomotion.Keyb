using UnityEngine;
using System.Collections;
using System;

namespace Mocapianimation
{

    public class InputSettings : MonoBehaviour
    {

        public static string assses;
        public static bool showError;

        /// <summary>
        /// name of the joystic axis 
        /// </summary>
        public static string joyMoveAxis = "Y axis"; //inverted
        public static string joyTurnAxis = "4th axis"; //non-inverted
        public static string joyStrafeAxis = "X axis";  //non-inverted

        /// <summary>
        /// name of the joystic axis 
        /// </summary>
        public static string joyCameraButton = "joystick button 7"; 
 
        bool IsAxisAvailable(string axisName)
        {
            try
            {
                Input.GetAxis(joyStrafeAxis);
                return true;
            }
            catch 
            {
                return false;
            }
        }


    void Awake()
    {
        if (IsAxisAvailable(joyStrafeAxis) == false)
        {
            Mocapianimation.OnScreen.showInfo = false;
            showError = true;
        }

        
        
        //Switch between cameras
        Mocapianimation.MocapiCameraSwitcher.joyCameraButton = joyCameraButton;
        try
        {
            Input.GetButton(Mocapianimation.MocapiCameraSwitcher.joyCameraButton); //
        }
        catch (UnityException e)
        {
            throw new System.InvalidOperationException("what a crap: " + e);
            //Debug.Log("Input Error: " + "\n" + e.ToString());
            //Mocapianimation.OnScreen.showInfo = false;
            //showError = true;
            //Application.Quit();
        }
    }

	// Use this for initialization
	void Start () {


}


    void OnGUI()
    {

        // Toggle Info panel
        if (showError == true)
        {
            ErrorInfo();
        }

    }

    void ErrorInfo()
    {
        GUI.Box(new Rect((Screen.width - (Screen.width * .8f)) / 2, (Screen.height - (Screen.height * .5f)) / 2, Screen.width * .8f, Screen.height * .5f), "Error");
    }

        // Update is called once per frame
	void Update () {
	}

}
}