/********************************
---------------------------------
著作者：RoanAlen
用途：能量條
---------------------------------
*********************************/
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class EnergyValue : MonoBehaviour
{
    public int ScoreValue;
    public EnergyColor energyColor;
    [SerializeField]
    private Image energy_Img;
    public TextMeshProUGUI energy_Value;
    string[] strColor = { "#59E1FF", "#FF4926", "#43CF44", "#ECED46", "#DE43FF" };

    public UnityEvent energy_Event;

    public void InitScore(int value)
    {
        ScoreValue = value;
        energy_Value.text = value.ToString();
        energy_Img.fillAmount = Mathf.InverseLerp(0f, 100f, value);
    }

    public void InitScoreColor(int value)
    {
        ScoreValue = value;
        energy_Value.text = $"<color={strColor[(int)energyColor]}>{ScoreValue.ToString()}</color>";
        energy_Img.fillAmount = Mathf.InverseLerp(0f, 100f, value);
    }

    public void InitScoreEightyColor(int value)
    {
        ScoreValue = value;
        energy_Value.text = $"<color={strColor[(int)energyColor]}>{ScoreValue.ToString()}</color>";
        energy_Img.fillAmount = Mathf.InverseLerp(0f, 80f, value);
    }

    public void InitLerpScoreEightyColor(int value)
    {
        ScoreValue = value;
        energy_Value.text = $"<color={strColor[(int)energyColor]}>{ScoreValue.ToString()}</color>";
        
        float targetValue = Mathf.InverseLerp(0f, 100f, value);
        energy_Img.DOFillAmount(targetValue, 1f).SetEase(Ease.OutCubic);
    }
    public void Energy_Event()
    {
        if (ScoreValue < 60)
        {
            energy_Event?.Invoke();
        }
    }
}
