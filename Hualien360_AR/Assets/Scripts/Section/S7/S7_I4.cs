/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;


public class S7_I4 : MonoBehaviour
{
    public int count;
    public Animator animator;

    public UnityEvent Click_NextEvent;
    public UnityEvent PlayShortSFX_Event;
    public UnityEvent PlayLongSFX_Event;

    public void OnClicked_I4()
    {
        count++;
        if (count == 1)
        {
            //S7_InteractionAnimator[0].SetBool("S7_1_Click", true);

            animator.Play("S7_4_R");

        }
    }

    public void NextEvent()
    {
        Click_NextEvent?.Invoke();
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
