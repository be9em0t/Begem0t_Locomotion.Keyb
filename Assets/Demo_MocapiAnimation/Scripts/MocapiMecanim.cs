using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MocapiMecanim : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo animState;
    bool canChangeState = true;

    public float DampTime = 3f;                     // adjust motion lerping:  0 - infinity, 10 almost instant, default 3
    public static float animSpeed = 1f;             // global animation speed
    int CurrentIdleVariant = 1;
    int NextIdleVariant = 1;

    float moveForeBack;            //keyb walk speed
    float moveLeftRight;           //keyb strafing speed

    float lookLeftRight;           //keyb running speed
    float lookUpDown;              //keyb turning speed

    float allAxis = 0f;                 //sum of all joystick axis inputs

    bool button0A = false;
    bool button1B = false;
    bool button2X = false;
    bool button3Y = false;
    bool button4LT = false;
    bool button5RT = false;
    bool button6 = false;
    bool button7 = false;
    bool button8 = false;
    bool button9 = false;

    //static int idleState = Animator.StringToHash("Base Layer.Stand_Idle");
    //static int idleState = Animator.StringToHash("STAND_IDLES.Idle01");
    //static int run2stand = Animator.StringToHash("MOVE_AHEAD.Walk-2-Stand");

    static int idle00 = Animator.StringToHash("STAND_IDLES.Idle00"); //default idle
    static int idle01 = Animator.StringToHash("STAND_IDLES.Idle01");
    static int idle02 = Animator.StringToHash("STAND_IDLES.Idle02");
    static int idle03 = Animator.StringToHash("STAND_IDLES.Idle03");
    static int idle04 = Animator.StringToHash("STAND_IDLES.Idle04");
    static int idle05 = Animator.StringToHash("STAND_IDLES.Idle05");

    void Start()
    {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Debug.Log("quitme!");
            Application.Quit();
        }

            anim.speed = animSpeed;                         // Set the global speed of our animator
            animState = anim.GetCurrentAnimatorStateInfo(0);// Get our animator's current state

            ControllerInput();                              //  Apply inputs
            AnyInput();

            //AvatarMove(); //AvatarSpeed();                                  //  Read avatar current condition
            LogicStates();                                  //  when what can be done

	}

    void AvatarMove()
    {
        //animSpeedABS = Mathf.Clamp01(Mathf.Abs(anim.deltaPosition.z * 100) / 4.5f);   //scaledValue = rawValue / max; //Scale a value with min=0
        //animRotABS = Mathf.Clamp01(Mathf.Abs(anim.deltaRotation.y * 100) / 1.5f);     //scaledValue = (rawValue - min) / (max - min);  //Scale a value
        //anim.SetFloat("absSpeed", animSpeedABS);    //input Speed absolute value
        //anim.SetFloat("absRotation", animRotABS);   //input Direcion absolute value
    }

    void ControllerInput()
    {
        // Stick Controls
        anim.SetFloat("moveForeBack", Input.GetAxis("LVertical")); // + walk + run);					// set our animator's Speed to the Left Stick vertical input axis				
        //OLD anim.SetFloat("LeftRight", Input.GetAxis("LHorizontal")); // + turn); 						// set our animator's Direction to the Left Stick horizontal input axis
        anim.SetFloat("turnLeftRight", Input.GetAxis("RHorizontal")); // + turn); 						// set our animator's Direction to the Left Stick horizontal input axis
        anim.SetFloat("strafeLeftRigh", Input.GetAxis("LHorizontal")); // + turn); 						// set our animator's Direction to the Left Stick horizontal input axis

        //Controller Buttons
        if (Input.GetButtonDown("joystick button 1(B)") || Input.GetKey(KeyCode.LeftAlt))
            button1B = true;
        else if (!Input.GetButton("joystick button 1(B)") && !Input.GetKey(KeyCode.LeftAlt))
            button1B = false;
        if (Input.GetButtonDown("joystick button 2(X)") || Input.GetKey(KeyCode.L))
            button2X = true;
        else if (!Input.GetButton("joystick button 2(X)") && !Input.GetKey(KeyCode.L))
            button2X = false;

        if (Input.GetButtonDown("joystick button 4(LT)") || Input.GetKey(KeyCode.Q))
            button4LT = true;
        else if (!Input.GetButton("joystick button 4(LT)") && !Input.GetKey(KeyCode.Q))
            button4LT = false;
        if (Input.GetButtonDown("joystick button 5(RT)") || Input.GetKey(KeyCode.L))
            button5RT = true;
        else if (!Input.GetButton("joystick button 5(RT)") && !Input.GetKey(KeyCode.L))
            button5RT = false;


        //anim.SetBool("trigger1L", true);
        //anim.SetBool("trigger2R", true);
        anim.SetBool("buttonLTAlert", button4LT);
        anim.SetBool("buttonRTLook", button5RT);

        anim.SetBool("buttonBstrafe", button1B);
        //anim.SetBool("xboxYjump", true);
        //anim.SetBool("xboxAcrouch", true);
    }

    void AnyInput() 
    { 
        //Composite Input (joystick + buttons)
        allAxis = Mathf.Abs(Input.GetAxis("LVertical") + Input.GetAxis("LHorizontal") + Input.GetAxis("RVertical") + Input.GetAxis("RHorizontal"));
        ArrayList allButtons = new ArrayList() { button0A, button1B, button2X, button3Y, button4LT, button5RT, button6, button7, button8, button9 };

        if (Input.anyKey || allButtons.Contains(true))                  //any keyb or button
            anim.SetBool("AnyButton", true);
        else
            anim.SetBool("AnyButton", false);

        if (allAxis > .2 || Input.anyKey || allButtons.Contains(true))  //any keyb, button or stick
            anim.SetBool("AnyInput", true);
        else
            anim.SetBool("AnyInput", false);
    }

    void LogicStates()
    {

        //if (animState.nameHash == idleState)    //Default Idle state
        //    IdleVariants();

        if (animState.nameHash == idle00 || animState.nameHash == idle01 || animState.nameHash == idle02 || animState.nameHash == idle03 || animState.nameHash == idle04 || animState.nameHash == idle05 )   //Switching between Idle Variants
            IdleVariants(); 
    }

    void IdleVariants()
    {
        {
            int[] IdleAnims = new int[6] { idle00, idle01, idle02, idle03, idle04, idle05};                //List of available variant anims

            int animLoopNum = (int)animState.normalizedTime;
            float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2

            if (animPercent > .85f && canChangeState == true)       //crossfade after this percent
            {
                while (CurrentIdleVariant == NextIdleVariant)
                NextIdleVariant = UnityEngine.Random.Range(0, IdleAnims.Length);  //random select next transition
                canChangeState = false;                             //stop state change until next crossfade
                CurrentIdleVariant = NextIdleVariant;               //start selection of next random clip
                anim.CrossFade(IdleAnims[NextIdleVariant], .3f, -1, float.NegativeInfinity);    //Crossfade to
            }
            else if (animPercent < .3f && canChangeState == false)  //arm for a new crossfade
            {
                canChangeState = true;
            }
        }
    }


}
