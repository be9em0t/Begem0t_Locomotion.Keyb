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
        Vector2[] idleAnimsList;    //array holding Idle animations' vectors in 2D BlendTree
        static int idleBlendTree = Animator.StringToHash("Base Layer.STAND_IDLE"); //idle blend tree

        /// <summary>
        /// flag to indicate if any valid input was given
        /// or the animation needs to stay in idle
        /// </summary>
        private bool idle;
        /// <summary>
        /// flag to indicate if idle state can change
        /// </summary>
        private bool canChangeState = true;
        bool NextIdleAllow = true;  //allow to select next idle animation

        /// <summary>
        /// current idle state
        /// </summary>
        private int CurrentIdleVariant = 1;
        /// <summary>
        /// next idle state
        /// </summary>
        private int NextIdleVariant = 1;

        int NextIdle;               //select next idle animation from a list
        Vector2 currentIdleVector;  //current idle animation' vector
        Vector2 nextIdleVector;     //next idle animation' vector
        float IdleSmooth = 0.01f;   //how much we want to lerp when transitioning between idle animations


        
		/// <summary>
		/// Selected input method
		/// </summary>
		public enum InputMethod {None, Keyboard, Joystick, Mouse};
        /// <summary>
        /// current input method
        /// </summary>
        public  InputMethod inputMethod = InputMethod.Joystick;

        /// <summary>
        /// axisLimit for any action on an axis
        /// </summary>
        public float axisLimit = 0.1f;

        /// <summary>
        /// dead zone for any action on an axis
        /// </summary>
        public float axisDeadZone = 0.1f;

        /// <summary>
        /// input values for Stick One and Stick Two
        /// </summary>
        Vector3 stickInput;

        /// <summary>
        /// Name of the keyboard axis
        /// </summary>
        public string keyMoveAxis; //TODO define defaults
        public string keyTurnAxis;
        public string keyStrafeAxis;

        /// <summary>
        /// name of the joystic axis 
        /// </summary>
        public string joyMoveAxis = "Y axis";
        public string joyTurnAxis = "4th axis";
        public string joyStrafeAxis = "X axis";

        /// <summary>
        /// name of the mouse axis
        /// </summary>
        public string mouseMoveAxis;
        public string mouseTurnAxis;
        public string mouseStrafeAxis;  //probably not axis but (mouse) key + mouseTurnAxis


        /// <summary>
        /// Check if animation in any of the IDLE state
        /// </summary>
        public bool Idle
        {
        	get
        	{
        		animState = anim.GetCurrentAnimatorStateInfo(0);
                //foreach (int s in idleAnims)
                //{
                //    if (animState.nameHash == s)
                //    {
                //        return true;
                //    }
                //}

                if (animState.nameHash == idleBlendTree)
                {
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
                if (value > axisLimit || value < -axisLimit)
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
                if (value > axisLimit || value < -axisLimit)
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
                if (value > axisLimit || value < -axisLimit)
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
        //public float LookAround
        //{
        //    get { return lookAround; }
        //    set
        //    {
        //        if (value > axisLimit || move < -axisLimit)
        //            idle = false;

        //        lookAround = value;
        //    }
        //}
        //private float lookAround;

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

		/// <summary>
		/// validate parameters on start
		/// </summary>
        void Start()
        {
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
            }

            //populate the idle animation's list
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

            Debug.Log(Idle + " : " + idle);

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

			//update animator		    
			anim.SetFloat("Move", stickInput.x);
			anim.SetFloat("Direction", stickInput.y);
            anim.SetFloat("Strafe", stickInput.z);
            //anim.SetFloat("LookAround", lookAround);  //Unused
			anim.SetBool("Idle", idle);
			anim.SetBool("SitDown", sitDown);
			anim.SetBool("Alert", alert);
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

            }

			//process basic keys
			if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Exit!");
                Application.Quit();
            }

            if( Input.GetKey(KeyCode.F1))
            {
            	//set to keyboard
            	inputMethod = InputMethod.Keyboard;
            }
            else if( Input.GetKey(KeyCode.F2))
            {
            	inputMethod = InputMethod.Mouse;
            }
            else if( Input.GetKey(KeyCode.F3))
            {
            	inputMethod = InputMethod.Joystick;
            }

		}

        /// <summary>
        /// Tweak input values acording to a deadzone.
        /// </summary>
        void ProcessDeadZone()
        {

            //code by stfx
            stickInput = new Vector3(Input.GetAxis(joyMoveAxis), Input.GetAxis(joyTurnAxis), Input.GetAxis(joyStrafeAxis));
            float inputMagnitude = stickInput.magnitude;

            //Precize DeadZone for Stick One            
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
        //void IdleVariants()
        //{
        //    {

        //        int animLoopNum = (int)animState.normalizedTime;
        //        float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2

        //        if (animPercent > .85f && canChangeState == true)       //crossfade after this percent
        //        {
        //            while (CurrentIdleVariant == NextIdleVariant)
        //                NextIdleVariant = UnityEngine.Random.Range(0, idleAnims.Length);  //random select next transition
        //            canChangeState = false;                             //stop state change until next crossfade
        //            CurrentIdleVariant = NextIdleVariant;               //start selection of next random clip
        //            anim.CrossFade(idleAnims[NextIdleVariant], .1f, -1, 0f);    //Crossfade to
        //        }
        //        else if (animPercent < .3f && canChangeState == false)  //arm for a new crossfade
        //        {
        //            canChangeState = true;
        //        }
        //    }
        //}

        void IdleVariants()
        {
            int animLoopNum = (int)animState.normalizedTime;
            float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2
            Debug.Log(animPercent);
            if ((animPercent > .9f) && (NextIdleAllow == true))
            {
                NextIdle = UnityEngine.Random.Range(0, 6);  //random integer number between min [inclusive] and max [exclusive]
                NextIdleAllow = false;
            }
            else if (animPercent > .0f && animPercent < .5f && (NextIdleAllow == false))
            {
                NextIdleAllow = true;
            }

            nextIdleVector = idleAnimsList[NextIdle];       //get the vector for the nex Idle animation
            currentIdleVector = Vector2.Lerp(currentIdleVector, nextIdleVector, Time.time * IdleSmooth);  //lerp for a smooth transition in 2D blendTree
            anim.SetFloat("IdleRandA", currentIdleVector.x);
            anim.SetFloat("IdleRandB", currentIdleVector.y);
        }

	}
}