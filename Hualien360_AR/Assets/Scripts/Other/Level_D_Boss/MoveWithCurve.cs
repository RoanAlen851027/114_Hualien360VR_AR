using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;

/********************************
---------------------------------
著作者：RoanAlen
用途：UI 使用 DOTween 做曲線移動
---------------------------------
*********************************/

public class MoveWithCurveUI : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float duration = 1f;
    public AnimationCurve curve;

    public UnityEvent OnInvokeEvent;
    public UnityEvent OnReachedDestination; // 觸發事件
    private RectTransform rectTransform;

    public WaitToOpenPanel openPanel;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        // 停止之前的動畫，防止干擾
        DOTween.Kill(rectTransform);

        // 設定初始位置
        rectTransform.anchoredPosition = startPosition;
        OnInvokeEvent?.Invoke();

        // 播放動畫
        rectTransform.DOAnchorPos(endPosition, duration)
            .SetEase(curve)
            .OnComplete(() =>
            {
                Debug.Log("到達定點！");
                OnReachedDestination?.Invoke();
                openPanel.WaitToOpen();
                // 可選：回到初始位置
                rectTransform.anchoredPosition = startPosition;
            });
    }


}
