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
        /// Names of Avatar keyboard axis and buttons
        /// </summary>
        public static string keyMoveAxis = "keyboard Y";
        public static string keyTurnAxis = "keyboard X";
        //public static string keyStrafeAxis = "keyboard X2";
        public static KeyCode keyRunButton = KeyCode.LeftShift;
        public static KeyCode keyStrafeButton = KeyCode.LeftAlt;

        /// <summary>
        /// Names of Avatar joystick axis and buttons
        /// </summary>
        public static string joyMoveAxis = "joystick Y"; //inverted
        public static string joyTurnAxis = "joystick X"; //non-inverted
        //public static string joyStrafeAxis = "joystick X2";  //non-inverted
        public static string joySitButton = "joystick button 0";
        public static string joyLookButton = "joystick button 1";
        public static string joyStrafeButton = "joystick button 2";
        public static string joyAlertButton = "joystick button 3";

        /// <summary>
        /// Names of Avatar mouse axis
        /// </summary>
        public static string mouseMoveAxis = "Mouse Y";
        public static string mouseTurnAxis = "Mouse X";
        //public static string mouseStrafeAxis = "Mouse X";  //probably not axis but (mouse) key + mouseTurnAxis

        /// <summary>
        /// Names of Camera control axis and buttons
        /// </summary>
        public static string joyCamSwitchButton = "joystick button 7";
        public static string joyCamResetButton = "joystick button 6";
        /// Names of Camera View joystick axis
        public static string joyDPadX = "d-pad X";
        public static string joyDPadY = "d-pad Y";
        public static string joyHatX = "hat X";
        public static string joyHatY = "hat Y";
 
        //bool IsAxisAvailable(string axisName)
        //{
        //    try
        //    {
        //        Input.GetAxis(joyStrafeAxis);
        //        return true;
        //    }
        //    catch 
        //    {
        //        return false;
        //    }
        //}


    void Awake()
    {
        //if (IsAxisAvailable(joyStrafeAxis) == false)
        //{
        //    Mocapianimation.OnScreen.showInfo = false;
        //    showError = true;
        //}

        
        
        //Switch between cameras
        Mocapianimation.MocapiCameraSwitcher.joyCamSwitchButton = joyCamResetButton;
        //try
        //{
        //    Input.GetButton(Mocapianimation.MocapiCameraSwitcher.joyCamSwitchButton); //
        //}
        //catch (UnityException e)
        //{
        //    throw new System.InvalidOperationException("what a crap: " + e);
        //    //Debug.Log("Input Error: " + "\n" + e.ToString());
        //    //Mocapianimation.OnScreen.showInfo = false;
        //    //showError = true;
        //    //Application.Quit();
        //}
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