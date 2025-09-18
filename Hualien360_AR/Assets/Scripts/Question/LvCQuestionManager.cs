using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/

public class LvCQuestionManager : BaseQuestionManager
{


    public List<QuestionBase> selectedQuestions;
    public List<QuestionBase> errorQuestions;  // 錯誤題庫
    
    
    public QuestionBase currentQuestion;

    public List<QuestionBase> questionList;
    public List<Image> AlienBars;
    public List<Image> AlienBars_Alpha;

    [Header("當前題目")]
    public TextMeshProUGUI questionText; // 顯示問題的文本
    public TypingEffect question_Effect;
    public EnergyColor NowQuestionType;
    // 狀態列表，index 對應 EnergyColor 的順序
    public List<int> energyFillStates = new List<int>() { 0, 0, 0, 0, 0 };
    public List<CanvasGroup> alienCanvasGroups;
    public List<CanvasGroup> alienBar_CanvasGroups;


    public SpawnAfterParticle SpawnParticle;
    [Header("選項")]
    public Button btnA; // 按鈕 A
    public TextMeshProUGUI btnA_Text;

    public Button btnB; // 按鈕 B
    public TextMeshProUGUI btnB_Text;

    [Header("答對事件")]
    public UnityEvent CorrectFeedbackPanel;
    [Header("答錯事件")]
    public MoveFadeBoth_Tf QuestionPanel;
    public UnityEvent ErrorFeedbackPanel;
    public TextMeshProUGUI feedback_Text;
    public TypingEffect feedback_Effect;

    [Header("結束")]
    public List<ScaleFadeOut> AliensFadeOut;
    public UnityEvent EndEvent;
    protected override void Start()
    {
        base.Start();
        //errorQuestions = new List<QuestionBase>();  // 初始化錯誤題庫
        StartCoroutine(SelectQuestions());

        // 設置按鈕點擊事件
        btnA.onClick.AddListener(() => OnAnswerSelected(btnA));
        btnB.onClick.AddListener(() => OnAnswerSelected(btnB));

        for (int i = 0; i < AlienBars.Count; i++)
        {
            AlienBars[i].fillAmount = 0;
            AlienBars_Alpha[i].fillAmount = 0;
        }
    }

    private IEnumerator SelectQuestions()
    {
        yield return new WaitUntil(() => questionQueue.Count > 0);

        selectedQuestions = new List<QuestionBase>();

        selectedQuestions.AddRange(SelectRangeQuestions(1, 4, 2));
        selectedQuestions.AddRange(SelectRangeQuestions(5, 8, 2));
        selectedQuestions.AddRange(SelectRangeQuestions(9, 12, 2));
        selectedQuestions.AddRange(SelectRangeQuestions(13, 16, 2));
        selectedQuestions.AddRange(SelectRangeQuestions(17, 20, 2));

        if (selectedQuestions.Count == 10)
        {
            //questionQueue.Clear();
            //foreach (var question in selectedQuestions)
            //{
            //    questionQueue.Enqueue(question);
            //}

            // 然後再打亂這些題目
            questionList = selectedQuestions;
            ShuffleQueue();  // ShuffleQueue 只需要打亂隊列中的題目

            //// 顯示第一題
            //ShowNextQuestion();
            //ListAllQuestionIds();
        }
        else
        {
            Debug.LogError("選擇題目數量錯誤，應該選擇10題！");
        }
    }
    public void InTypeText()
    {
        questionText.text = "";
        //feedback_Text.text = "";

        if (selectedQuestions.Count > 0 ||errorQuestions.Count>0)
        {
            question_Effect.StartTypingEffectOnly(currentQuestion.QuestionName);
        }

    }

    public void OutNoText()
    {
        questionText.text = "";


        //if (selectedQuestions.Count > 0 || errorQuestions.Count > 0)
        //{
        //    feedback_Effect.StartTypingEffectOnly();
        //}

    }

