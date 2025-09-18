/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
public class AlenDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;

    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition; // 紀錄原始位置

    public Transform originalTransform;
    public string TheQuestionAnswer;
    Coroutine coroutine;

    public ScaleFadeIn scale_FadeIn;
    public ScaleFadeOut scale_FadeOut;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition; // 初始化時記錄位置
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false; // 讓 Raycast 可以穿過，方便判斷是否進入目標區
        canvasGroup.alpha = 0.6f; // 讓 UI 透明一點，表明正在拖動
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true; // 讓 Raycast 正常運作
        canvasGroup.alpha = 1f; // 恢復原狀

        // 檢查是否有被放置到 Drop 區域
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<AlenDrop>() == null)
        {
            rectTransform.anchoredPosition = originalPosition; // 如果沒成功放置，回到原位
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

    }

    // 回到原本位置的函式
    public void ResetPosition()
    {
        this.gameObject.transform.SetParent(originalTransform);
        rectTransform.anchoredPosition = originalPosition;
    }

    //答對
    public void GetCurrect()
    {
        coroutine = StartCoroutine(GetCurrectAns());
    }

    IEnumerator GetCurrectAns()
    {
        scale_FadeOut.FadeOut();
        canvasGroup.blocksRaycasts = false; // 讓 Raycast 可以穿過，方便判斷是否進入目標區
        yield return new WaitForSeconds(1f);
        canvasGroup.blocksRaycasts = true; // 讓 Raycast 可以穿過，方便判斷是否進入目標區
        ResetPosition();
        scale_FadeIn.FadeIn();
        coroutine = null;
    }

}
