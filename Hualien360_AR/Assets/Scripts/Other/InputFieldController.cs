/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InputFieldController : MonoBehaviour
{
    public InputField inputField;      // 原生 InputField
    public Button targetButton;        // 建立角色按鈕
    public TextMeshProUGUI warningText;           // 提示訊息文字

    // 髒話清單（可擴充）
    private List<string> bannedWords = new List<string>
    {
        "屁", "幹", "媽的", "fuck", "shit", "靠北", "87", "三小","你媽","操","幹你娘","白癡","垃圾","智障","窩囊廢","喜憨兒","雞掰","靠夭","靠","甲洨","大便","吃大便","腦癱","丁丁","腦殘","胖呆","北七",
        "ㄐㄐ","傻","蠢","idiot","婊子","神經病","敗類","屎","賤","尿","破麻","騷","娘泡","淫","人妖","白目","肏"
    };

    void Start()
    {
        inputField.onValueChanged.AddListener(OnTextChanged);
        UpdateButtonState(inputField.text);
    }

    void OnTextChanged(string text)
    {
        UpdateButtonState(text);

        // 即時警告使用者
        if (!IsValidName(text))
        {
            if (warningText != null)
                warningText.text = "名稱含有不雅詞語！請重新輸入";
        }
        else
        {
            if (warningText != null)
                warningText.text = "";
        }
    }

    void UpdateButtonState(string text)
    {
        // 名稱要非空 & 不含髒話才能啟用按鈕
        targetButton.interactable = !string.IsNullOrWhiteSpace(text) && IsValidName(text);
    }

    public bool IsValidName(string name)
    {
        string lowerName = name.ToLower();

        foreach (string word in bannedWords)
        {
            if (lowerName.Contains(word.ToLower()))
            {
                return false;
            }
        }

        return true;
    }

    public void OnCreateButtonClicked()
    {
        string inputName = inputField.text;

        if (IsValidName(inputName))
        {
            Debug.Log($"✅ 名稱合法，建立角色：{inputName}");
            // 執行創角邏輯
        }
        else
        {
            Debug.Log("❌ 名稱含有不當詞彙");
            if (warningText != null)
                warningText.text = "❌ 名稱中含有不當詞語，請重新輸入";
        }
    }
}