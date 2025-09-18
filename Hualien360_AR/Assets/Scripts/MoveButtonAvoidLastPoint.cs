/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveButtonAvoidLastPoint : MonoBehaviour
{
    public Button moveButton;  // 需要移動的按鈕
    public RectTransform buttonRectTransform;  // 按鈕的 RectTransform
    public List<Transform> movePoints;  // 存放所有可能移動的位置
    public float moveDuration = 1.0f; // 移動時間
    public float enableDelay = 0.5f; // 到達後的延遲時間

    private Transform lastPoint; // 記錄上一次的位置

    void Start()
    {
        //// 綁定按鈕點擊事件
        //if (moveButton != null)
        //{
        //    moveButton.onClick.AddListener(MoveButton);
        //}
    }

    public void MoveButton()
    {
        if (movePoints.Count == 0 || buttonRectTransform == null) return;

        Transform targetPoint;
        do
        {
            targetPoint = movePoints[Random.Range(0, movePoints.Count)];
        } while (targetPoint == lastPoint && movePoints.Count > 1); // 確保不重複

        // 記錄這次選擇的位置
        lastPoint = targetPoint;

        // 禁用按鈕點擊
        moveButton.interactable = false;

        // 使用 DOTween 移動按鈕到目標點
        buttonRectTransform.DOMove(targetPoint.position, moveDuration)
            .SetEase(Ease.OutQuad) // 平滑過渡
            .OnComplete(() =>
            {
                // 延遲 0.5 秒後恢復可點擊
                Invoke(nameof(EnableButton), enableDelay);
            });
    }

    void EnableButton()
    {
        moveButton.interactable = true;
    }
}

