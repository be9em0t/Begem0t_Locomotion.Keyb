using UnityEngine;
using System.Collections;


namespace Mocapianimation
{ 

	/// <summary>
    /// Controller for a character.Direction
	/// This class will process user inputs and translate it to animation.
	/// The character will be animated based on predefined animations
	/// by a Mecanim animator.
	/// </summary> 
	public class CharacterControl : MonoBehaviour 
	{

        /// <summary>
        /// Multiple Idle states
        /// list of available idle animations' vectors in 2D BlendTree (you need a corresponding 2D blendtree in Animator Controller)
        /// </summary>       
        Vector2 idle1 = new Vector2(0f, 0f);
        Vector2 idle2 = new Vector2(0f, 1f);
        Vector2 idle3 = new Vector2(-1f, 0.2f);
        Vector2 idle4 = new Vector2(1f, 0.2f);
        Vector2 idle5 = new Vector2(-1f, -1f);
        Vector2 idle6 = new Vector2(1f, -1f);
        /// <summary>
        /// array holding Idle Animations' vectors in 2D BlendTree
        /// </summary>
        Vector2[] idleAnimsList; 
        /// <summary>
        /// 2D blend tree, containing all idle variants
        /// </summary>
        static int idleBlendTree = Animator.StringToHash("Base Layer.STAND_IDLE"); 

        /// <summary>
        /// flag to indicate if any valid input was given
        /// or the animation needs to stay in idle
        /// </summary>
        private bool idle;
        /// <summary>
        /// flag to indicate if idle state can change
        /// </summary>
        private bool canChangeState = true;

        /// <summary>
        /// current idle state
        /// </summary>
        private Vector2 currentIdleVariant; 
        /// <summary>
        /// next idle state
        /// </summary>
        private Vector2 nextIdleVariant;     
        /// <summary>
        /// next idle animation number from a list
        /// </summary>
        private int NextIdle;
        /// <summary>
        /// how much we want to smooth transition between idle animations
        /// </summary>
        public float IdleSmooth = 3f;   
 
		/// <summary>
		/// Selected input method
		/// </summary>
		public enum InputMethod {None, Keyboard, Joystick, Mouse, All};
        /// <summary>
        /// current input method
        /// </summary>
        public  InputMethod inputMethod = InputMethod.All; //set default

        /// <summary>
        /// axisLimit for any action on an axis
        /// </summary>
        //public float axisLimit = 0.2f; //Using DeadZone instead

        /// <summary>
        /// dead zone for any action on an axis
        /// </summary>
        public float axisDeadZone = 0.2f;

        /// <summary>
        /// input values for Stick One and Stick Two
        /// </summary>
        Vector3 stickInput;

        /// <summary>
        /// Name of the keyboard axis
        /// </summary>
        public string keyMoveAxis = "keyboard move"; //TODO define defaults
        public string keyTurnAxis = "keyboard turn";
        public string keyStrafeAxis = "keyboard strafe";

        /// <summary>
        /// name of the joystic axis 
        /// </summary>
        public string joyMoveAxis = "Y axis"; //inverted
        public string joyTurnAxis = "4th axis"; //non-inverted
        public string joyStrafeAxis = "X axis";  //non-inverted

        /// <summary>
        /// name of the mouse axis
        /// </summary>
        string mouseMoveAxis = "mouse move";
        string mouseTurnAxis = "mouse turn";
        string mouseStrafeAxis = "mouse strafe";  //probably not axis but (mouse) key + mouseTurnAxis

        // Buttons
        string joyAlertButton = "joystick button 3";
        string joySitButton = "joystick button 0";
        string joyLookButton = "joystick button 4";

        /// <summary>
        /// Check if animation in any of the IDLE state
        /// </summary>
        public bool Idle
        {
        	get
        	{
        		animState = anim.GetCurrentAnimatorStateInfo(0);

                if (animState.nameHash == idleBlendTree)
                {
                    //Debug.Log("idle test " + animState.nameHash + " : " + idleBlendTree);
                    return true;
                }
				return false;
        	}
        }


        /// <summary>
        /// speed of the character
        /// </summary>
        public float Move
        {
            get { return move; }
            set
            {
                if (value > axisDeadZone || value < -axisDeadZone)
                    idle = false;

                move = value;

                //Debug.Log("Move:" + move);
            }
        }
        private float move;

		/// <summary>
		/// strafe value
		/// </summary>
        public float Strafe
        {
            get { return strafe; }
            set
            {
                if (value > axisDeadZone || value < -axisDeadZone)
                    idle = false;

                strafe = value;

                //Debug.Log("Strafe:" + strafe);
            }
        }
		private float strafe;

		/// <summary>
		/// direction of the character
		/// </summary>
        public float Direction
        {
            get { return direction; }
            set
            {
                if (value > axisDeadZone || value < -axisDeadZone)
                    idle = false;

                direction = value;

                //Debug.Log("Direction:" + direction);
            }
        }
		private float direction;

