/*************************************************
  * 名稱：ScaleFadeIn
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ScaleFadeIn : MonoBehaviour
{
    public bool activeEnable;
    [DisplayOnly] public Vector3 nativeSize;
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeIn;
    Tweener core = null;

    void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        nativeSize = rect.localScale;
    }
    void OnEnable()
    {
        if (activeEnable) FadeIn();
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        if (!enabled) return;

        float delay = transform.GetSiblingIndex() * subDelay;
        core = transform.DOScale(nativeSize, speed)
            .From(Vector3.zero)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
            });
    }

    private void OnValidate()
    {
        Awake();
    }
}