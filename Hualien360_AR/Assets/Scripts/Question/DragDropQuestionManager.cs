using UnityEngine;

using TMPro;

/********************************
---------------------------------
著作者：RoanAlen
用途：管理拖放題目的邏輯
---------------------------------
*********************************/
public class DragDropQuestionManager : BaseQuestionManager
{
    public AlenDrag alenDrag;
    public TextMeshProUGUI questionText;
    private QuestionBase currentQuestion;
    public TypingEffect typing;

    public ScaleFadeOut BackBTN_FadeOut;
    public ScaleFadeOut GemContent_FadeOut;

    public GameObject EndPanel;

    public GASender game_Complete;

    protected override void Start()
    {
        base.Start();
        LoadNextQuestion(); // 載入第一題
    }


    public void LoadNextQuestion()
    {
        if (questionQueue.Count > 0)
        {
            currentQuestion = questionQueue.Dequeue(); // 取出下一題
            questionText.text = currentQuestion.QuestionName; // 顯示題目
            typing.StartTypingEffectOnly(questionText.text);
            alenDrag.TheQuestionAnswer = currentQuestion.Answers[0].AnswerName;
            ShowQuestion(currentQuestion);
        }
        else
        {
            questionText.text = "";
            alenDrag.TheQuestionAnswer = "";
            BackBTN_FadeOut.FadeOut();
            GemContent_FadeOut.FadeOut();
            EndPanel.SetActive(true);
            game_Complete.SendGA();
            Debug.Log("所有題目都完成了！");
        }
    }
    private void ShowQuestion(QuestionBase question)
    {
        Debug.Log("目前題目：" + question.QuestionName);
    }
}

