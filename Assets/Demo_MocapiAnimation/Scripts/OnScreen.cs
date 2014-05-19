using UnityEngine;
using System.Collections;

namespace Mocapianimation
{
    public class OnScreen : MonoBehaviour
    {

        public static Color guiTextColor;
        public static Color guiTitleColor;

        void Update()
        {



        }

        void OnGUI()
        {


        }

        public static void PanelInfo()
        {
            guiTextColor = new Color(0.94F, 0.6F, 0.2F, .92F);
            guiTitleColor = new Color(1F, 1F, 1F, .85F);

            GUIStyle infoStyle = new GUIStyle();
            infoStyle.fontSize = 9;
            infoStyle.font = GUI.skin.font;

            GUIStyle headerStyle = new GUIStyle();
            headerStyle.normal.textColor = guiTitleColor;
            headerStyle.fontSize = 13;
            headerStyle.font = GUI.skin.font;

            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiTitleColor;
            headerStyleCentered.fontSize = 13;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;
            headerStyleCentered.font = GUI.skin.font;

            GUIStyle mainStyle = new GUIStyle();
            mainStyle.normal.textColor = guiTextColor;
            mainStyle.fontSize = 13;
            mainStyle.font = GUI.skin.font;

            GUIStyle mainStyleCentered = new GUIStyle();
            mainStyleCentered.normal.textColor = guiTextColor;
            mainStyleCentered.fontSize = 13;
            mainStyleCentered.alignment = TextAnchor.UpperCenter;
            mainStyleCentered.font = GUI.skin.font;

            // Make a background box
            GUI.Box(new Rect(5, 5, 240, 400), Mocapianimation.InputSettings.assses);
            GUI.Label(new Rect(10, 12, 200, 120), MocapiCameraSwitcher.camActive.name, headerStyleCentered);
            GUI.Label(new Rect(10, 30, 200, 120), "(C to change, H to hide)", mainStyleCentered);

            GUI.Label(new Rect(10, 60, 200, 120), "Zoom : PgUp PgDn, +/-", mainStyle);
            GUI.Label(new Rect(10, 80, 200, 120), "Reset Camera: Home, Gamepad 6", mainStyle);

            GUI.Label(new Rect(10, 120, 200, 120), "Gamepad", headerStyle);
            GUI.Label(new Rect(10, 140, 200, 120), "Move: Left Stick Y", mainStyle);
            GUI.Label(new Rect(10, 160, 200, 120), "Sidestep: LStick X", mainStyle);
            GUI.Label(new Rect(10, 180, 200, 120), "Look Left/Right: RStick + button 4", mainStyle);
            GUI.Label(new Rect(10, 200, 200, 120), "Alert: button 3", mainStyle);
            GUI.Label(new Rect(10, 220, 200, 120), "Sit Down: button 0", mainStyle);

            GUI.Label(new Rect(10, 260, 200, 120), "Keyboard", headerStyle);
            GUI.Label(new Rect(10, 280, 200, 120), "Move: Arrows, AWSD", mainStyle);
            GUI.Label(new Rect(10, 300, 200, 120), "Run: LShift", mainStyle);
            GUI.Label(new Rect(10, 320, 200, 120), "SideStep: Q or E", mainStyle);
            GUI.Label(new Rect(10, 340, 200, 120), "Look L/R: LAlt + Arrows", mainStyle);
            GUI.Label(new Rect(10, 360, 200, 120), "Alert: Z", mainStyle);
            GUI.Label(new Rect(10, 380, 200, 120), "Sit Down: X", mainStyle);

        }

        //Format Error Info panel
        public static void ErrorInfo()
        {
            GUI.Box(new Rect((Screen.width - (Screen.width * .8f)) / 2, (Screen.height - (Screen.height * .5f)) / 2, Screen.width * .8f, Screen.height * .5f), "Input Manager Error. \n\n Please replace \n ProjectSettings\\InputManager.asset \n with the one contained in \n Assets\\Demo_MocapiAnimation\\InputManager_Demo.zip");
        }

    }
}