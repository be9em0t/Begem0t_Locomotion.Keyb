using UnityEngine;
using System.Collections;

public class IdleTest : MonoBehaviour {

    float currentIdle = 0f;
    float NextIdle;
    bool NextIdleAllow = true;
    private Animator anim;
    private AnimatorStateInfo animState;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();	

    }
	
	// Update is called once per frame
	void Update () {

        animState = anim.GetCurrentAnimatorStateInfo(0);// Get our animator's current state
        IdleVariantsTest();

        currentIdle = Mathf.Lerp(currentIdle, NextIdle, Time.time * 0.001f);
        anim.SetFloat("IdleState", currentIdle);

        Debug.Log(currentIdle);

	}

    void IdleVariantsTest()
    {

        int animLoopNum = (int)animState.normalizedTime;
        float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2
        //float animPercent = animState.normalizedTime;
        if ((animPercent > .9f) && (NextIdleAllow == true) )
        {
            Debug.Log("change!");
            NextIdle = UnityEngine.Random.Range(0, 5);  //random select next transition
            NextIdleAllow = false;
        }
        else if (animPercent > .0f && animPercent < .5f && (NextIdleAllow == false))
        {
            NextIdleAllow = true;
        }

        //{
            //int[] IdleAnims = new int[6] { idle00, idle01, idle02, idle03, idle04, idle05 };                //List of available variant anims

            //int animLoopNum = (int)animState.normalizedTime;
            //float animPercent = Mathf.Round(((animState.normalizedTime - animLoopNum) * 100f)) / 100f;     //round to DP2

            //if (animPercent > .85f && canChangeState == true)       //crossfade after this percent
            //{
            //    while (CurrentIdleVariant == NextIdleVariant)
            //        NextIdleVariant = UnityEngine.Random.Range(0, IdleAnims.Length);  //random select next transition
            //    canChangeState = false;                             //stop state change until next crossfade
            //    CurrentIdleVariant = NextIdleVariant;               //start selection of next random clip
            //    anim.CrossFade(IdleAnims[NextIdleVariant], .3f, -1, float.NegativeInfinity);    //Crossfade to
            //}
            //else if (animPercent < .3f && canChangeState == false)  //arm for a new crossfade
            //{
            //    canChangeState = true;
            //}
        //}
    }

}
