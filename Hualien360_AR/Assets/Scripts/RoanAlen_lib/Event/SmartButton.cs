/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SmartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPointerDown = false;
    private Vector2 _pressPosition;
    public float dragThreshold = 20f;

    public float scaleFactor = 0.9f;
    public float duration = 0.05f;
    private Vector3 originalScale;

    // 自訂點擊事件
    public UnityEvent onClick;

    private Coroutine coroutine;
    void Start()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        _pressPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isPointerDown) return;
        _isPointerDown = false;

        Vector2 releasePosition = eventData.position;
        float delta = Vector2.Distance(_pressPosition, releasePosition);

        if (delta < dragThreshold)
        {
            // ✅ 這裡才做縮放回彈或點擊特效
            coroutine = StartCoroutine(ClickFeedbackAnimation());

            if (onClick != null)
                onClick.Invoke();
        }
        else
        {
            // 滑動處理
            ExecuteEvents.ExecuteHierarchy<ISlideHandler>(
                gameObject,
                eventData,
                (handler, data) => handler.OnSlide(releasePosition - _pressPosition)
            );
        }
    }

    private IEnumerator ClickFeedbackAnimation()
    {
        transform.localScale = originalScale * scaleFactor;
        yield return new WaitForSeconds(duration);
        transform.localScale = originalScale;
    }
}

