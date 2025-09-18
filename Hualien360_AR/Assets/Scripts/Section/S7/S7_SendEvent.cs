/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using RoanAlen.Extension;
using UnityEngine.Events;

public class S7_SendEvent : MonoBehaviour
{

    public int ClickCount;
    public List<Animator> S7_InteractionAnimator;

    public UnityEvent Click_NextEvent;
    public UnityEvent PlayShortSFX_Event;
    public UnityEvent PlayLongSFX_Event;

    public UnityEvent ClearEvent;

    public void ClearCount()
    {
        ClickCount = 0;
    }
    //public void ClickNextEvent()
    //{
    //    ClickCount++;
    //}
    //public void S7_1_Ani()
    //{
    //}
    //public void S7_2_Ani()
    //{
    //    S7_InteractionAnimator[1].SetBool("S7_2_Click", true);
    //}
    //public void S7_3_Ani()
    //{
    //    S7_InteractionAnimator[2].SetBool("S7_3_Click", true);
    //}
    //public void S7_4_Ani()
    //{
    //    S7_InteractionAnimator[3].SetBool("S7_4_Click", true);
    //}

    public void NextEvent()
    {
        Click_NextEvent?.Invoke();
    }

    public void InteractionEvent_1()
    {
        ClickCount++;
        if (ClickCount==1)
        {
            //S7_InteractionAnimator[0].SetBool("S7_1_Click", true);
             
            S7_InteractionAnimator[0].Play("S7_1_R");

        }
    }

    public void InteractionEvent_2()
    {
        ClickCount++;

        if (ClickCount==1)
        {
            S7_InteractionAnimator[1].Play("S7_2_R");
        }
        if (ClickCount == 2)
        {

            S7_InteractionAnimator[1].Play("S7_2_2R");
        }
    }

    public void InteractionEvent_3()
    {
        ClickCount++;

        if (ClickCount == 1)
        {
            S7_InteractionAnimator[2].Play("S7_3_R");
        }
        if (ClickCount == 2)
        {

            S7_InteractionAnimator[2].Play("S7_3_2R");
        }
    }


    public void InteractionEvent_4()
    {
        ClickCount++;
        if (ClickCount == 1)
        {
            //S7_InteractionAnimator[0].SetBool("S7_1_Click", true);

            S7_InteractionAnimator[3].Play("S7_4_R");

        }
    }

    public void EndInteraction()
    {
        ClickCount = 0;
    }

    public void ShortSFX_Event()
    {
        PlayShortSFX_Event?.Invoke();
    }
    public void LongSFX_Event()
    {
        PlayLongSFX_Event?.Invoke();
    }
}
