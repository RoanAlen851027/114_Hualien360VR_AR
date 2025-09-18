/********************************
---------------------------------
著作者：RoanAlen
用途：DoTween - 擴充功能
---------------------------------
*********************************/
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace RoanAlen
{
    namespace Extension
    {
        public static class DT_Extension
        {
            /// <summary>
            /// ( 當前數值 , 最小值 , 最大值 , 撥放時間 , 撥放效果 )
            /// </summary>
            public static Tweener RoanAlen_DOFillAmountByValue(this Image img, float value, float min, float max, float duration, Ease ease = Ease.OutCubic)
            {
                float targetValue = Mathf.InverseLerp(min, max, value);
                return img.DOFillAmount(targetValue, duration).SetEase(ease);
            }

            /// <summary>
            /// 搖晃效果
            /// </summary>
            /// <param name="target">目標</param>
            /// <param name="duration"></param>
            /// <param name="strength">力度</param>
            /// <param name="vibrato"></param>
            /// <param name="randomness"></param>
            /// <returns></returns>
            public static Tweener RoanAlen_DOShake(this Transform target, float duration = 0.5f, float strength = 10f, int vibrato = 10, float randomness = 90f)
            {
                return target.DOShakePosition(duration, strength, vibrato, randomness);
            }

            /// <summary>
            /// 彈出警告視窗效果 (讓 RectTransform 從縮小狀態展開)
            /// </summary>
            public static Tweener RoanAlen_DoPopUp(this RectTransform rect, float duration = 1, Ease ease = Ease.OutBack)
            {
                // 初始化視窗為縮小的狀態
                rect.localScale = Vector3.zero;

                // 使用 DOTween 動畫將其縮放到目標大小
                return rect.DOScale(1, duration).SetEase(ease);
            }

            /// <summary>
            /// 收回警告視窗效果 (讓 RectTransform 從正常大小縮回)
            /// </summary>
            public static Tweener RoanAlen_DoPopDown(this RectTransform rect, float duration = 1, Ease ease = Ease.InBack)
            {
                // 使用 DOTween 動畫將其縮小回來
                return rect.DOScale(0, duration).SetEase(ease);
            }


        }


    }

}

