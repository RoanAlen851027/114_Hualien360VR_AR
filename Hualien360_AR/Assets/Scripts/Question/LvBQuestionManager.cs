using UnityEngine;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/

using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
public class LvBQuestionManager : BaseQuestionManager
{

    public EnergyColor energyColor;

    public QuestionBase currentQuestion; // 當前情境題
    public int currentSubQuestionIndex = 0; // 當前回答的子題索引
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public UnityEvent OnEnableEvent;

    public UnityEvent OnDisableEvent;


    [SerializeField]
    private TextMeshProUGUI QuestionCount_Text;
    public int QuestionCount;
    public int CurrectAns;

    public UnityEvent QuestionEvent;

    public List<GameObject> AlienShow;

    [SerializeField]
    private TextMeshProUGUI QuestionGroupTitle;
    [SerializeField]
    private TextMeshProUGUI QuestionGruopContent;//題組名稱
    

    [SerializeField]
    private TypingEffect QuestionTypeEffect;
    public TextMeshProUGUI QuestionText;

    public Text SelectA;
    public Text SelectB;
    public Text SelectC;

    public UnityEvent CurrentEvent;
    public UnityEvent FailedEvent;


    public UnityEvent FinishedEvent;

    [SerializeField]
    private TextMeshProUGUI addScore_text;
    [SerializeField]
    private UnityEvent addScore_Event;

    [SerializeField]
    private UnityEvent NotNextEvent;

    protected override void Start()
    {
        //base.Start();  //修改題目 打開去撈取
    }
    private void OnEnable()
    {
        // 如果 queue 已經有題目，則不再重複載入
        if (questionQueue.Count == 0)
        {
            LoadFromSO();  // 從 SO 載入題目
        }

        // 僅取出第一個情境題
        if (questionQueue.Count > 0)
        {
            QuestionBase firstQuestion = questionQueue.Dequeue(); // 取得第一題
            currentQuestion = firstQuestion;
            currentSubQuestionIndex = 0;  // ✅重置子題索引
                                          //Debug.Log("情境題: " + currentQuestion.QuestionName);        }
            QuestionCount = currentSubQuestionIndex + 1;
            QuestionCount_Text.text = $"<size=60>{QuestionCount}</size>/ 3";
            OnEnableEvent?.Invoke();

            CurrectAns = 0;
            switch (CurrectAns)
            {
                case 0:
                    for (int i = 0; i < AlienShow.Count; i++)
                    {
                        AlienShow[i].SetActive(false);
                    }
                    break;
            }
        }
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
    }

    public void ClearText()
    {
        QuestionGruopContent.text = "";
        QuestionGroupTitle.text = "";
    }
    public void GetQuestionGroupContent()
    {
        QuestionGroupTitle.text = currentQuestion.GroupName;
        QuestionGruopContent.text = currentQuestion.QuestionName;
    }


    public void GetQuestion()
    {
        //// 如果 queue 已經有題目，則不再重複載入
        //if (questionQueue.Count == 0)
        //{
        //    LoadFromSO();  // 從 SO 載入題目
        //}

        //// 僅取出第一個情境題
        //if (questionQueue.Count > 0)
        //{
        //    //QuestionBase firstQuestion = questionQueue.Dequeue(); // 取得第一題
        //    Debug.Log("第一題: " + firstQuestion.QuestionName);
        //    currentQuestion = firstQuestion;
        //    // 假設第一題有四個題組，取出第一個題組

        //}

        // 取得該題的子題
        if (currentQuestion.SubQuestions != null && currentQuestion.SubQuestions.Count > 0)
        {
            DisplaySubQuestion(currentQuestion.SubQuestions[currentSubQuestionIndex]); // 顯示第一個子題


        }
        else
        {
            Debug.Log("這個情境題沒有子題");
        }

        //else
        //{
        //    Debug.LogWarning("題庫為空，無法加載題目");
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AnswerSubQuestion(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AnswerSubQuestion(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AnswerSubQuestion(2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetQuestion();
        }
    }

    // 顯示當前子題
    private void DisplaySubQuestion(LittleQuestion subQuestion)
    {
        //Debug.Log("顯示子題: " + subQuestion.QuestionName);
        // 在這裡，你可以顯示 UI 或其他交互來讓玩家回答
        //QuestionTypeEffect.StartTypingEffectOnly(subQuestion.QuestionName);
        //SelectA.text = subQuestion.Answers[0].AnswerName;
        //SelectB.text = subQuestion.Answers[1].AnswerName;
        //SelectC.text = subQuestion.Answers[2].AnswerName;

        // ✅ 複製原本的答案列表
        List<Answer> shuffledAnswers = new List<Answer>(subQuestion.Answers);

        // ✅ 洗牌
        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledAnswers.Count);
            Answer temp = shuffledAnswers[i];
            shuffledAnswers[i] = shuffledAnswers[randomIndex];
            shuffledAnswers[randomIndex] = temp;
        }

        // ✅ 顯示 UI 選項
        QuestionTypeEffect.StartTypingEffectOnly(subQuestion.QuestionName);
        SelectA.text = shuffledAnswers[0].AnswerName;
        SelectB.text = shuffledAnswers[1].AnswerName;
        SelectC.text = shuffledAnswers[2].AnswerName;

        // ✅ 為了讓 AnswerSubQuestion 正確比對答案，需要記住當前順序
        subQuestion.Answers = shuffledAnswers;

    }

