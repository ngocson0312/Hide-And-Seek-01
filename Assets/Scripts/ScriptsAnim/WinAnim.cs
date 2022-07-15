using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnim : StateMachineBehaviour
{
    [SerializeField] float TimeWin;
    [SerializeField] int NumberWinAnim;


    bool isBored;
    float winTimeAnim;
    int boredAnimation;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isBored == false)
        {
            winTimeAnim += Time.deltaTime;
            if (winTimeAnim > TimeWin && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isBored = true;
                boredAnimation = Random.Range(1, NumberWinAnim + 1);
                boredAnimation = boredAnimation * 2 - 1;
                animator.SetFloat("WinIdle", boredAnimation - 1);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }
        animator.SetFloat("WinIdle", boredAnimation, 0.2f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (isBored)
        {
            boredAnimation--;
        }
        isBored = false;
        winTimeAnim = 0;
        boredAnimation = 0;
    }
}
