/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
/********************************
---------------------------------
著作者：RoanAlen
用途：DoTween - CanvasGroupFade動態效果
---------------------------------
*********************************/
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using RoanAlen.Attribute;
using System.Collections;
namespace RoanAlen
{
    namespace Extension
    {
        [Serializable]
        public struct FadeModeFlags
        {
            public bool FadeIn;
            public bool FadeOut;
            public bool FadeBreathing;
        }

        [Serializable]
        public struct ShakeModeFlags
        {
            public bool Shake;
        }
        [RequireComponent(typeof(CanvasGroup))]
        public class DT_FadeController : MonoBehaviour
        {
            [Header("當前數值")]
            [SerializeField]
            private bool effect_Active;
            [SerializeField, DisplayOnly]
            private CanvasGroup canvasGroup;
            [Range(0, 1), SerializeField]
            private float Canvas_Group_Value;

            [SerializeField, ModeBool("FadeIn", "FadeOut", "FadeBreathing")]
            private FadeModeFlags fadeMode;

            //[SerializeField, ModeTrigger("Shake")]
            //private ShakeModeFlags shake_Click;

            private Tween fadeTween;


            public void Shake_Event()
            {
                Debug.Log("觸發發拉");
                this.gameObject.transform.RoanAlen_DOShake();
            }

            [Header("參數設定")]
            [SerializeField, Space(5)]
            private Ease easeType = Ease.InOutQuad;


            [Space(5)]
            [SerializeField, MinMaxRange(0f, 1f)]
            private FloatRange Breathing_alphaRange;//透明度
            [Space(5)]
            [SerializeField, Tooltip("效果時間"), RandomButton(1, 3)]
            private float durationTime = 1;// 效果時間（秒）

            [SerializeField, Tooltip("延遲時間（秒）"), RandomButton(0.1f, 5f)]
            private float SubDelay = 0;

            private Coroutine delayCoroutine;

            [EventButton("Events", "Events"), Space(20)] public bool dynamicBool;
            [DynamicHiden("dynamicBool")] public UnityEvent onFadeIn;
            [DynamicHiden("dynamicBool")] public UnityEvent onFadeOut;

            private void OnEnable()
            {
                //if (effect_Active == true)
                //{
                //    if (fadeMode.FadeIn == true)
                //    {
                //        RoanAlen_DoFadeIn();
                //    }
                //    if (fadeMode.FadeOut == true)
                //    {
                //        RoanAlen_DoFadeOut();
                //    }
                //    if (fadeMode.Breathing == true)
                //    {
                //        RoanAlen_DoBreathingFade();
                //    }
                //}
                if (effect_Active)
                {
                    StartFadeWithDelay();
                }
            }

            public void StartFadeWithDelay()
            {
                if (delayCoroutine != null) StopCoroutine(delayCoroutine);
                delayCoroutine = StartCoroutine(DelayedStart());
            }

     

            private IEnumerator DelayedStart()
            {
                yield return new WaitForSeconds(SubDelay);

                if (fadeMode.FadeIn)
                {
                    RoanAlen_DoFadeIn();
                }
                else if (fadeMode.FadeOut)
                {
                    RoanAlen_DoFadeOut();
                }
                else if (fadeMode.FadeBreathing)
                {
                    RoanAlen_DoBreathingFade();
                }
            }

