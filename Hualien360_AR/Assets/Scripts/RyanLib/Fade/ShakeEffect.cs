/*************************************************
  * 名稱：ButtonShake
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using DG.Tweening;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [Header("Setting")]
    public float duration = 0.5f; // 震動持續時間
    public float strength = 5f; // 震動強度
    public int vibrato = 10; // 震動頻率
    public float randomness = 90f; // 隨機性

    bool shaking = false;
    Vector3 pos;

    private void OnEnable()
    {
        RectTransform rect = (RectTransform)transform;
        pos = rect.anchoredPosition;
    }

    private void OnDisable()
    {
        RectTransform rect = (RectTransform)transform;
        rect.anchoredPosition = pos;
    }

    public void TriggerShake()
    {
        if (shaking) return;

        // 取得按鈕的 RectTransform
        RectTransform rect = (RectTransform)transform;
        shaking = true;
        pos = rect.anchoredPosition;

        // 執行 DoShakePosition 動畫
        rect.DOShakePosition(
            duration, // 持續時間
            strength, // 強度
            vibrato,  // 震動次數
            randomness, // 隨機角度
            false,     // 是否淡出
            true       // 是否相對於本地位置
        )
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            shaking = false;
            rect.anchoredPosition = pos;
        });
    }

}
