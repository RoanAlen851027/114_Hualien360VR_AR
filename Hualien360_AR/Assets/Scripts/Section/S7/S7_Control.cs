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

public class S7_Control : MonoBehaviour
{

    public List<GameObject> S7_InteractionObject;

    public List<Animator> S7_InteractionAnimator;
    //public List<S7_SendEvent> S7_InteractionCount;

    public S7_I1 S7__I1;
    public S7_I2 S7__I2;
    public S7_I3 S7__I3;
    public S7_I4 S7__I4;

    public Animator S7_End;

    public void ClearAllCount()
    {
        //for (int i = 0; i < S7_InteractionCount.Count; i++)
        //{
        //    S7_InteractionCount[i].ClickCount = 0;
        //    S7_InteractionObject[i].SetActive(false);
        //}
        S7__I1.count = 0; 
        S7__I2.count = 0; 
        S7__I3.count = 0;
        S7__I4.count = 0;
        for (int i = 0; i < S7_InteractionObject.Count; i++)
        {
            S7_InteractionObject[i].SetActive(false);
        }

        S7_InteractionAnimator[0].Play("S7_1");
        S7_InteractionAnimator[1].Play("S7_2");
        S7_InteractionAnimator[2].Play("S7_3");
        S7_InteractionAnimator[3].Play("S7_4");
        S7_End.SetBool("S7_End", false);

    }

    public void Enable_InteractionObject(int step)
    {
        for (int i = 0; i < S7_InteractionObject.Count; i++)
        {
            S7_InteractionObject[i].gameObject.SetActive(i == step);

        }
    }

    public void End_InteractionObject()
    {
        Debug.Log("互動結束");
        S7_End.SetBool("S7_End", true);
    }
   

}
