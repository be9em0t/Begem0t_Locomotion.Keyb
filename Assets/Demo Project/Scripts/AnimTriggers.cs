using UnityEngine;
using System.Collections;

public class AnimTriggers : MonoBehaviour
{
    // Create a reference to the animator component
    private Animator animator;

    void Start()
    {
        // initialise the reference to the animator component
        animator = GetComponent<Animator>();
    }

    // check for colliders with a Trigger collider
    // if we are entering something called JumpTrigger, set a bool parameter called JumpDown to true..
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "SitZone")
        {
            animator.SetBool("SitDown", true);
        }
    }

    // ..and when leaving the trigger, reset it to false
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "SitZone")
        {
            animator.SetBool("SitDown", false);
        }
    }
}
