using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("!!!!");
        animator.SetBool("LedgeClimbFinished", true);
    }
}