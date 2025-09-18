/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class PointerShine : MonoBehaviour
{
    public Image original;          // 原始圖
    public Sprite OnSelect;          // 選擇時的圖
    public Sprite OnDeselect;        // 取消選擇時的圖（若沒有可設為 null）

    public float scaleUpSize = 1.2f;
    public float scaleDuration = 0.2f;

    public UnityEvent OnSelectEvent;
    public UnityEvent OnDeselectEvent;
    // 選擇時呼叫：放大 & 換成 OnSelect 圖片
    public void OnSelectAction()
    {
        if (original != null && OnSelect != null)
        {
            original.sprite = OnSelect;
        }
        OnSelectEvent?.Invoke();
        // 放大動畫
        transform.DOScale(Vector3.one * scaleUpSize, scaleDuration).SetEase(Ease.OutBack);
    }

    // 取消選擇時呼叫：縮小 & 換回原圖
    public void OnDiselectAction()
    {
        if (original != null && OnDeselect != null)
        {
            original.sprite = OnDeselect;
        }
        OnDeselectEvent?.Invoke();  
        // 縮回動畫
        transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.InOutSine);
    }
}
