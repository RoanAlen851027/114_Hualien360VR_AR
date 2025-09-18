/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using NUnit.Framework;
using Unity.VisualScripting.Antlr3.Runtime;


public class LobbyController : MonoBehaviour
{
    public Text CharaterName;
    public string Professtion;
    public Text CharaterProfessionText;

    public TalkToPACscore pACscore;
    private Coroutine coroutine;


    public UnityEvent Level_A_Finished;
    public UnityEvent Level_B_Finished;
    public UnityEvent Level_C_Finished;

    [Header("顯示按鈕")]
    public List<GameObject> BTN_Rockets; //0 第一個火箭
    public List<Button> BTN_CanClick;
    public List<Sprite> BTN_Show;
    public List<Sprite> BTN_Finished_Img;

    //private void Start()
    //{
    //    coroutine = StartCoroutine(WaitToStart());
    //}
    private void OnEnable()
    {
        coroutine = StartCoroutine(WaitToStart()); //測試用
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.5f);

        CharaterName.text = pACscore.pAC_Score.UserName;

        switch (pACscore.pAC_Score.charaterProfession)
        {
            case CharaterProfession.Conductor:
                Professtion = "指揮官";
                break;
            case CharaterProfession.Picketer:
                Professtion = "糾察官";
                break;
            case CharaterProfession.Enginer:
                Professtion = "工程官";
                break;
            case CharaterProfession.Walker:
                Professtion = "漫遊者";
                break;
            case CharaterProfession.Creater:
                Professtion = "創意官";
                break;
            case CharaterProfession.Doctor:
                Professtion = "醫療官";
                break;
            case CharaterProfession.Strategist:
                Professtion = "策略官";
                break;
        }
        CharaterProfessionText.text = Professtion;


        if (pACscore.pAC_Score.LobbyFirstBool != true)
        {
            pACscore.SetLobbyState(1);
        }
        else
        {
            pACscore.SetLobbyState(2);
        }

        //第一關 整個全部結束 且 取得裝備 打開控制 
        if (pACscore.pAC_Score.State_1_GetEquiment == true &&pACscore.pAC_Score.Level_state_1== Level_State.Finish)
        {
            RocketSet(false,true,false,false);
            BtnSet(false, true, false, false);
            BTN_CanClick[0].image.sprite = BTN_Finished_Img[0];
            BTN_CanClick[1].image.sprite = BTN_Show[1];

            Level_A_Finished.Invoke();
        }

        //第二關 整個全部結束 且 取得裝備 打開控制 
        if (pACscore.pAC_Score.State_2_GetEquiment == true && pACscore.pAC_Score.Level_state_2 == Level_State.Finish)
        {
            RocketSet(false, false, true, false);
            BtnSet(false, false, true, false);
            BTN_CanClick[0].image.sprite = BTN_Finished_Img[0];
            BTN_CanClick[1].image.sprite = BTN_Finished_Img[1];
            BTN_CanClick[2].image.sprite = BTN_Show[2];

            Level_B_Finished.Invoke();
        }

        //第三關 整個全部結束 且 取得裝備 打開控制 
        if (pACscore.pAC_Score.State_3_GetEquiment == true && pACscore.pAC_Score.Level_state_3 == Level_State.Finish)
        {
            RocketSet(false, false, false, true);
            BtnSet(false, false, false, true);
            BTN_CanClick[0].image.sprite = BTN_Finished_Img[0];
            BTN_CanClick[1].image.sprite = BTN_Finished_Img[1];
            BTN_CanClick[2].image.sprite = BTN_Finished_Img[2];
            BTN_CanClick[3].image.sprite = BTN_Show[3];
            Level_A_Finished.Invoke();
            Level_B_Finished.Invoke();
            Level_C_Finished.Invoke();
        }

        if (pACscore.pAC_Score.Level_state_4== Level_State.Finish)
        {
            RocketSet(false, false, false, false);
            BtnSet(true, true, true, true);

            BTN_CanClick[0].image.sprite = BTN_Show[0];
            BTN_CanClick[1].image.sprite = BTN_Show[1];
            BTN_CanClick[2].image.sprite = BTN_Show[2];
            BTN_CanClick[3].image.sprite = BTN_Show[3];

            Level_A_Finished.Invoke();
            Level_B_Finished.Invoke();
            Level_C_Finished.Invoke();
        }

    }

    //按鈕上火箭開關
    public void RocketSet(bool LvA ,bool LvB ,bool LvC,bool LvD)
    {
        BTN_Rockets[0].SetActive(LvA);

        BTN_Rockets[1].SetActive(LvB);

        BTN_Rockets[2].SetActive(LvC);

        BTN_Rockets[3].SetActive(LvD);
    }
    public void BtnSet(bool LvA, bool LvB, bool LvC, bool LvD)
    {
        BTN_CanClick[0].enabled = LvA;
        BTN_CanClick[1].enabled = LvB;
        BTN_CanClick[2].enabled = LvC;
        BTN_CanClick[3].enabled = LvD;

    }

}
