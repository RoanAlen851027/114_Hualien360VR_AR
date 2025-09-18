/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class BaseQuestionManager : MonoBehaviour
{

    [Header("開啟去讀JSON")]
    public bool GoToGetJSON_Data;
    [Space(10)]
    public QuestionDataSO questionDataSO; // SO 存題目的地方
    public string jsonFile;
    public bool isRandom = true; // 是否隨機抽題
    public Queue<QuestionBase> questionQueue = new Queue<QuestionBase>(); // 題目隊列

    protected virtual void Start()
    {
//#if UNITY_EDITOR
//#else
        //StartCoroutine(LoadQuestions()); // 只有在編輯器模式下會讀取 JSON
//#endif
        if (GoToGetJSON_Data==true)
        {
            StartCoroutine(LoadQuestions());
        }
        else
        {
            LoadFromSO(); // 在運行時直接從 SO 加載
        }
    }

    // 讀取 JSON 檔案
    protected IEnumerator LoadQuestions()
    {
        string jsonPath = GetJsonPath();

        using (UnityWebRequest request = UnityWebRequest.Get(jsonPath))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("讀取 JSON 失敗：" + request.error);
            }
            else
            {
                string jsonText = request.downloadHandler.text;
                Debug.Log("讀取到的 JSON：" + jsonText);

                QuestionDataContainer tempData = JsonUtility.FromJson<QuestionDataContainer>(jsonText);

                if (tempData != null && tempData.Questions != null)
                {
                    if (isRandom)
                    {
                        ShuffleList(tempData.Questions); // 如果是隨機模式，先洗牌
                    }

                    foreach (var question in tempData.Questions)
                    {
                        questionQueue.Enqueue(question); // 放入隊列
                        Debug.Log("已加入題目：" + question.QuestionName); // 打印題目名稱確認
                    }

                    // 也可以選擇將題目放進 SO，以便於後續其他操作
                    questionDataSO.Questions = new List<QuestionBase>(tempData.Questions);
                    Debug.Log("成功載入題目數：" + questionDataSO.Questions.Count);
                }
                else
                {
                    Debug.LogError("JSON 解析失敗，請檢查格式");
                }
            }
        }
    }
    // 從 SO 加載題目
    protected void LoadFromSO()
    {
        //if (questionDataSO != null) return;
        //if (isRandom)
        //{
        //    ShuffleList(questionDataSO.Questions); // 如果是隨機模式，先洗牌
        //}

        //questionQueue = new Queue<QuestionBase>(questionDataSO.Questions);






        if (questionDataSO != null && questionDataSO.Questions.Count > 0)
        {
            if (isRandom)
            {
                ShuffleList(questionDataSO.Questions); // 如果是隨機模式，先洗牌
            }

            foreach (var question in questionDataSO.Questions)
            {
                questionQueue.Enqueue(question); // 從 SO 加入題目到隊列
            }
            Debug.Log("從 SO 載入題目數：" + questionQueue.Count);
        }
        else
        {
            Debug.LogError("SO 中沒有題目！");
        }
    }

    // 根據平台選擇合適的 JSON 路徑
    protected string GetJsonPath()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return Path.Combine(Application.streamingAssetsPath, jsonFile);
        }
        else
        {
            return "file://" + Path.Combine(Application.streamingAssetsPath, jsonFile);
        }
    }

    // Fisher-Yates 隨機排序
    protected void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]); // 交換
        }
    }

}
