using UnityEngine;
using System.Collections;
using System;

namespace Mocapianimation
{

    public class InputSettings : MonoBehaviour
    {

        public static string assses;
        public static bool showError;
        string axisName = "Xx axis";
        bool IsAxisAvailable(string axisName)
        {
            try
            {
                Input.GetAxis(axisName);
                return true;
            }
            catch 
            {
                return false;
            }
        }


    void Awake()
    {
        //if (IsAxisAvailable(axisName) == false)
        //{
        //    Mocapianimation.OnScreen.showInfo = false;
        //    showError = true;
        //    Application.Quit();
        //}

        //showError = true;
        //Debug.Log(showError);
        //Debug.Log(Input.GetAxis("X axis").ToString());
        //Debug.Log(Input.GetAxis("XXX axis").ToString());
        assses = IsAxisAvailable(axisName).ToString();
        Debug.Log(assses);

        //Debug.Log(IsAxisAvailable(axisName));
        //Mocapianimation.OnScreen.showInfo = false;

        
        
        //Switch between cameras
        Mocapianimation.MocapiCameraSwitcher.joyCameraButton = "joystick button 7";
        //try
        //{
        //    Input.GetButton(Mocapianimation.MocapiCameraSwitcher.joyCameraButton); //
        //}
        //catch (UnityException e)
        //{
        //    Debug.Log("Input Error: " + "\n" + e.ToString());
        //    Mocapianimation.OnScreen.showInfo = false;
        //    showError = true;
        //    Application.Quit();
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
        //GUI.Label(new Rect(155, 12, 200, 120), MocapiCameraSwitcher.camActive.name, headerStyleCentered);
    }
	// Update is called once per frame
	void Update () {
	}

}
}