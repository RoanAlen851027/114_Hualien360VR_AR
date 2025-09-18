/*************************************************
  * 名稱：MoveFadeBoth
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MoveFadeBoth : MonoBehaviour
{
    public bool activeEnable;
    public Vector2 fromV;
    public Vector2 toV;
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeIn;
    public UnityEvent onFadeOut;

    bool? isFade = null;

    [SerializeField, HideInInspector] RectTransform rect;
    Tweener core = null;

    void Awake()
    {
        rect = (RectTransform)transform;
    }
    void OnEnable()
    {
        if (activeEnable) FadeIn();
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        if (isFade == true) return;
        isFade = true;

        float delay = transform.GetSiblingIndex() * subDelay;

        core = rect.DOLocalMove(toV, speed)
            .From(fromV)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
            });
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        if (isFade == false) return;
        isFade = false;

        float delay = transform.GetSiblingIndex() * subDelay;

        core = rect.DOLocalMove(fromV, speed)
            .From(toV)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeOut?.Invoke();
                core = null;
            });
    }

    public void FadeBool(bool io)
    {
        if (io)
            FadeIn();
        else
            FadeOut();
    }

    private void OnDestroy()
    {
        if (core != null) DOTween.Kill(core);
    }

    private void OnValidate()
    {
        Awake();
    }
}