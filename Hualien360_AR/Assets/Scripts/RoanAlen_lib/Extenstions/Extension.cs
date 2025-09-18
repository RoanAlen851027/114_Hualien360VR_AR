/********************************
---------------------------------
著作者：RoanAlen
用途：擴充工具
---------------------------------
*********************************/
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoanAlen
{
    namespace Extension
    {
        public static class Extension
        {

        }

        public static class ValueExtension
        {

            #region 參數代換
            /// <summary>
            /// 補0參數 - Float
            /// </summary>
            /// <param name="value"></param>
            /// <param name="digits">幾位數</param>
            /// <returns></returns>
            public static string ZeroPadding(this float value, int digits)
            {
                int intValue = Mathf.FloorToInt(value);  // 轉換為整數
                return intValue.ToString($"D{digits}");
            }
            /// <summary>
            /// 補0參數 - Int
            /// </summary>
            /// <param name="value"></param>
            /// <param name="digits">幾位數</param>
            /// <returns></returns>
            public static string ZeroPadding(this int value, int digits)
            {
                return value.ToString($"D{digits}");
            }

            /// <summary>
            /// 顯示小數點後幾位 - Float
            /// </summary>
            /// <param name="value"></param>
            /// <param name="digits"></param>
            /// <returns></returns>
            public static string DecimalPadding(this float value, int digits)
            {
                float multiplier = Mathf.Pow(10, digits);
                float truncatedValue = Mathf.Floor(value * multiplier) / multiplier;
                return truncatedValue.ToString($"F{digits}");
            }

            /// <summary>
            /// String 轉 Int
            /// </summary>
            /// <param name="str"></param>
            /// <param name="defaultValue"></param>
            /// <returns></returns>
            public static int StringToInt(this string str, int defaultValue = 0)
            {
                return int.TryParse(str, out int result) ? result : defaultValue;
            }

            /// <summary>
            /// String 轉 Float 
            /// </summary>
            /// <param name="str"></param>
            /// <param name="defaultValue"></param>
            /// <returns></returns>
            public static float StringToFloat(this string str, float defaultValue = 0f)
            {
                return float.TryParse(str, out float result) ? result : defaultValue;
            }

            /// <summary>
            /// 最小值 , 最大值 => 回傳數值0~1 (Float)
            /// </summary>
            /// <param name="value">Float 當前數值</param>
            /// <param name="min">Float 最小值</param>
            /// <param name="max">Float 最大值</param>
            /// <returns></returns>
            public static float ToInverseLerp_Float(this float value, float min, float max)
            {
                return Mathf.InverseLerp(min, max, value);
            }

            /// <summary>
            /// 最小值 , 最大值 => 回傳數值0~1 (Float)
            /// </summary>
            /// <param name="value">Int 當前數值</param>
            /// <param name="min">Int 最小值</param>
            /// <param name="max">Int 最大值</param>
            /// <returns></returns>
            public static float ToInverseLerp_Int(this int value, int min, int max)
            {
                return (float)Mathf.InverseLerp(min, max, value);
            }

            #endregion

        }
    }
}