/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
/********************************
---------------------------------
著作者：RoanAlen
用途：Scale 物體大小 動態效果
---------------------------------
*********************************/

namespace RoanAlen
{
    namespace Extension
    {

        using DG.Tweening;
        using RoanAlen.Attribute;
        using System;
        using System.Collections;
        using UnityEngine;
        using UnityEngine.Events;

        [Serializable]
        public struct ScaleModeFlags
        {
            public bool ScaleIn;
            public bool ScaleOut;
            public bool ScaleBreathing;
        }

        [Serializable]
        public struct DefaultModeFlags
        {
            public bool Default;
        }

        public class DT_ScaleController : MonoBehaviour
        {
            [Header("當前數值")]
            [SerializeField] private bool effect_Active;
            [Range(0, 3), SerializeField]
            private float Scale_Value;

            [ModeBool("ScaleIn", "ScaleOut", "ScaleBreathing")]
            public ScaleModeFlags scaleMode;

            [Space(10)]
            [SerializeField, Trigger_Mode("Default")]
            private DefaultModeFlags shake_Click;
            //[SerializeField,ModeTrigger("DefalutMode")]
            //private DefaultModeFlags defaultMode_Click;

            private Tween scaleTween;
            private Coroutine delayCoroutine;
            [Header("參數設定")]
            [Space(5)]
            [SerializeField] private Ease easeType = Ease.InOutQuad;
            [Space(5)]
            [SerializeField, MinMaxRange(0f, 3f)]
            private FloatRange Breathing_ScaleRange;

            [Space(5)]
            [SerializeField, Tooltip("縮放效果時間"), RandomButton(1, 3)]
            private int durationTime = 1;

            [SerializeField, Tooltip("延遲時間（秒）"), RandomButton(0.1f, 5f)]
            private float SubDelay = 0;

            [EventButton("Events", "Events"), Space(20)] public bool dynamicBool;
            [DynamicHiden("dynamicBool")] public UnityEvent onScaleIn;
            [DynamicHiden("dynamicBool")] public UnityEvent onScaleOut;

            private void OnEnable()
            {
                if (effect_Active)
                {
                    StartScaleWithDelay();
                }
            }


            public void Default_Event()
            {
                if (scaleTween != null) scaleTween.Kill();
                ScaleModeControl(false, false, false);

                float currentScale = transform.localScale.x;

                scaleTween = DOTween.To(
                    () => transform.localScale.x,
                    x => transform.localScale = Vector3.one * x,
                    1f, // 目標是 Vector3.one => x 為 1
                    durationTime
                )
                .OnUpdate(UpdateScaleGroupValue)
                .SetEase(easeType);
                //float currentScale = transform.localScale.x;

                //if (Mathf.Approximately(currentScale, Breathing_ScaleRange.min))
                //{
                //    scaleTween = DOTween.To(
                //        () => transform.localScale.x,
                //        x => transform.localScale = Vector3.one *x,
                //        1f,
                //        durationTime
                //    )
                //    .OnUpdate(UpdateScaleGroupValue)
                //    .SetEase(easeType);
                //}
                //else
                //{
                //    scaleTween = DOTween.To(
                //        () => transform.localScale.x,
                //        x => transform.localScale = Vector3.one*x,
                //        1,
                //        durationTime / 2f
                //    )
                //    .OnUpdate(UpdateScaleGroupValue)
                //    .SetEase(easeType)
                //    .OnComplete(() =>
                //    {
                //        scaleTween = DOTween.To(
                //            () => transform.localScale.x,
                //            x => transform.localScale = Vector3.one*x,
                //            1f,
                //            durationTime
                //        )
                //        .SetEase(easeType);
                //    });
                //}
            }


            private void StartScaleWithDelay()
            {
                if (delayCoroutine != null) StopCoroutine(delayCoroutine);
                delayCoroutine = StartCoroutine(DelayedStart());
            }

            private IEnumerator DelayedStart()
            {
                yield return new WaitForSeconds(SubDelay);
                if (scaleMode.ScaleIn) DoScaleIn();
                else if (scaleMode.ScaleOut) DoScaleOut();
                else if (scaleMode.ScaleBreathing) DoBreathingScale();
            }

