using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class LvB_EnergyManager : MonoBehaviour
{
    public TalkToPACscore talkToPACscore;

    [Header("Dependce")]
    public List<LvB_Energy> energy_Values;
    public List<TextMeshProUGUI> energy_Text;


    public List<EnergyValue> energyBar;

    public List<int> scores;
    public List<bool> scoreFinish;

    public UnityEvent Bubble_Event;

    public List<Button> scoreButton;
    public UnityEvent EndEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ValueUpdate();
        }
    }

    public void ValueUpdate()
    {
        scores = PAC_Score.instance.Reslut_score;
        for (int i = 0; i < energy_Values.Count; i++)
        {
            energy_Values[i].InitScore(scores[i]);
            Debug.Log(scores[i]);
            Debug.Log(energy_Values[i]);

        }
    }

    public void ValueBar()
    {
        scores = PAC_Score.instance.Reslut_score;
        for (int i = 0; i < energyBar.Count; i++)
        {
            energyBar[i].InitLerpScoreEightyColor(scores[i]);
        }
        Bubble_Event?.Invoke();
    }

    public void CheckEND()
    {
        if (talkToPACscore.pAC_Score.All_End==true)
        {

        }
        else
        {
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] >= 60)
                {
                    scoreButton[i].enabled = false;
                    scoreFinish[i] = true;
                }
            }
            if (scoreFinish.All(f => f))
            {
                Debug.Log("所有分數都達標！");

                for (int i = 0; i < scoreButton.Count; i++)
                {
                    scoreButton[i].enabled = false;//取消 按鈕可點
                    EndEvent?.Invoke();//執行結束事件
                }
            }
        }

    }


//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        if (energy_Values != null && energy_Values.Count == 0)
//        {
//            energy_Values = GetComponentsInChildren<LvB_Energy>().ToList();
//        }
//    }

//#endif
}