    // 玩家回答了當前子題
    public void AnswerSubQuestion(int answerIndex)
    {
        if (currentQuestion != null && currentQuestion.SubQuestions != null && currentSubQuestionIndex < currentQuestion.SubQuestions.Count)
        {
            LittleQuestion currentSubQuestion = currentQuestion.SubQuestions[currentSubQuestionIndex];
            Answer playerAnswer = currentSubQuestion.Answers[answerIndex];
            //Debug.Log("玩家回答: " + playerAnswer.AnswerName);

            currentSubQuestionIndex++; // 移動到下一個子題
            QuestionCount = currentSubQuestionIndex+1;

            if (QuestionCount<4)
            {
                QuestionCount_Text.text = $"<size=60>{QuestionCount}</size>/ 3";
            }

            if (playerAnswer.AnswerCount==1)
            {
                CurrectAns++;
                CurrentEvent?.Invoke();
                Debug.LogError("你竟然答對了");
            }
            else
            {
                FailedEvent?.Invoke();
            }

            // 檢查是否回答完所有子題
            if (currentSubQuestionIndex >= currentQuestion.SubQuestions.Count)
            {
                // 所有子題已經回答完畢，呼叫 FinishedQuestion
                FinishedQuestion();
            }
            else
            {
                // 顯示下一個子題
                DisplaySubQuestion(currentQuestion.SubQuestions[currentSubQuestionIndex]);
            }
            //移動
            QuestionEvent.Invoke();
        }
    }

    // 顯示題目回答完畢的結果
    private void FinishedQuestion()
    {
        //Debug.Log("題目回答完畢，看看結果吧!");
        FinishedEvent?.Invoke();
        // 在這裡，可以顯示總結結果，或進行其他操作
    }


    public void QuestionEventShow()
    {
        //Debug.Log("每次回答進入這個事件");

        switch (CurrectAns)
        {
            case 0:
                for (int i = 0; i < AlienShow.Count; i++)
                {
                    AlienShow[i].SetActive(false);
                }
                break;
            case 1:
                AlienShow[0].SetActive(true);
                break;
            case 2:
                AlienShow[1].SetActive(true);
                break;
            case 3:
                AlienShow[2].SetActive(true);
                break;
        }
    }

    
    public void AddToResultScore()
    {
        switch (energyColor)
        {
            case EnergyColor.Blue:
                AddScore(0);
                break;
            case EnergyColor.Red:
                AddScore(1);
                break;
            case EnergyColor.Green:
                AddScore(2);
                break;
            case EnergyColor.Yellow:
                AddScore(3);
                break;
            case EnergyColor.Purple:
                AddScore(4);
                break;

        }
    }

    public void AddScore(int score)
    {

        if (PAC_Score.instance.All_End == true)
        {

        }
        else
        {
            int addedScore = 0; // 記錄實際增加的分數

            switch (CurrectAns)
            {
                case 0:
                    addedScore = 0;
                    break;
                case 1:
                    addedScore = 5;
                    break;
                case 2:
                    addedScore = 10;
                    break;
                case 3:
                    addedScore = 15;
                    break;
            }

            // 計算最終得分，確保不會超過 60
            int newScore = PAC_Score.instance.Reslut_score[score] + addedScore;
            if (newScore >= 60)
            {
                addedScore = 60 - PAC_Score.instance.Reslut_score[score]; // 修正加分數值
                newScore = 60; // 限制最高分
                if (addedScore>0 && addedScore<15)
                {
                    addScore_Event?.Invoke();
                }
                NotNextEvent?.Invoke();
            }
            else 
            {
                if (CurrectAns > 0)
                {
                    addScore_Event?.Invoke();
                }
            }

            // 更新分數與 UI
            PAC_Score.instance.Reslut_score[score] = newScore;
            addScore_text.text = $"+{addedScore}";
        }

       
    }

}
