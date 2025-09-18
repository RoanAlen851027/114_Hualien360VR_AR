/*************************************************
  * 名稱：CanvasFadeOut
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class CanvasFadeOut : MonoBehaviour
{
    public bool activeEnable;
    [Range(0f, 1f)] public float endValue = 0f;
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeIn;

    [SerializeField, HideInInspector] CanvasGroup canvas;
    Tweener core = null;

    void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }
    void OnEnable()
    {
        if (activeEnable) FadeOut();
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        if (canvas.blocksRaycasts == false) return;

        float delay = transform.GetSiblingIndex() * subDelay;
        canvas.blocksRaycasts = false;

        core = canvas.DOFade(endValue, speed)
            .From(1f)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
            });
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