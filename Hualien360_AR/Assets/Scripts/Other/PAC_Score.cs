/********************************
---------------------------------
著作者：RoanAlen
用途：所有PAC 出來的分數
---------------------------------
*********************************/
using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Unity.VisualScripting;
public enum PAC_Mode
{
    None,
    GoTest,
    HaveScore
}

public enum CharaterProfession
{
    None,       //暫無
    Conductor,  //指揮官
    Picketer,   //糾察官
    Enginer,    //工程師
    Walker,     //漫遊者
    Creater,    //創造家
    Doctor,     //治療官
    Strategist, //策略家
    //[System.ComponentModel.Description("漫遊者")]
}

public enum Lobby_State
{
    None,
    Introduction,
    NoIntroduction,
    Finish,
}

public enum Level_State
{
    None,
    NotStart,
    Introduction,//關卡介紹
    NoIntroduction,//如果通關體驗第二次就取消介紹
    Finish,//通過

}

public enum EnergyColor
{
    Blue,
    Red,
    Green,
    Yellow,
    Purple,
}

public enum GemValue
{
    None,
    CP,
    NP,
    A,
    FC,
    AC
}

public class PAC_Score : MonoBehaviour
{
    public static PAC_Score instance;

    [Header("使用者名稱")]
    public string UserName;
    [Header("PAC分數")]
    public List<int> PAC_score; // 藍色 CP 0  , 紅色 NP 1 ,  綠色 A  2 , 紫色 AC 3 , 黃色 FC 4  

    public bool GetResult;
    [Header("計算後分數")]
    public List<int> Reslut_score;  // 藍色 CP 0  , 紅色 NP 1 ,  綠色 A  2 , 紫色 AC 3 , 黃色 FC 4  

    [Header("等待新增分數")]
    public List<int> Wait_Add_score;  // 藍色 CP 0  , 紅色 NP 1 ,  綠色 A  2 , 紫色 AC 3 , 黃色 FC 4  

    public PAC_Mode pac_Mode;

    [Space(15)]
    public CharaterProfession charaterProfession;

    [Header("Lobby介紹狀況")]
    public Lobby_State LobbyContent;
    public bool LobbyFirstBool;

    [Header("各關卡狀態")]
    public bool State_1_GetEquiment; //如果False且第1關已經通過 開啟領取裝備面板
    public Level_State Level_state_1;//大廳介紹

    public bool State_2_GetEquiment; //如果False且第2關已經通過 開啟領取裝備面板
    public Level_State Level_state_2;//大廳介紹
    
    public bool State_3_GetEquiment; //如果False且第3關已經通過 開啟領取裝備面板
    public Level_State Level_state_3;//大廳介紹
    
    public bool State_4_GetEnd; //如果False且第4關已經通過 開啟結束畫面
    public Level_State Level_state_4;//大廳介紹

    [Header("全部體驗結束")]
    public bool All_End; // 全部結束自由體驗

    public void GoTest()
    {
        pac_Mode = PAC_Mode.GoTest;
    }
    public void HaveScore()
    {
        pac_Mode = PAC_Mode.HaveScore;
    }

    public void GetReultScore()
    {
        for (int i = 0; i < PAC_score.Count; i++)
        {
            Reslut_score[i] = PAC_score[i] + 12;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 保證跨場景保持
            Debug.Log("PAC_Score instance initialized!");
        }
        else
        {
            Destroy(gameObject); // 防止多重實例
        }
    }

    public void LobbyFirstTime()
    {
        if (LobbyFirstBool ==false)
        {
            LobbyFirstBool=true;
            LobbyContent= Lobby_State.Introduction;
        }
    }

    public void Send_AddScore()
    {
        for (int i = 0; i < Reslut_score.Count; i++)
        {
            Reslut_score[i] = Reslut_score[i] + Wait_Add_score[i];
        }
    }

    public void Clear_AddScore()
    {
        for (int i = 0; i < Wait_Add_score.Count; i++)
        {
            Wait_Add_score[i] = 0;
        }
    }

    public void GetLvB_WaitAddScore()
    {
        for (int i = 0; i < Wait_Add_score.Count; i++)
        {
            Wait_Add_score[i] = 60-Reslut_score[i];
        }
    }
}
