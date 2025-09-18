/*************************************************
  * 名稱：MoveFadeBoth_Tf
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MoveFadeBoth_Tf : MonoBehaviour
{
    public bool activeEnable;
    public void CopyAxisFrom() => fromV = transform.localPosition;
    [ContextMenuItem("CopyAxis", "CopyAxisFrom")] public Vector3 fromV;
    public void CopyAxisTo() => toV = transform.localPosition;
    [ContextMenuItem("CopyAxis", "CopyAxisTo")] public Vector3 toV;

    [Range(0f, 5f)] public float speed = 0.5f;
    [Range(0f, 1f)] public float subDelay = 0f;
    public Ease ease = Ease.InOutSine;
    public bool useOutEase = false;
    [DynamicHide("useOutEase")] public Ease easeOut = Ease.InOutSine;

    public UnityEvent onFadeIn;
    public UnityEvent onFadeOut;

    bool? isFade = null;

    Tweener core = null;

    void OnEnable()
    {
        if (activeEnable) FadeIn();
    }

    void OnDisable()
    {
        isFade = null;
        if (core != null) DOTween.Kill(core, true);
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        if (isFade == true) return;
        isFade = true;

        float delay = transform.GetSiblingIndex() * subDelay;

        core = transform.DOLocalMove(toV, speed)
            .From(fromV)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeIn?.Invoke();
                core = null;
                isFade = null;
            });
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        if (isFade == false) return;
        isFade = false;

        float delay = transform.GetSiblingIndex() * subDelay;

        core = transform.DOLocalMove(fromV, speed)
            .From(toV)
            .SetEase(useOutEase ? easeOut : ease)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                onFadeOut?.Invoke();
                core = null;
                isFade = null;
            });
    }

    [ContextMenu("FadeYoYo")]
    public void FadeYoYo()
    {
        CancelInvoke();
        FadeOut();
        this.Invoke(FadeIn, 1f);

        //if (isFade == null) isFade = true; // 如果尚未開始過，預設為 FadeIn 狀態

        //float delay = transform.GetSiblingIndex() * subDelay;

        //core = transform.DOLocalMove(isFade == true ? fromV : toV, speed)
        //    .From(isFade == true ? toV : fromV)
        //    .SetEase(ease)
        //    .SetDelay(delay)
        //    .SetLoops(2, LoopType.Yoyo) // 設置無限次 YoYo 循環
        //    .OnStepComplete(() =>
        //    {
        //        isFade = !isFade; // 每次完成一次完整的 YoYo 循環後切換狀態
        //    })
        //    .OnComplete(() =>
        //    {
        //        core = null;
        //        isFade = null;
        //    });
    }

    public void FadeBool(bool io)
    {
        if (io)
            FadeIn();
        else
            FadeOut();
    }

}