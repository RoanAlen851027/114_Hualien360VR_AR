/*************************************************
  * 名稱：Draggable
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public Action<bool> OnDragged;

    [Header("Setting")]
    public Vector2 offset;

    [Header("Depend")]
    public Image image;
    public CanvasMouseMapping _canvas;
    public CanvasMouseMapping canvas { get => _canvas ?? gameObject.GetComponentInParent<CanvasMouseMapping>(); }
    private Vector3 startPosition;

    private void Reset()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 保存初始位置和 siblingIndex
        startPosition = transform.position;
        image.raycastTarget = false;
        if (OnDragged != null) OnDragged.Invoke(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = canvas.GetMousePos() + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 如果沒有放在放置區域，將物件位置還原
        ResetPos();
        if (OnDragged != null) OnDragged.Invoke(false);
    }

    public void ResetPos()
    {
        transform.position = startPosition;
        image.raycastTarget = true;
    }

    public Vector3 CanvasToLocalPosition(
        RectTransform canvasRect,
        Vector2 screenPosition,
        Transform parentTransform,
        Camera worldCamera = null)
    {
        // Step 1: 將螢幕座標轉換為 Canvas LocalPosition
        Vector2 canvasLocalPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPosition, worldCamera, out canvasLocalPoint))
        {
            // Step 2: 將 Canvas LocalPosition 轉換到目標物件父物件的 LocalPosition
            Vector3 worldPosition = canvasRect.TransformPoint(canvasLocalPoint);
            return parentTransform.InverseTransformPoint(worldPosition);
        }

        return Vector3.zero; // 預設值
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (canvas == null) _canvas = gameObject.GetComponentInParent<CanvasMouseMapping>();
    }
#endif
}
