/*************************************************
  * 名稱：ScaleFadeOut
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ScaleFadeOut : MonoBehaviour
{
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeOut;
    Tweener core = null;

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        float delay = transform.GetSiblingIndex() * subDelay;
        core = transform.DOScale(Vector3.zero, speed)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeOut?.Invoke();
                core = null;
            });
    }

    private void OnDestroy()
    {
        if (core != null) DOTween.Kill(core);
    }

}