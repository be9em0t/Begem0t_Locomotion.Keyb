using UnityEngine;
using System.Collections;

namespace Mocapianimation
{
    public class OnScreen : MonoBehaviour
    {

        //public float hSliderValue = 0.0F;

        Color guiTextColor;
        Color guiTitleColor;
        bool showInfo = true;

        void Start()
        {
            // hSliderValue = MocapiMecanim.animSpeed; // ToDo after A.
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                showInfo = !showInfo;
            }

        }

        void OnGUI()
        {


            if (showInfo == true)
            {
                PanelInfo();
            }

        }

        void PanelInfo()
        {
            guiTextColor = new Color(0.94F, 0.6F, 0.2F, .92F);
            guiTitleColor = new Color(1F, 1F, 1F, .85F);

            // Make a background box
            GUIStyle infoStyle = new GUIStyle();
            infoStyle.fontSize = 9;
            infoStyle.font = GUI.skin.font;

            GUIStyle smallStyle = new GUIStyle();
            smallStyle.normal.textColor = guiTitleColor;
            smallStyle.fontSize = 13;
            //smallStyle.fontStyle = FontStyle.Bold;
            smallStyle.font = GUI.skin.font;

            GUIStyle mainStyle = new GUIStyle();
            mainStyle.normal.textColor = guiTextColor;
            //mainStyle.fontStyle = FontStyle.Bold;
            mainStyle.fontSize = 13;
            mainStyle.font = GUI.skin.font;


            GUI.Box(new Rect(5, 10, 230, 400), MocapiCameraSwitcher.camActive.name + " (C to switch)");

            //GUI.Label(new Rect(10, 20, 200, 120), MocapiCameraSwitcher.camActive.name, smallStyle);
            GUI.Label(new Rect(10, 40, 200, 120), "Toggle this panel: I", mainStyle);
            GUI.Label(new Rect(10, 60, 200, 120), "Camera Switch C, Gamepad Button 2", mainStyle);
            GUI.Label(new Rect(10, 80, 200, 120), "Camera Zoom : PgUp PgDn, +/-", mainStyle);
            GUI.Label(new Rect(10, 100, 200, 120), "Reset Camera: Home, Gamepad 6", mainStyle);

            GUI.Label(new Rect(10, 140, 200, 120), "GAMEPAD", smallStyle);
            GUI.Label(new Rect(10, 160, 200, 120), "Move: Left Stick Y", mainStyle);
            GUI.Label(new Rect(10, 180, 200, 120), "Sidestep: LStick X", mainStyle);
            GUI.Label(new Rect(10, 200, 200, 120), "Look L/R: RStick + button 4", mainStyle);
            GUI.Label(new Rect(10, 220, 200, 120), "Alert: button 3", mainStyle);
            GUI.Label(new Rect(10, 240, 200, 120), "Sit Down: button 0", mainStyle);

            GUI.Label(new Rect(10, 280, 200, 120), "KEYBOARD", smallStyle);
            GUI.Label(new Rect(10, 300, 200, 120), "Move avatar: Arrows, AWSD", mainStyle);
            GUI.Label(new Rect(10, 320, 200, 120), "__SpeedUp: LeftShift+Arrows", mainStyle);
            GUI.Label(new Rect(10, 340, 200, 120), "SideStep: Alt+Arrows", mainStyle);
            GUI.Label(new Rect(10, 360, 200, 120), "Look L/R: Arrows + Z", mainStyle);
            GUI.Label(new Rect(10, 380, 200, 120), "Alert: X", mainStyle);


            ////GUI.Label(new Rect(10, 360, 200, 120), "Alert : Left Bumper", mainStyle);
            //if (GUI.Button(new Rect(Screen.width - 110, 30, 30, 28), ".5x"))
            //    hSliderValue = .5f;
            //if (GUI.Button(new Rect(Screen.width - 75, 30, 30, 28), "1x"))
            //    hSliderValue = 1f;
            //if (GUI.Button(new Rect(Screen.width - 40, 30, 30, 28), "2x"))
            //    hSliderValue = 2f;

            //hSliderValue = GUI.HorizontalSlider(new Rect(Screen.width - 110, 10, 100, 30), hSliderValue, 0.0F, 5.0F);  //anim speed slider
            //hSliderValue = Mathf.Round((hSliderValue * 10f)) / 10f;     //round to DP1
            //// MocapiMecanim.animSpeed = hSliderValue; //ToDo after A.
            //GUI.Label(new Rect(Screen.width - 110, 70, 100, 30), "Anim Speed:" + hSliderValue.ToString(), mainStyle);

            //		GUI.Label(new Rect(10,130, 160,120), "Z/X: Zoom camera");
            //		GUI.Label(new Rect(10,150, 160,120), "R  : Reset avatar");

        }
    }
}