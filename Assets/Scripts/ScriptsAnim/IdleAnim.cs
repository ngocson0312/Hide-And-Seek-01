using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : StateMachineBehaviour
{
    [SerializeField] float TimeIdle;
    [SerializeField] int NumberIdleAnim;


    bool isBored;
    float idelTimeAnim;
    int boredAnimation;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isBored == false)
        {
            idelTimeAnim += Time.deltaTime;
            if(idelTimeAnim> TimeIdle && stateInfo.normalizedTime %1 < 0.02f)
            {
                isBored = true;
                boredAnimation = Random.Range(1, NumberIdleAnim +1);
                boredAnimation = boredAnimation * 2 - 1;
                animator.SetFloat("IdleAnim", boredAnimation - 1);
            }
        }
        else if(stateInfo.normalizedTime %1 >0.98)
        {
            ResetIdle();
        }
        animator.SetFloat("IdleAnim", boredAnimation, 0.2f,Time.deltaTime);
    }


    private void ResetIdle()
    {
        if(isBored)
        {
            boredAnimation--;
        }
        isBored = false;
        idelTimeAnim = 0;
        boredAnimation = 0;
    }
}
