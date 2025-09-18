using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class LvB_Energy : MonoBehaviour
{
    public int ScoreValue;
    public EnergyColor energyColor;
    [SerializeField]
    private Image energy_Img;
    [SerializeField]
    private TextMeshProUGUI energy_Text;
    string[] strColor = { "#59E1FF", "#FF4926", "#43CF44", "#ECED46", "#DE43FF" };

    [SerializeField]
    private BreathingScale Breathing_scale;

    public bool GoBreathing;
    public void InitScore(int value)
    {
        float targetValue = Mathf.InverseLerp(0f, 80f, value);
        ScoreValue = value;
        if (value >= 60)
        {
            energy_Text.text = $"<color={strColor[(int)energyColor]}>{ScoreValue.ToString()}</color>";
            GoBreathing=false;
        }
        else
        {
            energy_Text.text = ScoreValue.ToString();
            GoBreathing = true;
        }

        energy_Img.DOFillAmount(targetValue, 2f).SetEase(Ease.OutCubic); // 平滑變化，0.5秒內完成
    }
    public void BreathingSet_IO()
    {
        Breathing_scale.enabled = GoBreathing;
        Breathing_scale.Turn(GoBreathing);
    }

    public void ClearBreathing()
    {
        Breathing_scale.Turn(false);
    }
}
