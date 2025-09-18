/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class S8_Control : MonoBehaviour
{

    public List<Button> S8_BTN;

    public Animator S8_Animator;

    public bool test_S8;
    public GameObject Title_BTN;

    public List<CanvasFadeOut> S8_CanvasFadeOuts;

    public void BackClick()
    {
        for (int i = 0; i < S8_CanvasFadeOuts.Count; i++)
        {
            S8_CanvasFadeOuts[i].FadeOut();
        }
    }

    public void ShowTitle()
    {
        Title_BTN.SetActive(true);
    }

    public void CanClick()
    {
        for (int i = 0; i < S8_BTN.Count; i++)
        {
            S8_BTN[i].enabled = true;
        }
    }

    public void DontClick()
    {
        for (int i = 0; i < S8_BTN.Count; i++)
        {
            S8_BTN[i].enabled = false;
        }
        Title_BTN.SetActive(false);

    }

    //--------------------監視對象
    public void S8_SeePeople_Start()
    {

        test_S8 = true;
        S8_Animator.Play("S8_SeePeople");

    }
    public void S8_SeePeople_End()
    {
        S8_Animator.SetBool("S8_SP", true);
    }

    //--------------------摯愛
    public void S8_Lover_Start()
    {
        S8_Animator.Play("S8_Lover");
    }
    public void S8_Lover_End()
    {
        S8_Animator.SetBool("S8_L", true);

    }

    //------------------歷史人物
    public void S8_HistioryPeople_Start()
    {
        S8_Animator.Play("S8_History");
    }
    public void S8_HistioryPeople_End()
    {
        S8_Animator.SetBool("S8_HP", true);
    }


    //-------------------鄰居叔叔
    public void S8_Uncle_Start()
    {
        S8_Animator.Play("S8_Uncle");
    }
    public void S8_Uncle_End()
    {
        S8_Animator.SetBool("S8_U", true);
    }



    public void S8_ClearAniBool()
    {
        S8_Animator.SetBool("S8_SP", false);
        S8_Animator.SetBool("S8_L", false);
        S8_Animator.SetBool("S8_HP", false);
        S8_Animator.SetBool("S8_U", false);

    }

}
