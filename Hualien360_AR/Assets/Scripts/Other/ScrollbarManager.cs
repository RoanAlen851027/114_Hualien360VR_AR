using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class ScrollbarManager : MonoBehaviour
{
    [Header("Setting")]


    [Header("Dependce")]
    public List<ScrollbarScore> scrollbarScores;


    //public float value = 0f;
    //public void  Send_Message(string message) => Debug.Log(message); //Lambda
    //public List<int> ScoreValue;

    private void Start()
    {
        //foreach (var item in scrollbarScores)
        //{
        //    item.InitScoreText(0);
        //}
        List<int> scores = PAC_Score.instance.PAC_score;
        for (int i = 0; i < scrollbarScores.Count; i++)
        {
            scrollbarScores[i].InitScoreText(scores[i]);
        }
    }

    public void GetScore()
    {

    }

    public void SendResult()
    {

        // List<???>  ===>  (排序)  OrderBy(??? => ???.參數)    ===>(選擇)  Select(??? => ???.) . ToList 或 ToArray
        List<int> scores = scrollbarScores.OrderBy(value=>value.energy_Color).Select(value=>value.ScoreValue).ToList();
        PAC_Score.instance.PAC_score = scores;

        //for (int i = 0; i < scrollbarScores.Count; i++)
        //{
        //    PAC_Score.instance.PAC_score[i] = scrollbarScores[i].ScoreValue;
        //}

    }

    //可以Reset掉值
    private void Reset()
    {
        
    }



#if UNITY_EDITOR
    //Inspector 修改之前會過這裡  <Inspector防呆處理>
    private void OnValidate() 
    {
        //if (value<0)
        //{
        //    value = 0;
        //}

        //List轉Array
        if (scrollbarScores!=null &&scrollbarScores.Count == 0)
        {
            scrollbarScores = GetComponentsInChildren<ScrollbarScore>().ToList();  //Array轉List
                                                                                   //scrollbarScores = GetComponentsInChildren<ScrollbarScore>().Take(2).ToList();  //Array轉List

            //scrollbarScores.OrderBy(value => (int)value.textColor);
            //scrollbarScores = scrollbarScores.OrderBy(value => value.textColor).ToList();
            //scrollbarScores = scrollbarScores.OrderBy(value => UnityEngine.Random.Range(0, 1000)).ToList(); //亂數搖骰子 點數 然後重新排序
            //scrollbarScores = scrollbarScores.OrderBy(value => Guid.NewGuid()).ToList(); //隨機亂數洗牌 

            //int[] values = scrollbarScores.Select(value => 
            //    value.ScoreValue
            //).ToArray(); //

            //ScoreValue = scrollbarScores.Select(value=> value.ScoreValue).ToList();

            //ScoreValue = scrollbarScores.Select(value => value.ScoreValue).Where(value => value > 10).ToList(); //抓取Where裡面符合條件的 //不符合濾掉

            //for (int i = 0; i < scrollbarScores.Count; i++)
            //{

            //}

            #region 兩種Foreach

            //scrollbarScores.ForEach(value => 
            //    Debug.Log(value)
            //);

            //foreach (var value in scrollbarScores)
            //{
            //    Debug.Log(value);
            //}

            #endregion

        }
    }
#endif
}
