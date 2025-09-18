using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class ScaleChange : MonoBehaviour
{
    public bool activeEnable;
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    [Range(0f, 5f)] public float targetScale = 2f;

    [Range(0f, 5f)] public float initialScale = 1f;

    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeIn;
    Tweener core = null;

    private Vector3 baseScale;


    public void FadeIn()
    {
        baseScale = new Vector3(initialScale, initialScale, initialScale);
        transform.localScale = baseScale;
        float delay = transform.GetSiblingIndex() * subDelay;

        // 目標縮放是「初始大小 * 目標倍數」
        Vector3 target = baseScale * targetScale;

        core = transform.DOScale(target, speed)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
            });
    }
}
