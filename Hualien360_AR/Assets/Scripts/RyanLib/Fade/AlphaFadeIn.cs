/*************************************************
  * 名稱：AlphaFadeIn
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class AlphaFadeIn : MonoBehaviour
{
    public bool activeEnable;
    [Range(0f, 1f)] public float endValue = 1f;
    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;

    public UnityEvent onFadeIn;

    [SerializeField, HideInInspector] Image image;
    Tweener core = null;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        if (activeEnable) FadeIn();
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        float delay = transform.GetSiblingIndex() * subDelay;

        core = image.DOFade(endValue, speed)
            .From(0f)
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