		/// <summary>
		/// looking direction of the character
        /// Unused.
		/// </summary>
        public float LookAround
        {
            get { return lookAround; }
            set
            {
                if (value > axisDeadZone || move < -axisDeadZone)
                    idle = false;

                lookAround = value;
            }
        }
        private float lookAround;

		/// <summary>
		/// Run flag to indicate if character should be in running mode
		/// </summary>
		public bool Run
		{
			get{ return run;}
			set
			{
				if(value)
					idle = false;

				run = value;
			}
		}
		private bool run;

		/// <summary>
		/// sit flag to indicate sit animation
		/// </summary>
		public bool SitDown
		{
			get{ return sitDown;}
			set
			{

				if(sitDown)
				{
					idle = false;
                    Move = 0;
                    Direction = 0;

				}
				sitDown = value;
            }
		}
		private bool sitDown;

		/// <summary>
		/// Alert flag to indicate Alert mode
		/// </summary>
		public bool Alert
		{
			get{ return alert;}
			set
			{
				alert = value;
				if(value)
					idle = false;
			}
		}
		private bool alert;

        /// <summary>
        /// Look flag to indicate LookAround Mode
        /// </summary>
        public bool Look
        {
            get { return look; }
            set
            {
                look = value;
                if (value)
                    idle = false;
            }
        }
        private bool look;

        /// <summary>
		/// Animation controller
		/// </summary>
	 	private Animator anim;

	 	/// <summary>
	 	/// Current animation state
	 	/// </summary>
        private AnimatorStateInfo animState;



		/// <summary>
		/// Set default values
		/// </summary>
		void Awake () 
		{
			//get animator from current gameObject
			anim = GetComponent<Animator>();

		}

        void Start()
        {
            /// <summary>
            /// validate parameters on start
            /// </summary>
            switch (inputMethod)
            {
                case InputMethod.Keyboard:
                    if (keyMoveAxis.Length == 0 || keyTurnAxis.Length == 0 || keyStrafeAxis.Length == 0)
                        throw new System.ArgumentNullException("Missing keyboard axis! Please set up each axis!");

                    break;

                case InputMethod.Mouse:
                    if (mouseMoveAxis.Length == 0 || mouseTurnAxis.Length == 0 || mouseStrafeAxis.Length == 0)
                        throw new System.ArgumentNullException("Missing Mouse axis! Please set up each axis!");

                    break;

                case InputMethod.Joystick:
                    if (joyMoveAxis.Length == 0 || joyTurnAxis.Length == 0 || joyStrafeAxis.Length == 0)
                        throw new System.ArgumentNullException("Missing Joystick axis! Please set up each axis!");

                    break;

                case InputMethod.All:
                    if (keyMoveAxis.Length == 0 || keyTurnAxis.Length == 0 || keyStrafeAxis.Length == 0 || joyMoveAxis.Length == 0 || joyTurnAxis.Length == 0 || joyStrafeAxis.Length == 0)
                        throw new System.ArgumentNullException("Missing Joystick axis! Please set up each axis!");

                    break;
            }

            /// <summary>
            /// Populate the Idle Animation's list. 
            /// Would be nice if thes could be read directly from Animator Controller
            /// </summary>
            idleAnimsList = new Vector2[] { idle1, idle2, idle3, idle4, idle5, idle6 }; 
        }
	
		/// <summary>
		/// Get input and send it to the animator, which will translate it to animation
		/// </summary>
		void Update () 
		{

			//animState = anim.GetCurrentAnimatorStateInfo(0);

			ProcessInput();

            ProcessDeadZone();

			UpdateAnimator();

            ////mouse test
            //if (Input.GetAxis(mouseTurnAxis) < 0)
            //{
            //    //Code for action on mouse moving left
            //    print("Mouse moved left");
            //}
            //if (Input.GetAxis(mouseTurnAxis) > 0)
            //{
            //    //Code for action on mouse moving right
            //    print("Mouse moved right");
            //}

		}


        
        /// <summary>
		/// Update the character based on local values.
		/// These values can be modified by
		/// </summary>
		void UpdateAnimator()
		{

			//change idle state if possible
			if(Idle)
			{
                IdleVariants();
			}
            else
            {
                NextIdle = 0;
            }
 
			//update animator		    
			anim.SetFloat("Move", stickInput.x);
			anim.SetFloat("Direction", stickInput.y);
            anim.SetFloat("Strafe", stickInput.z);
            //anim.SetFloat("LookAround", lookAround);  //Unused
			anim.SetBool("Idle", idle);
			anim.SetBool("SitDown", sitDown);
			anim.SetBool("Alert", alert);
            anim.SetBool("LookAround", look);
			anim.SetBool("Run", run);

			//Debug.Log("Move:"+move);
			//Debug.Log("Dir:"+direction);
			//Debug.Log("Strafe:"+strafe);

		}

