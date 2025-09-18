/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ScrollbarScore : MonoBehaviour
{
    public int ScoreValue;
    public Text _scorllScore;
    public Text _PanelScore;

    public Scrollbar _scrollbar;
    private float _scoreValue;
    public EnergyColor energy_Color;

    string[] strColor = { "#59E1FF", "#FF4926", "#43CF44", "#ECED46", "#DE43FF" };

    private void Start()
    {
        Debug.Log($"[{name}] energy_Color: {energy_Color} ({(int)energy_Color})");
    }
    public void InitScoreText(int value)
    {
        ScoreValue = value; //回傳 初始化值
        _scorllScore.text = $"<color={strColor[(int)energy_Color]}>{ScoreValue.ToString()}</color>";
        _PanelScore.text = _scorllScore.text;
        Debug.Log("123");
        _scrollbar.value = Mathf.InverseLerp(0f,20f,value);
        Debug.Log(_scrollbar.value);
    }


  

    public void ScrollScore()
    {
        
        _scoreValue = Mathf.Round(_scrollbar.value * 10);
        ScoreValue = (int)_scoreValue*2;
        _scorllScore.text = $"<color={strColor[(int)energy_Color]}>{ScoreValue.ToString()}</color>";
        _PanelScore.text = _scorllScore.text;

    }
 

}
