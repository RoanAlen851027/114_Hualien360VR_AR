/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AutoScrollWithDotween : MonoBehaviour, IPointerDownHandler
{
    public ScrollRect scrollRect;
    public float scrollTime = 2f; // 滾動到底部所需時間
    public float DelayStart;
    private bool isScrolling = true;
    private Tweener scrollTween;

    public Button Clicked_BTN;
    public bool IsBottom;



    void Start()
    {
        StartScrolling();
        Clicked_BTN.interactable = false;
        // 加入滑動監聽事件
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    void StartScrolling()
    {
        // 目標位置：0 表示滾動到底部
        float targetPos = 0f;

        // 使用 DOTween.To 動畫來設定 ScrollRect 的 verticalNormalizedPosition
        scrollTween = DOTween.To(
            () => scrollRect.verticalNormalizedPosition,
            x => scrollRect.verticalNormalizedPosition = x,
            targetPos,
            scrollTime
        )
        .SetEase(Ease.Linear)
        .SetDelay(DelayStart)  // 延遲 5 秒後開始滾動
        .OnComplete(() => isScrolling = false);
    }



    // 當點擊 ScrollView 內部時，停止滾動
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isScrolling)
        {
            scrollTween.Kill(); // 停止 DoTween 動畫
            isScrolling = false;
        }
    }
    void OnScrollValueChanged(Vector2 scrollPos)
    {
        // scrollPos.y：垂直方向（1是頂部，0是底部）
        if (scrollRect.verticalNormalizedPosition <= 0.01f && IsBottom==false)
        {
            Debug.Log("已經滑到底部！");

            Clicked_BTN.interactable = true;
            IsBottom = true;
        }
    }
}