		/// <summary>
		/// Get the input from keyboard or Joystick
		/// </summary>
		void ProcessInput()
		{
			idle = true;

            //Process movement based on inputMethod
            switch(inputMethod)
            {
            	case InputMethod.None:
            		//we don't handle input in this case
            		//leave this for control from outside.
            		return;            		

            	case InputMethod.Keyboard:
            		ProcessKeyboard();
            		break;

            	case InputMethod.Mouse:
            		ProcessMouse();
            		break;

            	case InputMethod.Joystick:
            		ProcessJoystick();
            		break;

                case InputMethod.All:
                    ProcessAll();
                    break;

            }

			//process basic keys
			if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Exit!");
                Application.Quit();
            }

            if( Input.GetKey(KeyCode.F1))
            {
                inputMethod = InputMethod.Keyboard;     //set to keyboard
            }
            else if( Input.GetKey(KeyCode.F2))
            {
                inputMethod = InputMethod.Mouse;        //set to mouse
            }
            else if( Input.GetKey(KeyCode.F3))
            {
                inputMethod = InputMethod.Joystick;     //set to joystick
            }
            else if (Input.GetKey(KeyCode.F4))
            {
                inputMethod = InputMethod.All;     //set to joystick
            }

		}

        /// <summary>
        /// Tweak input values acording to a deadzone.
        /// Original code by stfx
        /// </summary>
        void ProcessDeadZone()
        {

            //Here we need 3 axis working together. You may need different configuration.
            //stickInput = new Vector3(Input.GetAxis(joyMoveAxis) + Input.GetAxis(keyMoveAxis), Input.GetAxis(joyTurnAxis) + Input.GetAxis(keyTurnAxis), Input.GetAxis(joyStrafeAxis) + Input.GetAxis(keyStrafeAxis)); 
            stickInput = new Vector3(Move, Direction, Strafe); 
            float inputMagnitude = stickInput.magnitude;

            if (inputMagnitude < axisDeadZone)
            {
                stickInput = Vector3.zero;
            }
            else
            {
                // rescale the clipped input vector into the non-dead zone space
                stickInput *= (inputMagnitude - axisDeadZone) / ((1f - axisDeadZone) * inputMagnitude);
            }

        }

		/// <summary>
		/// Process keyboard inputs
		/// </summary>
        void ProcessAll()
        {
            //process axis
            Move = Input.GetAxis(joyMoveAxis) + Input.GetAxis(keyMoveAxis);         // +Input.GetAxis(mouseMoveAxis); //Need more work on mouse controls
            Direction = Input.GetAxis(joyTurnAxis) + Input.GetAxis(keyTurnAxis);    // +Input.GetAxis(mouseTurnAxis);
            Strafe = Input.GetAxis(joyStrafeAxis) + Input.GetAxis(keyStrafeAxis);   // +Input.GetAxis(mouseStrafeAxis);

            //process buttons
            //TODO implement
            Alert = Input.GetButton(joyAlertButton);
            //Run = Input.GetButton(joyAlertButton);
            SitDown = Input.GetButton(joySitButton);
            Look = Input.GetButton(joyLookButton);

        }

        void ProcessKeyboard()
		{
			//process axis
            Move 		= Input.GetAxis(keyMoveAxis);
            Direction 	= Input.GetAxis(keyTurnAxis);
            Strafe 		= Input.GetAxis(keyStrafeAxis);

			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Process joystick inputs
		/// </summary>
		void ProcessJoystick()
		{
			//process axis
            Move 		= Input.GetAxis(joyMoveAxis);
            Direction 	= Input.GetAxis(joyTurnAxis);
			Strafe 		= Input.GetAxis(joyStrafeAxis);


			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Process mouse inputs
		/// </summary>
		void ProcessMouse()
		{
			//process axis
            Move 		= Input.GetAxis(mouseMoveAxis);
            Direction = Input.GetAxis(mouseTurnAxis);
            Strafe 		= Input.GetAxis(mouseStrafeAxis);

			//process buttons
			//TODO implement
		}

		/// <summary>
		/// Change idle if possible
		/// </summary>
        void IdleVariants()
        {
            int animLoopNum = (int)animState.normalizedTime;
            float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2
            //Debug.Log(animPercent);
            if ((animPercent > .85f) && (canChangeState == true))
            {
                NextIdle = UnityEngine.Random.Range(0, 6);  //random integer number between min [inclusive] and max [exclusive]
                canChangeState = false;
            }
            else if (animPercent > .0f && animPercent < .5f && (canChangeState == false))
            {
                canChangeState = true;
            }

            //get the vector for the nex Idle Animation
            nextIdleVariant = idleAnimsList[NextIdle];
            //smooth transition in 2D blendTree
            currentIdleVariant = Vector2.Lerp(currentIdleVariant, nextIdleVariant, Time.deltaTime * IdleSmooth); 
            //set the Animator Controller variables controlling 2D blend tree
            anim.SetFloat("IdleRandA", currentIdleVariant.x);
            anim.SetFloat("IdleRandB", currentIdleVariant.y);
        }

	}
}