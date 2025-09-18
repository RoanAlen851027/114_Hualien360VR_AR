/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SlideResponder : MonoBehaviour, ISlideHandler
{

    public UnityEvent onSwipeLeft;
    public UnityEvent onSwipeRight;

    public void OnSlide(Vector2 delta)
    {
        // 範例：根據滑動方向做出反應
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                onSwipeRight?.Invoke();
                Debug.Log("SmartButton：滑動向右");

            }
            else
            {
                Debug.Log("SmartButton：滑動向左");
                onSwipeLeft?.Invoke();

            }
        }
        else
        {
            if (delta.y > 0)
                Debug.Log("SmartButton：滑動向上");
            else
                Debug.Log("SmartButton：滑動向下");
        }
    }
}

