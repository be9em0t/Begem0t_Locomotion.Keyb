using UnityEngine;
using System.Collections;

public class IdleTest : MonoBehaviour {

    int NextIdle;               //select next idle animation from a list
    bool NextIdleAllow = true;  //allow to select next idle animation
    Vector2[] idleAnimsList;    //array holding Idle animations' vectors in 2D BlendTree
    Vector2 currentIdleVector;  //current idle animation' vector
    Vector2 nextIdleVector;     //next idle animation' vector
    float IdleSmooth = 0.01f;   //how much we want to lerp when transitioning between idle animations
    private Animator anim;
    private AnimatorStateInfo animState;

    //TEMP
    enum StandIdleVariants
    {
        Stand_Idle,
        Stand_Idle_Long,
        Stand_Idle_Dynamic,
        Stand_Idle_Left,
        Stand_Idle_Right,
        Stand_Idle_SwitchFeet
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        //describe Idle animations' vectors in 2D BlendTree (you need a corresponding 2D blendtree in Animator Controller)
        Vector2 idle1 = new Vector2(0f, 0f);
        Vector2 idle2 = new Vector2(0f, 1f);
        Vector2 idle3 = new Vector2(-1f, 0.2f);
        Vector2 idle4 = new Vector2(1f, 0.2f);
        Vector2 idle5 = new Vector2(-1f, -1f);
        Vector2 idle6 = new Vector2(1f, -1f);
        idleAnimsList = new Vector2[] { idle1, idle2, idle3, idle4, idle5, idle6 }; //populate the idle animation's list
    }
	
	// Update is called once per frame
	void Update () {
        animState = anim.GetCurrentAnimatorStateInfo(0);    // Get our animator's current state. we need this to be know when to switch animations

        IdleVariantsTest();
	}

    void IdleVariantsTest()
    {
        int animLoopNum = (int)animState.normalizedTime;
        float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2
        if ((animPercent > .9f) && (NextIdleAllow == true) )
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
