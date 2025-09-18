/********************************
---------------------------------
著作者：RoanAlen
用途：心理測驗管理
---------------------------------
*********************************/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PersonalityTestManager : BaseQuestionManager
{
    public UnityEvent OnTestEnd; // **事件：當測驗結束時通知**

    private Dictionary<string, int> emotionScores = new Dictionary<string, int>(); // 存放情緒分數
    public Button buttonO; // O 按鈕
    public Button buttonDelta; // Δ 按鈕
    public Button buttonX; // X 按鈕
    public TextMeshProUGUI questionText;
    public QuestionDataSO QuestionData;
    private QuestionBase currentQuestion; // 當前題目
    public bool IsEnd;
    public TypingEffect typing;
    private int QuestionInt;
    public TextMeshProUGUI QuestionIndex;

    //public TextBounceEffect bounceEffect;
    protected override void Start()
    {
        base.Start(); // 這裡會先調用 BaseQuestionManager 的 Start()，進行題目加載
        //if (QuestionData == null)
        //{
        //    Debug.LogError("QuestionData 沒有設置！");
        //}
        //else if (QuestionData.Questions.Count == 0)
        //{
        //    Debug.LogError("QuestionData 中沒有題目！");
        //}
        StartCoroutine(WaitForStart());
    }


    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1f);
        // 確保按鈕綁定了對應的事件處理方法
        buttonO.onClick.AddListener(() => SelectAnswer("O"));
        buttonDelta.onClick.AddListener(() => SelectAnswer("Δ"));
        buttonX.onClick.AddListener(() => SelectAnswer("X"));

        // 在這裡添加Debug檢查隊列的題目數量
        Debug.Log($"題目隊列大小: {questionQueue.Count}");

        // 當 LoadQuestions() 完成後初始化情緒分數
        StartCoroutine(InitializeEmotionScores());
    }
    // 確保 InitializeEmotionScores 在題目加載完後才執行
    private IEnumerator InitializeEmotionScores()
    {
        // 等待題目加載完成，並檢查題目數量是否正確
        yield return new WaitUntil(() => questionQueue.Count >= 25);

        // 初始化情緒分數
        emotionScores["CP"] = 0;
        emotionScores["NP"] = 0;
        emotionScores["A"] = 0;
        emotionScores["FC"] = 0;
        emotionScores["AC"] = 0;

        // 顯示第一題
        ShowNextQuestion();
    }

    public void SelectAnswer(string choice)
    {
        if (IsEnd != true)
        {
            // 根據選擇設定分數
            int score = 0;
            switch (choice)
            {
                case "O": score = 4; break;
                case "Δ": score = 2; break;
                case "X": score = 0; break;
            }

            if (currentQuestion != null && emotionScores.ContainsKey(currentQuestion.GroupName))
            {
                Debug.Log($"currentQuestion: {currentQuestion}, emotionScores.ContainsKey: {emotionScores.ContainsKey(currentQuestion.GroupName)}");
                emotionScores[currentQuestion.GroupName] += score;
                Debug.Log($"選擇 {choice}，{currentQuestion.GroupName} 分數變為 {emotionScores[currentQuestion.GroupName]}");
            }
        


            // 檢查 currentQuestion 是否為 null
            if (currentQuestion == null)
            {
                Debug.Log("currentQuestion 為 null，請檢查它是否有被正確賦值！");
            }
            else
            {
                Debug.Log($"currentQuestion 已初始化，GroupName: {currentQuestion.GroupName}");
            }

            // 檢查 emotionScores 是否為 null
            if (emotionScores == null)
            {
                Debug.Log("emotionScores 為 null，請確認它是否有被正確初始化！");
            }
            else
            {
                // 列出 emotionScores 中的所有鍵值
                Debug.Log("emotionScores 內容如下：");
                foreach (var pair in emotionScores)
                {
                    Debug.Log($"Key: {pair.Key}, Value: {pair.Value}");
                }
            }

            // 檢查 GroupName 是否存在於 emotionScores
            if (currentQuestion != null && emotionScores != null)
            {
                string groupName = currentQuestion.GroupName;
                if (!emotionScores.ContainsKey(groupName))
                {
                    Debug.Log($"emotionScores 不包含 {groupName}，目前已有的鍵有：{string.Join(", ", emotionScores.Keys)}");
                }
                else
                {
                    Debug.Log($"emotionScores 包含 {groupName}，目前值為 {emotionScores[groupName]}");
                }
            }



        }

        // 如果所有題目已經答完，顯示結果
        if (questionQueue.Count == 0)
        {
            ShowFinalResult();  // 顯示結果並結束
            IsEnd = true;
            return;
        }

        // 移除當前題目並顯示下一題))
        ShowNextQuestion();
    }



    private void ShowNextQuestion()
    {
        ////檢查題目隊列中是否還有題目，如果隊列為空，從 SO 中取得題目
        //if (questionQueue.Count == 0 && QuestionData != null && QuestionData.Questions.Count > 0)
        //{
        //    foreach (var question in QuestionData.Questions)
        //    {
        //        questionQueue.Enqueue(question); // 從 SO 加入題目到隊列
        //    }
        //}

        // 繼續進行題目顯示
        if (questionQueue.Count > 0)
        {
            currentQuestion = questionQueue.Dequeue(); // 取得下一題並更新 currentQuestion
            questionText.text = currentQuestion.QuestionName; // 顯示題目
            typing.StartTypingEffectOnly(questionText.text);
            QuestionInt++;
            QuestionIndex.text = $"<color=#10E88E>{QuestionInt.ToString()} <size=60>/ 25</color>";
        }
        else
        {
            Debug.Log("所有題目已完成！");
            ShowFinalResult();  // 如果沒有題目了，顯示結果
        }
    }

    void ShowFinalResult()
    {
        Debug.Log("心理測驗結果：");
        QuestionIndex.text = $"<color=#10E88E><size=80>測驗結束</color>";

        // 定義對應表：將 emotionScores 的 Key 對應到 PAC_score 的索引
        Dictionary<string, int> emotionIndexMap = new Dictionary<string, int>
    {
        { "CP", 0 },  // 藍色
        { "NP", 1 },  // 紅色
        { "A",  2 },  // 綠色
        { "FC", 3 },   // 黃色
        { "AC", 4 },  // 紫色
    };

        foreach (var pair in emotionScores)
        {
            Debug.Log($"{pair.Key}：{pair.Value} 分");

            // 更新 PAC_Score
            if (emotionIndexMap.ContainsKey(pair.Key))
            {
                int index = emotionIndexMap[pair.Key];
                PAC_Score.instance.PAC_score[index] = pair.Value;
            }
        }
        //Invoke(nameof(WaitInvokeEvent), 0.5f);
        OnTestEnd?.Invoke(); // **測驗結束時，觸發事件**
        Debug.Log("分數已更新到 PAC_Score！");
    }


    // 找出最高得分的情緒
    void AnalyzeResult()
    {
        string highestEmotion = "";
        int maxScore = 0;

        foreach (var pair in emotionScores)
        {
            if (pair.Value > maxScore)
            {
                maxScore = pair.Value;
                highestEmotion = pair.Key;
            }
        }

        Debug.Log($"你的主要情緒類型是：{highestEmotion}，分數：{maxScore}");
    }
}
