/*************************************************
  * 名稱：iMathf
  * 作者：RyanHsu
  * 功能說明：通用計算單元
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class iMathf
{
    public static float CyclicNum(float x, float A, float B)
    {
        float rangeSize = B - A + 1;
        return A + ((x - A) % rangeSize + rangeSize) % rangeSize;
    }

    public static int CyclicNum(int x, int A, int B)
    {
        int rangeSize = B - A + 1;
        return A + ((x - A) % rangeSize + rangeSize) % rangeSize;
    }

    /// <summary> 給一個角度angle以及半徑distance，計算落點Vector2 </summary>
    /// <param name="angle"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector2 CalculatePointOnCircle(float angle, float distance)
    {
        // 將角度轉換為弧度
        float radians = angle * Mathf.Deg2Rad;

        // 計算 x 和 y 座標
        float x = distance * Mathf.Cos(radians);
        float y = distance * Mathf.Sin(radians);

        return new Vector2(x, y);
    }
}