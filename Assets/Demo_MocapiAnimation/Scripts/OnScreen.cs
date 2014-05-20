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
            GUI.Label(new Rect(10, 140, 200, 120), "Move: Stick Y", mainStyle);
            GUI.Label(new Rect(10, 160, 200, 120), "Sidestep: Alt + Stick X", mainStyle);
            GUI.Label(new Rect(10, 180, 200, 120), "Look Left/Right: Ctrl + button 4", mainStyle);
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
        public static void ErrorInfoOFF()
        {
            guiTextColor = new Color(0.94F, 0.6F, 0.2F, .92F);
            guiTitleColor = new Color(1F, 1F, 1F, .85F);

            GUIStyle infoStyle = new GUIStyle();
            infoStyle.fontSize = 9;
            infoStyle.font = GUI.skin.font;

            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiTitleColor;
            headerStyleCentered.fontSize = 15;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;
            headerStyleCentered.font = GUI.skin.font;

            GUIStyle mainStyleCentered = new GUIStyle();
            mainStyleCentered.normal.textColor = guiTextColor;
            mainStyleCentered.fontSize = 13;
            mainStyleCentered.alignment = TextAnchor.UpperCenter;
            mainStyleCentered.font = GUI.skin.font;

            //GUI.BeginGroup(new Rect((Screen.width - (Screen.width * .8f)) / 2, (Screen.height - (Screen.height * .5f)) / 2, Screen.width * .8f, Screen.height * .5f));
            GUI.Box(new Rect((Screen.width - (Screen.width * .8f)) / 2, (Screen.height - (Screen.height * .5f)) / 2, Screen.width * .8f, Screen.height * .5f), "");
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 200, 120), "Input Manager Error.", headerStyleCentered);
            GUI.Label(new Rect(10, 30, 200, 120), "Please replace \n ProjectSettings\\InputManager.asset \n with the one contained in \n Assets\\Demo_MocapiAnimation\\InputManager_Demo.zip", mainStyleCentered);

            //GUI.EndGroup();
        }

        public static void ErrorInfo()
        {
            //ui dimensions
            int uiWidth = 420; 
            int uiHeight = 180; 

            //style definitions
            Color guiTextColor = new Color(0.94F, 0.6F, 0.2F, .92F);
            Color guiTitleColor = new Color(1F, 1F, 1F, .85F);

            GUIStyle infoStyle = new GUIStyle();
            infoStyle.fontSize = 9;
            infoStyle.font = GUI.skin.font;

            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiTitleColor;
            headerStyleCentered.fontSize = 15;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;
            headerStyleCentered.font = GUI.skin.font;
            headerStyleCentered.wordWrap = true;

            GUIStyle mainStyleCentered = new GUIStyle();
            mainStyleCentered.normal.textColor = guiTextColor;
            mainStyleCentered.fontSize = 13;
            mainStyleCentered.alignment = TextAnchor.UpperCenter;
            mainStyleCentered.font = GUI.skin.font;

            // Group on the center of the screen
            GUI.BeginGroup(new Rect(Screen.width / 2 - uiWidth/2, Screen.height / 2 - uiHeight/2, uiWidth, uiHeight));

            // Box background.
            GUI.Box(new Rect(0, 0, uiWidth, uiHeight), "Input Manager Error.");

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();
        }

        public static Texture2D texture = new Texture2D(128, 128);

        public static void ErrorInfo3()
        {
            //style definitions
            Color guiTextColor = new Color(0.94F, 0.6F, 0.2F, .92F);
            Color guiTitleColor = new Color(1F, 1F, 1F, .85F);
            Color guiInfoBGColor = new Color(.4F, .4F, .4F, .6F);
            Color guiErrorBGColor = new Color(1F, .4F, .4F, .6F);

            GUIStyle headerStyleCentered = new GUIStyle();
            headerStyleCentered.normal.textColor = guiTitleColor;
            headerStyleCentered.fontSize = 15;
            headerStyleCentered.alignment = TextAnchor.UpperCenter;
            headerStyleCentered.font = GUI.skin.font;
            headerStyleCentered.wordWrap = true;

            //GUIStyle styleBG = new GUIStyle();
            GUIStyle styleBG = new GUIStyle(GUI.skin.box);
            styleBG.normal.background = MakeTex(2, 2, guiErrorBGColor);
            GUI.Box(new Rect(100, 100, 100, 100), "test", styleBG);
        }

        private static Texture2D MakeTex(int width, int height, Color col) //courtesy of Benderlab
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    
    }
}