/*************************************************
  * 名稱：CanvasMouseMapping
  * 作者：RyanHsu
  * 功能說明：Canvas的MousePosition映射
  * ***********************************************/
using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Canvas))]
public class CanvasMouseMapping : MonoBehaviour
{
    [Header("Watching")]
    [DisplayOnly][SerializeField] Vector2 mousePos;
    public Vector2 GetMousePos() => mousePos;

    Canvas canvas = null;
    public Canvas Canvas { get => canvas ?? GetComponent<Canvas>(); }

    RectTransform canvasRect = null;
    public RectTransform CanvasRect { get => canvasRect ?? Canvas.GetComponent<RectTransform>(); }

    void Update()
    {
        if (enabled) GetMousepPos();
    }

    /// <summary>
    /// 取得Imput.Mouse在Canvas中的映射位置
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMousepPos()
    {
        if (Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                CanvasRect,
                Input.mousePosition,
                null,
                out mousePos
            );
        } else if (Canvas.renderMode == RenderMode.ScreenSpaceCamera || Canvas.renderMode == RenderMode.WorldSpace)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                Canvas.worldCamera,
                out mousePos
            );
        } else
        {
            Debug.LogError("不支援的 Canvas 渲染模式！");
            return Vector2.zero;
        }

        return mousePos;
    }

}