    public void UpdateAlienFade()
    {
        for (int i = 0; i < alienCanvasGroups.Count; i++)
        {
            if (i == (int)NowQuestionType)
            {
                // 淡入
                //question_Effect.StartTypingEffectOnly(questionText.text);
                alienCanvasGroups[i].DOFade(0f, 0.5f).SetEase(Ease.OutQuad);
                alienBar_CanvasGroups[i].DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
            }
            else
            {
                // 淡出
                alienCanvasGroups[i].DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
                alienBar_CanvasGroups[i].DOFade(0f, 0.5f).SetEase(Ease.OutQuad);
            }
        }
    }
    private void ShowNextQuestion()
    {

         if (selectedQuestions.Count>0) // 當總題庫已空，開始顯示錯誤題目
        {
          
            currentQuestion = selectedQuestions[0];  // 顯示第一個錯誤題目
            //question_Effect.StartTypingEffectOnly(currentQuestion.QuestionName);
            //questionText.text = $"{currentQuestion.QuestionName}"; // 顯示問題
            switch (currentQuestion.QuestionType)
            {
                case "CP":
                    NowQuestionType = EnergyColor.Blue;
                    break;
                case "NP":
                    NowQuestionType = EnergyColor.Red;
                    break;
                case "A":
                    NowQuestionType = EnergyColor.Green;
                    break;
                case "AC":
                    NowQuestionType = EnergyColor.Purple;
                    break;
                case "FC":
                    NowQuestionType = EnergyColor.Yellow;
                    break;
            }
            // 顯示錯誤題目的選項
            UpdateAlienFade();
            btnA_Text.text = currentQuestion.Answers[0].AnswerName;
            btnB_Text.text = currentQuestion.Answers[1].AnswerName;

            //    // 在錯誤題目回答後移除，然後顯示下一題
            //    //errorQuestions.RemoveAt(0); // 移除已顯示的錯誤題目
        }
        else if(errorQuestions.Count>0)
        {
            selectedQuestions = errorQuestions;
            //question_Effect.StartTypingEffectOnly(currentQuestion.QuestionName);
            errorQuestions = new List<QuestionBase>();
            currentQuestion = selectedQuestions[0];  // 顯示第一個錯誤題目
            //questionText.text = $"{currentQuestion.QuestionName}"; // 顯示問題
            switch (currentQuestion.QuestionType)
            {
                case "CP":
                    NowQuestionType = EnergyColor.Blue;
                    break;
                case "NP":
                    NowQuestionType = EnergyColor.Red;
                    break;
                case "A":
                    NowQuestionType = EnergyColor.Green;
                    break;
                case "AC":
                    NowQuestionType = EnergyColor.Purple;
                    break;
                case "FC":
                    NowQuestionType = EnergyColor.Yellow;
                    break;
            }
            // 顯示錯誤題目的選項
            UpdateAlienFade();
            btnA_Text.text = currentQuestion.Answers[0].AnswerName;
            btnB_Text.text = currentQuestion.Answers[1].AnswerName;
        }
        else  // 如果總題庫和錯誤題庫都空了，結束遊戲
        {
            EndEvent?.Invoke();
            btnA_Text.text = "";
            btnB_Text.text = "";
            questionText.text = "";
            for (int i = 0; i < AliensFadeOut.Count; i++)
            {
                AliensFadeOut[i].FadeOut();
            }
            Debug.Log("遊戲結束！所有題目已答完。");
        }

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GetNextQuestion();
        }
    }
    public void FailedAns()
    {
        // 錯誤答案，加入錯誤題庫
        ErrorFeedbackPanel?.Invoke();
        feedback_Text.text = currentQuestion.GroupName;
        //Debug.Log("XXX通道");

    }

    public void CurrentAns()
    {
        CorrectFeedbackPanel?.Invoke();
        //SpawnParticle.ShowCorrect((int)NowQuestionType);

        switch (NowQuestionType)
        {
            case EnergyColor.Blue:
                SpawnParticle.ShowCorrect(0);
                energyFillStates[0]++;
                AnimateAlienBar(0, energyFillStates[0]);
                break;
            case EnergyColor.Red:
                SpawnParticle.ShowCorrect(1);
                energyFillStates[1]++;
                AnimateAlienBar(1, energyFillStates[1]);
                break;
            case EnergyColor.Green:
                SpawnParticle.ShowCorrect(2); 
                energyFillStates[2]++;
                AnimateAlienBar(2, energyFillStates[2]);
                break;
            case EnergyColor.Yellow:
                SpawnParticle.ShowCorrect(3);
                energyFillStates[3]++;
                AnimateAlienBar(3, energyFillStates[3]);
                break;
            case EnergyColor.Purple:
                SpawnParticle.ShowCorrect(4);
                energyFillStates[4]++;
                AnimateAlienBar(4, energyFillStates[4]);
                break;
    
        }
        //Debug.Log("OOO通道");
    }
    private void AnimateAlienBar(int energy,int index)
    {
        if (index >= 3)
            return;

        float targetFill = 0f;

        switch (index)
        {
            case 1:
                targetFill = 0.5f;
                break;
            case 2:
                targetFill = 1f;
                break;
            default:
                return; // 已經兩次就不再增加
        }


        AlienBars[energy].DOFillAmount(targetFill, 0.5f);
        AlienBars_Alpha[energy].DOFillAmount(targetFill, 0.5f);
    }

    public void GetNextQuestion()
    {
        // 繼續提問錯誤題目
        ShowNextQuestion();

    }

    private void OnAnswerSelected(Button selectedButton)
    {
        // 根據按鈕選擇的答案，更新對應的選項
        Answer selectedAnswer = null;
        if (selectedButton == btnA)
        {
            selectedAnswer = currentQuestion.Answers[0];
        }
        else if (selectedButton == btnB)
        {
            selectedAnswer = currentQuestion.Answers[1];
        }

        if (selectedAnswer != null)
        {
            // 檢查答案是否正確
            //答錯通道
            if (selectedAnswer.AnswerCount == 0)
            {
                if (!errorQuestions.Contains(currentQuestion))
                {
                    errorQuestions.Add(currentQuestion);
                }
                QuestionPanel.FadeOut();
                FailedAns();
            }
            else
            {
                CurrentAns();
            }

            //Debug.Log($"剩下題目：{questionQueue.Count}，錯誤題庫：{Count}");
            // 顯示下一題
            ShowNextQuestion();
        }
        selectedQuestions.Remove(currentQuestion);
    }

    private List<QuestionBase> SelectRangeQuestions(int start, int end, int count)
    {
        List<QuestionBase> rangeQuestions = new List<QuestionBase>();

        foreach (var question in questionQueue)
        {
            int questionIndex = int.Parse(question.QuestionId);
            if (questionIndex >= start && questionIndex <= end)
            {
                rangeQuestions.Add(question);
            }
        }

        if (rangeQuestions.Count > count)
        {
            ShuffleList(rangeQuestions);
            rangeQuestions = rangeQuestions.Take(count).ToList();
        }

        return rangeQuestions;
    }

    private void ShuffleQueue()
    {
        ShuffleList(selectedQuestions);  // 打亂順序
    }

    protected new void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}