            /// <summary>
            /// 開始呼吸效果
            /// </summary>
            public void RoanAlen_DoBreathingFade()
            {
                // 先確保沒有殘留的 Tween
                canvasGroup.blocksRaycasts = true;

                if (fadeTween != null)
                {
                    fadeTween.Kill();
                    fadeTween = null;
                }
                FadeModeControl(false, false, true);
                if (canvasGroup == null) return;
                float currentAlpha = canvasGroup.alpha;

                if (Mathf.Approximately(currentAlpha, Breathing_alphaRange.min))
                {
                    // 如果已經在 minAlpha，就直接開始呼吸
                    fadeTween = DOTween.To(
                         () => canvasGroup.alpha,
                         x => canvasGroup.alpha = x,
                         Breathing_alphaRange.max,
                         durationTime
                     )
                     .OnUpdate(UpdateCanvasGroupValue)
                     .SetEase(easeType)
                     .SetLoops(-1, LoopType.Yoyo); // << 每次 DOTween 變動時更新;
                }
                else
                {
                    // 如果不是，先平滑地從 current ➔ minAlpha，再開始呼吸
                    fadeTween = DOTween.To(
                         () => canvasGroup.alpha,
                         x => canvasGroup.alpha = x,
                         Breathing_alphaRange.min,
                         durationTime / 2f // 可以控制快一點到 minAlpha
                     )
                     .OnUpdate(UpdateCanvasGroupValue)
                     .SetEase(easeType)
                     .OnComplete(() =>
                     {
                         fadeTween = DOTween.To(
                              () => canvasGroup.alpha,
                              x => canvasGroup.alpha = x,
                              Breathing_alphaRange.max,
                              durationTime
                          )
                          .OnUpdate(UpdateCanvasGroupValue)
                          .SetEase(easeType)
                          .SetLoops(-1, LoopType.Yoyo); // << 每次 DOTween 變動時更新;
                     });
                }


            }


            private void UpdateCanvasGroupValue()
            {
                Canvas_Group_Value = canvasGroup.alpha;
                if (fadeMode.FadeOut == fadeMode.FadeIn == fadeMode.FadeBreathing == false)
                {
                    StopBreathing();
                }
            }


            /// <summary>
            /// 停止呼吸效果
            /// </summary>
            public void StopBreathing(float targetAlpha = 1f)
            {
                if (fadeTween != null)
                {
                    fadeTween.Kill();
                    fadeTween = null;
                }
                if (canvasGroup != null)
                {
                    //canvasGroup.alpha = targetAlpha; // 預設停下來後設成不透明
                }

            }

            /// <summary>
            /// 使用 CanvasGroup 進行淡入效果
            /// </summary>
            public void RoanAlen_DoFadeIn()
            {
                FadeModeControl(true, false, false);
                // 如果有正在播放的動畫，停止它
                if (fadeTween != null)
                {
                    fadeTween.Kill();
                    fadeTween = null;
                }
                canvasGroup.blocksRaycasts = true;
                fadeTween = canvasGroup.DOFade(1f, durationTime).SetEase(easeType).OnUpdate(UpdateCanvasGroupValue).OnComplete(FadeIn_Finished);
            }
            private void FadeIn_Finished()
            {
                onFadeIn?.Invoke();
            }

            /// <summary>
            /// 使用 CanvasGroup 進行淡出效果
            /// </summary>
            public void RoanAlen_DoFadeOut()
            {
                FadeModeControl(false, true, false);
                // 如果有正在播放的動畫，停止它
                if (fadeTween != null)
                {
                    fadeTween.Kill();
                }
                canvasGroup.blocksRaycasts = false;

                fadeTween = canvasGroup.DOFade(0f, durationTime).SetEase(easeType).OnUpdate(UpdateCanvasGroupValue).OnComplete(FadeOut_Finished);
            }
            private void FadeOut_Finished()
            {
                onFadeOut?.Invoke();
            }

            private void FadeModeControl(bool fadeIn, bool fadeOut, bool fadeBreathing)
            {
                fadeMode.FadeIn = fadeIn;
                fadeMode.FadeOut = fadeOut;
                fadeMode.FadeBreathing = fadeBreathing;
            }

#if UNITY_EDITOR
            private FadeModeFlags lastFadeMode; // 用來記錄上一次的狀態，避免重複播放

            private void OnValidate()
            {
                if (canvasGroup == null)
                {
                    this.gameObject.TryGetComponent<CanvasGroup>(out canvasGroup);
                }

                if (effect_Active == true)
                {
                    if (!fadeMode.Equals(lastFadeMode))
                    {
                        lastFadeMode = fadeMode;
                        //if (fadeMode.FadeIn)
                        //{
                        //    RoanAlen_DoFadeIn();
                        //}
                        //else if (fadeMode.FadeOut)
                        //{
                        //    RoanAlen_DoFadeOut();
                        //}
                        //else if (fadeMode.Breathing)
                        //{
                        //    RoanAlen_DoBreathingFade();
                        //}
                        StartFadeWithDelay(); // 使用相同延遲邏輯
                    }
                }
            }
#endif
        }
    }
}