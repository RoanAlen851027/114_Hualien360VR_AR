/*************************************************
  * 名稱：ScaleFadeBoth
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ScaleFadeBoth : MonoBehaviour
{
    public bool effectEnable;

    public Vector3 nativeSize = Vector3.one;
    [Range(0f, 5f)] public float speed = 0.5f;
    public bool useSiblingIndex = true;
    [Range(0f, 1f)] public float delay = 0f;

    public Ease easeIn = Ease.OutBack;
    public UnityEvent onFadeIn;
    public Ease easeOut = Ease.InSine;
    public UnityEvent onFadeOut;

    DirtyBool dirty;
    bool? isFade = null;
    Tweener core = null;

    void OnEnable()
    {
        dirty = new DirtyBool(effectEnable);
        if (effectEnable)
        {
            OnEffectEnable(effectEnable);
        } else
        {
            transform.localScale = Vector3.zero;
        }
    }
    void OnDisable()
    {
        if (core != null) DOTween.Kill(core);
        isFade = null;
    }

    public void OnEffectEnable(bool io) { if (io) FadeIn(); else FadeOut(); }

    void Update()
    {
        if (dirty.isDirty(effectEnable))
        {
            OnEffectEnable(effectEnable);
        }
    }

    void FadeIn()
    {
        if (isFade == true) return;
        isFade = true;

        float useDelay = useSiblingIndex ? transform.GetSiblingIndex() * delay : delay;
        core = transform.DOScale(nativeSize, speed)
            .From(Vector3.zero)
            .SetEase(easeIn)
            .SetDelay(useDelay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
                isFade = null;
            });
    }
    void FadeOut()
    {
        if (isFade == false) return;
        isFade = false;

        float useDelay = useSiblingIndex ? transform.GetSiblingIndex() * delay : delay;
        core = transform.DOScale(Vector3.zero, speed)
            .From(nativeSize)
            .SetEase(easeOut)
            .SetDelay(useDelay)
            .OnComplete(() =>
            {
                onFadeOut?.Invoke();
                core = null;
                isFade = null;
            });
    }

    public void FadeBool(bool io)
    {
        if (io) FadeIn(); else FadeOut();
    }
}