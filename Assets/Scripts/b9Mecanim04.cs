using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class b9Mecanim04 : MonoBehaviour
{
    public float DampTime = 3f;                     // adjust motion lerping:  0 - infinity, 10 almost instant, default 3
    public static float animSpeed = 1f;             // global animation speed
    int CurrentIdleVariant = 1;
    int NextIdleVariant = 1;

    //Actual Animator speed and turn rates
    float animSpeedABS;             //absolute values of Animator
    float animRotABS;

    float strafe = 0f;              //keyb strafing speed
    float walk = 0f;                //keyb walk speed
    float run = 0f;                 //keyb running speed
    float turn = 0f;                //keyb turning speed
    float allAxis = 0f;             //sum of all joystick axis inputs

    bool button0A = false;
    bool button1B = false;
    bool button2X = false;
    bool button3Y = false;
    bool button4LB = false;
    bool button5RB = false;
    bool button6 = false;
    bool button7 = false;
    bool button8 = false;
    bool button9 = false;

    //States
    private Animator anim;
    private AnimatorStateInfo animState;
    bool canChangeState = true;

    static int idleState = Animator.StringToHash("Base Layer.Stand_Idle");
    static int run2stand = Animator.StringToHash("MOVE_AHEAD.Walk-2-Stand");

    static int idle01 = Animator.StringToHash("STAND_IDLES.Idle01");
    static int idle02 = Animator.StringToHash("STAND_IDLES.Idle02");
    static int idle03 = Animator.StringToHash("STAND_IDLES.Idle03");
    static int idle04 = Animator.StringToHash("STAND_IDLES.Idle04");
    static int idle05 = Animator.StringToHash("STAND_IDLES.Idle05");
    //static int idle06 = Animator.StringToHash("STAND_IDLES.Idle06");

    //===OLD===
    // private AnimatorStateInfo animState;			// a reference to the current state of the animator, used for base layer
    //float h = 0f;				// setup h variable as our horizontal input axis
    //float v = 0f;				// setup v variables as our vertical input axis
    //public bool Altkey = false;     //is alt key pessed
    ////animation state hashes
    //static int idleState = Animator.StringToHash("Base Layer.Stand_Idle");
    //static int idleSwitchFeetState = Animator.StringToHash("Base Layer.Stand_Idle (change feet)");
    //static int standAlertState = Animator.StringToHash("Base Layer.Alert");
    //static int sideStepState = Animator.StringToHash("SIDESTEP.SideStep");
    //static int walkRunState = Animator.StringToHash("MOVE_AHEAD.WALK-RUN");
    //static int walkRunBackState = Animator.StringToHash("WALK_BACK.WALK-RUN-BACK");
    //static int stand2walkState = Animator.StringToHash("WALK-RUN.Stand-2-Walk");
    //static int walkState = Animator.StringToHash("WALK-RUN.Walk");
    //static int standTurnState = Animator.StringToHash("TURN_ON_SPOT.TURN_ON_SPOT");

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = animSpeed;                         // Set the global speed of our animator
        animState = anim.GetCurrentAnimatorStateInfo(0);// Get our animator's current state

        ControllerInput();                              //  Apply inputs
        AvatarSpeed();                                  //  Read avatar current condition
        LogicStates();                                  //  when what can be done

        //GenericInput();
    }

    void AvatarSpeed()
    {
        //animSpeedABS = anim.deltaPosition.z * 100;                                //unclamped
        //animRotABS = anim.deltaRotation.y * 100;
        animSpeedABS = Mathf.Clamp01(Mathf.Abs(anim.deltaPosition.z * 100) / 4.5f);   //scaledValue = rawValue / max; //Scale a value with min=0
        animRotABS = Mathf.Clamp01(Mathf.Abs(anim.deltaRotation.y * 100) / 1.5f);     //scaledValue = (rawValue - min) / (max - min);  //Scale a value
        anim.SetFloat("absSpeed", animSpeedABS);    //input Speed absolute value
        anim.SetFloat("absRotation", animRotABS);   //input Direcion absolute value
    }

    void ControllerInput()
    {
        //Keybd Walk
        if (Input.GetKey(KeyCode.UpArrow))
            walk = Mathf.Lerp(walk, .5f, DampTime * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            walk = Mathf.Lerp(walk, -.5f, DampTime * Time.deltaTime);
        else if (!Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.DownArrow))
        {
            walk = Mathf.Lerp(walk, 0f, (DampTime / 2) * Time.deltaTime);
            run = Mathf.Lerp(run, 0f, (DampTime / 2) * Time.deltaTime);
        }

        //Keyb Run
        if (Mathf.Abs(walk) > .05)
        {
            if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Sign(walk) == 1))
                run = Mathf.Lerp(run, .5f, DampTime * Time.deltaTime);
            else if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Sign(walk) == -1))
                run = Mathf.Lerp(run, -.5f, DampTime * Time.deltaTime);
            else if (!Input.GetKey(KeyCode.LeftShift))
                run = Mathf.Lerp(run, 0f, (DampTime / 2) * Time.deltaTime);

        }

        //Keybd Alt-Strafe
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftArrow))
        {
            strafe = Mathf.Lerp(strafe, -1f, DampTime * Time.deltaTime);
            //turn = Input.GetAxis("LHorizontal") * -1;
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.RightArrow))
        {
            strafe = Mathf.Lerp(strafe, 1f, DampTime * Time.deltaTime);
            //turn = Input.GetAxis("LHorizontal") * -1;
        }
        else if (!Input.GetKey(KeyCode.LeftAlt) || (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
            strafe = Mathf.Lerp(strafe, 0f, DampTime * Time.deltaTime);

        //Keybd Turn
        if (Input.GetKey(KeyCode.LeftArrow))
            turn = Mathf.Lerp(turn, -1f, DampTime * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            turn = Mathf.Lerp(turn, 1f, DampTime * Time.deltaTime);
        else if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
            turn = Mathf.Lerp(turn, 0f, DampTime * Time.deltaTime);

        // Stick Controls
        anim.SetFloat("ForeBack", Input.GetAxis("LVertical") + walk + run);					// set our animator's Speed to the Left Stick vertical input axis				
        anim.SetFloat("LeftRight", Input.GetAxis("LHorizontal") + turn); 						// set our animator's Direction to the Left Stick horizontal input axis	
        //anim.SetFloat("_____", Input.GetAxis("RVertical"));           // Right Stick Vertical

        //Controller Buttons
        if (Input.GetButtonDown("joystick button 1(B)") || Input.GetKey(KeyCode.LeftAlt))
            button1B=true;
        else if (!Input.GetButton("joystick button 1(B)") && !Input.GetKey(KeyCode.LeftAlt))
            button1B = false;
        if (Input.GetButtonDown("joystick button 2(X)") || Input.GetKey(KeyCode.L))
            button2X = true;
        else if (!Input.GetButton("joystick button 2(X)") && !Input.GetKey(KeyCode.L))
            button2X = false;

        if (Input.GetButtonDown("joystick button 4(LB)") || Input.GetKey(KeyCode.Q))
            button4LB = true;
        else if (!Input.GetButton("joystick button 4(LB)") && !Input.GetKey(KeyCode.Q))
            button4LB = false;
        //if (Input.GetButtonDown("joystick button 5(RB)"))
        //    button5RB = true;
        //else if (Input.GetButtonUp("joystick button 5(RB)"))
        //    button5RB = false;

        //anim.SetBool("trigger1L", true);
        //anim.SetBool("trigger2R", true);
        anim.SetBool("triggerLBAlert", button4LB);
        //anim.SetBool("triggerRB", button5RB);

        anim.SetBool("buttonBstrafe", button1B);
        anim.SetBool("buttonXLook", button2X);
        //anim.SetBool("xboxYjump", true);
        //anim.SetBool("xboxAcrouch", true);


        //Composite Input (joystick + buttons)
        //allAxis = Mathf.Abs(Input.GetAxis("LVertical") + Input.GetAxis("LHorizontal") + Input.GetAxis("RVertical") + Input.GetAxis("RHorizontal") + Input.GetAxis("HatHorizontal") + Input.GetAxis("HatVertical"));
        allAxis = Mathf.Abs(Input.GetAxis("LVertical") + Input.GetAxis("LHorizontal") + Input.GetAxis("RVertical") + Input.GetAxis("RHorizontal"));
        //ArrayList allButtons = new ArrayList() { Input.GetButton("joystick button 0(A)"), Input.GetButton("joystick button 1(B)"), Input.GetButton("joystick button 2(X)"), Input.GetButton("joystick button 3(Y)"), Input.GetButton("joystick button 4"), Input.GetButton("joystick button 5"), Input.GetButton("joystick button 6"), Input.GetButton("joystick button 7"), Input.GetButton("joystick button 8"), Input.GetButton("joystick button 9") };                
        ArrayList allButtons = new ArrayList() { button0A, button1B, button2X, button3Y, button4LB, button5RB, button6, button7, button8, button9 };

        if (Input.anyKey || allButtons.Contains(true))                  //any keyb or button
        {
            anim.SetBool("AnyButton", true);
            //print("anykey");
        }
        else
            anim.SetBool("AnyButton", false);

        if (allAxis > .2 || Input.anyKey || allButtons.Contains(true))  //any keyb, button or stick
            anim.SetBool("AnyInput", true);
        else
            anim.SetBool("AnyInput", false);
    }

    void LogicStates()
    {

        if (animState.nameHash == idleState)    //Default Idle state
            IdleVariants();

        if (animState.nameHash == idle01 || animState.nameHash == idle02 || animState.nameHash == idle03 || animState.nameHash == idle04 || animState.nameHash == idle05 )   //Switching between Idle Variants
            IdleVariants();
    }

    void IdleVariants()
    {
        {
            int[] IdleAnims = new int[5] { idle01, idle02, idle03, idle04, idle05 };                //List of available variant anims

            int animLoopNum = (int)animState.normalizedTime;
            float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2

//            print("idlevar");
//            NextIdleVariant = Mathf.RoundToInt(UnityEngine.Random.Range(1, IdleAnims.Length));  //random select next transition
//            anim.CrossFade(IdleAnims[NextIdleVariant], .1f, -1, 0f);    //Crossfade to
//
            if (animPercent > .85f && canChangeState == true)       //crossfade after this percent
            {
                canChangeState = false;                             //stop state change until next crossfade
                CurrentIdleVariant = NextIdleVariant;               //start selection of next random clip
                while (CurrentIdleVariant == NextIdleVariant)
                    NextIdleVariant = Mathf.RoundToInt(UnityEngine.Random.Range(1, IdleAnims.Length));  //random select next transition
                anim.CrossFade(IdleAnims[NextIdleVariant], .1f, -1, 0f);    //Crossfade to
            }
            else if (animPercent < .3f && canChangeState == false)  //arm for a new crossfade
            {
                canChangeState = true;
            }
        }
    }



        //=======OLD================

        //void GenericInput()
        //{
        //    h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
        //    v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
        //    anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
        //    anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis	


        //    //anim.SetFloat("Speed", Input.GetAxis("Vertical"));							// set our animator's float parameter 'Speed' equal to the vertical input axis				
        //    //anim.SetFloat("Direction", Input.GetAxis("Horizontal"), DampTime, Time.deltaTime); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
        //    animState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        //    if (Input.GetKey(KeyCode.LeftAlt))		//While LeftShift pressed
        //    {
        //        //print("alt");
        //        Altkey = true;
        //        anim.SetBool("Alt", true);
        //    }
        //    else
        //    {
        //        Altkey = false;
        //        anim.SetBool("Alt", false);
        //    }
        //}

        //void LogicStates() {
        //    if (animState.nameHash == idleState)
        //    {
        //        // to Turn on place
        //        if (Altkey == false && h != 0f) //(!Input.anyKeyDown)
        //        {
        //            anim.CrossFade(standTurnState, 0f, -1, 0f);
        //            //print("turn");
        //        }
        //        //Idle variations
        //        else if (Input.GetKey(KeyCode.I)) //(!Input.anyKeyDown)
        //        {
        //            anim.CrossFade(idleSwitchFeetState, .3f, -1, 0f);
        //        }

        //        // to Alert
        //        else if (Input.GetKey(KeyCode.L)) //
        //        {
        //            anim.CrossFade(standAlertState, .3f, -1, 0f);
        //        }
        //        // to Sidestep -- implemented in Animator

        //        // to Look Left, Right, Over Shoulder
        //        // to Walk
        //    }

        //    if (animState.nameHash == standTurnState)
        //    {
        //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
        //        {
        //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
        //        }
        //        else
        //        {
        //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
        //        }

        //        if (h == 0f)
        //        {
        //            anim.CrossFade(idleState, 0f, -1, 0f);
        //        }

        //    }

        //    if (animState.nameHash == stand2walkState)
        //    {
        //        if (Input.GetKey(KeyCode.DownArrow))   //(Input.GetAxis("Vertical") == 0 && Input.GetKey(KeyCode.DownArrow))
        //        {
        //            anim.CrossFade(standTurnState, .2f, -1, .1f);
        //        }
        //        //Debug.Log("trans");

        //    }

        //    if (animState.nameHash == walkRunState)
        //    {
        //        //print("walkrun state");
        //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
        //        {
        //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
        //        }
        //        else
        //        {
        //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
        //        }

        //        //Debug.Log("walkRun");

        //    }

        //    if (animState.nameHash == walkRunBackState)
        //    {
        //        //print("walkrunBack state");
        //        if (Input.GetKey(KeyCode.LeftShift))		//While LeftShift pressed
        //        {
        //            anim.SetFloat("Shift", 1f, DampTime, Time.deltaTime);
        //        }
        //        else
        //        {
        //            anim.SetFloat("Shift", 0f, DampTime, Time.deltaTime);
        //        }


        //    }
        //}

        //=======UNUSED================

        //IEnumerator WaitSec()
        //{
        //    float waitTime = Random.Range(0.0f, 100.0f);
        //    yield return new WaitForSeconds(waitTime);

        //    //yield return new WaitForSeconds(5);
        //    //print(Time.time);
        //}

        //void OnGUI()
        //{
        //    Event e = Event.current;
        //    if (e.alt)      //Strafing in Animator
        //    {
        //        if (Application.platform == RuntimePlatform.OSXEditor)
        //        {
        //            Altkey = true;
        //            anim.SetBool("Alt", true);
        //        }
        //        else
        //            if (Application.platform == RuntimePlatform.WindowsEditor)
        //            {
        //                Altkey = true;
        //                anim.SetBool("Alt", true);
        //            }
        //    }
        //    else
        //    {
        //        Altkey = false;
        //        anim.SetBool("Alt", false);
        //    }
        //}

    }

