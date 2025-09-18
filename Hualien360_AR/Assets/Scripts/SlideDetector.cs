/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlideDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startPos;
    public float swipeThreshold = 50f;

    [Header("滑動事件")]
    public UnityEvent onSwipeUp;
    public UnityEvent onSwipeDown;
    public UnityEvent onSwipeLeft;
    public UnityEvent onSwipeRight;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 endPos = eventData.position;
        Vector2 delta = endPos - startPos;

        if (delta.magnitude < swipeThreshold)
        {
            // 視為輕點 → 嘗試觸發底下的按鈕
            TryClickButton(eventData);
            return;
        }

        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            if (delta.y > 0)
                onSwipeUp?.Invoke();
            else
                onSwipeDown?.Invoke();
        }
        else
        {
            if (delta.x > 0)
                onSwipeRight?.Invoke();
            else
                onSwipeLeft?.Invoke();
        }
    }

    private void TryClickButton(PointerEventData data)
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = data.position;

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (var result in results)
        {
            var btn = result.gameObject.GetComponent<Button>();
            if (btn != null && btn.interactable)
            {
                btn.onClick.Invoke();
                break;
            }
        }
    }
}
