/********************************
---------------------------------
著作者：RoanAlen
用途：問答結構
---------------------------------
*********************************/

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/QuestionDataSO", order = 1)]
public class QuestionDataSO : ScriptableObject
{
    public List<QuestionBase> Questions;
}

[System.Serializable]
public class QuestionDataContainer  // **這個類只用來解析 JSON**
{
    public List<QuestionBase> Questions;
}

[System.Serializable]
public class QuestionBase
{
    public string QuestionId;
    public string QuestionName;
    public string QuestionType;
    public List<Answer> Answers;
    public string GroupId;
    public string GroupName;
    public List<LittleQuestion> SubQuestions ; // 題組內的子題
}

[System.Serializable]
public class LittleQuestion
{
    public string QuestionId;
    public string QuestionName;
    public string QuestionType;
    public List<Answer> Answers;
    public string GroupId;
    public string GroupName;
}

[System.Serializable]
public class Answer
{
    public string AnswerName;
    public int AnswerCount;
}