            public void DoScaleIn()
            {
                ScaleModeControl(true, false, false);
                if (scaleTween != null) scaleTween.Kill();

                transform.localScale = Vector3.zero;
                scaleTween = transform.DOScale(Vector3.one, durationTime)
                    .OnUpdate(UpdateScaleGroupValue)
                    .SetEase(easeType)
                    .OnComplete(() => onScaleIn?.Invoke());
            }

            public void DoScaleOut()
            {
                ScaleModeControl(false, true, false);
                if (scaleTween != null) scaleTween.Kill();

                scaleTween = transform.DOScale(Vector3.zero, durationTime)
                    .OnUpdate(UpdateScaleGroupValue)
                    .SetEase(easeType)
                    .OnComplete(() => onScaleOut?.Invoke());
            }

            public void DoBreathingScale()
            {
                ScaleModeControl(false, false, true);
                if (scaleTween != null) scaleTween.Kill();

                float currentScale = transform.localScale.x;

                if (Mathf.Approximately(currentScale, Breathing_ScaleRange.min))
                {
                    scaleTween = DOTween.To(
                        () => transform.localScale.x,
                        x => transform.localScale = Vector3.one * x,
                        Breathing_ScaleRange.max,
                        durationTime
                    )
                    .OnUpdate(UpdateScaleGroupValue)
                    .SetEase(easeType)
                    .SetLoops(-1, LoopType.Yoyo);
                }
                else
                {
                    scaleTween = DOTween.To(
                        () => transform.localScale.x,
                        x => transform.localScale = Vector3.one * x,
                        Breathing_ScaleRange.min,
                        durationTime / 2f
                    )
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        scaleTween = DOTween.To(
                            () => transform.localScale.x,
                            x => transform.localScale = Vector3.one * x,
                            Breathing_ScaleRange.max,
                            durationTime
                        )
                        .OnUpdate(UpdateScaleGroupValue)
                        .SetEase(easeType)
                        .SetLoops(-1, LoopType.Yoyo);
                    });
                }
            }

            public void DoBreathingScaleCount(int Count)
            {
                ScaleModeControl(false, false, true);
                if (scaleTween != null) scaleTween.Kill();

                float currentScale = transform.localScale.x;

                if (Mathf.Approximately(currentScale, Breathing_ScaleRange.min))
                {
                    scaleTween = DOTween.To(
                        () => transform.localScale.x,
                        x => transform.localScale = Vector3.one * x,
                        Breathing_ScaleRange.max,
                        durationTime
                    )
                    .OnUpdate(UpdateScaleGroupValue)
                    .SetEase(easeType)
                    .SetLoops(Count, LoopType.Yoyo);
                }
                else
                {
                    scaleTween = DOTween.To(
                        () => transform.localScale.x,
                        x => transform.localScale = Vector3.one * x,
                        Breathing_ScaleRange.min,
                        durationTime / 2f
                    )
                    .OnUpdate(UpdateScaleGroupValue)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        scaleTween = DOTween.To(
                            () => transform.localScale.x,
                            x => transform.localScale = Vector3.one * x,
                            Breathing_ScaleRange.max,
                            durationTime
                        )
                        .OnUpdate(UpdateScaleGroupValue)
                        .SetEase(easeType)
                        .SetLoops(Count, LoopType.Yoyo);
                    });
                }
            }

            private void UpdateScaleGroupValue()
            {
                Vector3 scale = transform.localScale;
                Scale_Value = (scale.x + scale.y + scale.z) / 3f;

                if (scaleMode.ScaleIn == scaleMode.ScaleOut == scaleMode.ScaleBreathing == false)
                {
                    StopBreathing();
                }
            }
            public void StopBreathing(float targetScale = 1f)
            {
                if (scaleTween != null)
                {
                    scaleTween.Kill();
                    scaleTween = null;
                }
                //transform.localScale = Vector3.one * targetScale;
            }

            private void ScaleModeControl(bool inMode, bool outMode, bool breathMode)
            {
                scaleMode.ScaleIn = inMode;
                scaleMode.ScaleOut = outMode;
                scaleMode.ScaleBreathing = breathMode;
            }

#if UNITY_EDITOR
            private ScaleModeFlags lastScaleMode;

            private void OnValidate()
            {
                if (effect_Active)
                {
                    if (!scaleMode.Equals(lastScaleMode))
                    {
                        lastScaleMode = scaleMode;
                        StartScaleWithDelay();
                    }
                }
            }
#endif
        }
    }
}
